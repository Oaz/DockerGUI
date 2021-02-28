using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using DockerGUI.Models;
using ReactiveUI;
using DynamicData;
using DynamicData.Alias;

namespace DockerGUI.ViewModels
{
  public class ContainersViewModel : ViewModelBase
  {
    public ContainersViewModel(DockerService docker)
    {
      docker.Containers
        .Connect()
        .Select(x => new ContainerViewModel(docker, x))
        .ObserveOn(RxApp.MainThreadScheduler)
        .Bind(out _containers)
        .Subscribe();
    }

    public ReadOnlyObservableCollection<ContainerViewModel> AsList => _containers;
    private readonly ReadOnlyObservableCollection<ContainerViewModel> _containers;
  }
}