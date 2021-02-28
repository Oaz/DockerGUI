using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Docker.DotNet.Models;
using DockerGUI.Models;
using DynamicData.Binding;
using ReactiveUI;

namespace DockerGUI.ViewModels
{
  public class NewContainerViewModel : ViewModelBase
  {
    public NewContainerViewModel(DockerService docker, ImageViewModel image)
    {
      _docker = docker;
      _image = image;
      _name = string.Empty;
      IsAutoRemove = true;
      _portMapping = image
        .WhenPropertyChanged(i => i.ExposedPorts)
        .Select(x => x.Value?.Select(e => new PortMappingViewModel(e))?.ToArray())
        .ToProperty(this, x => x.PortMapping)!;
    }
    
    public async Task Create()
    {
      var result = await _docker.Client.Containers.CreateContainerAsync(
        new CreateContainerParameters
        {
          Name = Name,
          Image = _image.Name,
          HostConfig = new HostConfig
          {
            AutoRemove = IsAutoRemove,
            PortBindings = PortMapping.ToDictionary(
              pm => pm.ContainerPort,
              pm=>pm.Binding
              )
          }
        }
        );
    }
    
    public string Name
    {
      get => _name;
      set => this.RaiseAndSetIfChanged(ref _name, value);
    }
    private string _name;
    
    public bool IsAutoRemove
    {
      get => _isAutoRemove;
      set => this.RaiseAndSetIfChanged(ref _isAutoRemove, value);
    }
    private bool _isAutoRemove;

    public PortMappingViewModel[] PortMapping => _portMapping.Value;
    private readonly ObservableAsPropertyHelper<PortMappingViewModel[]> _portMapping;
    
    private readonly DockerService _docker;
    private readonly ImageViewModel _image;
  }
}