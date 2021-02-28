using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using DockerGUI.Models;
using DynamicData;
using DynamicData.Alias;
using ReactiveUI;

namespace DockerGUI.ViewModels
{
  public class ImagesViewModel : ViewModelBase
  {
    public ImagesViewModel(DockerService docker)
    {
      docker.Images
        .Connect()
        .Select(x => new ImageViewModel(docker, x))
        .Sort( Comparer<ImageViewModel>.Create((x,y) => string.Compare(x.Name, y.Name, StringComparison.Ordinal)))
        .ObserveOn(RxApp.MainThreadScheduler)
        .Bind(out _list)
        .Subscribe(_ =>
        {
          AsTree = Tree<ImageViewModel>.Create<ImageNode>(
            AsList,
            image => image.IsRoot,
            (child, parent, _) => child.IsChildOf(parent),
            (p,c) => new ImageNode(p,c)
          );
        });
    }

    public ReadOnlyObservableCollection<ImageViewModel> AsList => _list;
    private readonly ReadOnlyObservableCollection<ImageViewModel> _list;
    
    public IEnumerable<ImageNode> AsTree
    {
      get => _tree;
      set
      {
        if (HasCurrent && !AsList.Contains(Current!))
          Current = null;
        this.RaiseAndSetIfChanged(ref _tree, value);
      }
    }

    private IEnumerable<ImageNode> _tree = null!;
    
    public ImageNode Selection
    {
      set => Current = value.Parent;
    }

    public ImageViewModel? Current
    {
      get => _current;
      set
      {
        this.RaiseAndSetIfChanged(ref _current, value);
        this.RaisePropertyChanged(nameof(HasCurrent));
      }
    }
    private ImageViewModel? _current = null;
    public bool HasCurrent => _current != null;
  }
  
  public class ImageNode : Tree<ImageViewModel>.Node
  {
    public ImageNode(ImageViewModel parent, IEnumerable<ImageNode> children) : base(parent, children)
    {
    }
  }
}