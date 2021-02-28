using System.Collections.Generic;
using Docker.DotNet.Models;
using ReactiveUI;

namespace DockerGUI.ViewModels
{
  public class PortMappingViewModel : ViewModelBase
  {
    public PortMappingViewModel(string containerPort)
    {
      ContainerPort = containerPort;
      _hostPort = string.Empty;
      _hostIP = string.Empty;
    }
    public string ContainerPort { get; }
    
    public string HostPort
    {
      get => _hostPort;
      set => this.RaiseAndSetIfChanged(ref _hostPort, value);
    }
    private string _hostPort;
    
    public string HostIP
    {
      get => _hostIP;
      set => this.RaiseAndSetIfChanged(ref _hostIP, value);
    }
    private string _hostIP;

    public IList<PortBinding> Binding =>
      HostPort.Length == 0
      ? new List<PortBinding>()
      : new List<PortBinding>
        {
          new() { HostPort=HostPort, HostIP=HostIP }
        };

  }
}