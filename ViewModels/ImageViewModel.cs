using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Docker.DotNet.Models;
using DockerGUI.Models;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using ReactiveUI;

namespace DockerGUI.ViewModels
{
  public class ImageViewModel : ViewModelBase
  {
    public ImageViewModel(DockerService docker, ImagesListResponse image)
    {
      _docker = docker;
      Name = image.RepoTags?.ElementAt(0) ?? "---";
      ID = image.ID;
      ShortID = ID.Substring(7,12);
      IsRoot = image.ParentID == string.Empty;
      Size = image.Size;
      DisplaySize = $"{((float)image.Size) / 1024 / 1024:F1}MB";
      Tags = image.RepoTags!;
      _raw = image;
      ConfirmDeleteImage = new Interaction<MessageBoxStandardParams, ButtonResult>();
      NewContainer = new NewContainerViewModel(docker, this);
      
      Task.Run(async () =>
      {
        var d = await docker.Client.Images.InspectImageAsync(ID);
        var exposedPorts = d.Config.ExposedPorts;
        if (exposedPorts == null)
          return;
        var x = exposedPorts.Keys.ToArray();
        ExposedPorts = x;
      });
    }

    public Interaction<MessageBoxStandardParams, ButtonResult> ConfirmDeleteImage { get; }

    public async Task DeleteImage()
    {
      var dialog = new MessageBoxStandardParams
      {
        ContentTitle = "Confirm Deletion",
        ContentMessage = $"Please confirm deletion of {Name} {ShortID}",
        ButtonDefinitions = ButtonEnum.OkCancel,
        Icon = Icon.Warning
      };
      var result = await ConfirmDeleteImage.Handle(dialog);
      if (result != ButtonResult.Ok)
        return;
      try
      {
        await _docker.Client.Images.DeleteImageAsync(
          ID,
          new ImageDeleteParameters()
        );
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
    }
    
    public string Name { get; }
    public string ID { get; }
    public string ShortID { get; }
    public long Size { get; }
    public string DisplaySize { get; }
    public bool IsRoot { get; }
    public IEnumerable<string> Tags { get; }
    public NewContainerViewModel NewContainer { get; }
    
    public string[] ExposedPorts
    {
      get => _exposedPorts;
      set => this.RaiseAndSetIfChanged(ref _exposedPorts, value);
    }
    private string[] _exposedPorts = null!;
    
    public bool IsChildOf(ImageViewModel parent) => _raw.ParentID == parent.ID;
    private readonly ImagesListResponse _raw;
    private DockerService _docker;

  }
}