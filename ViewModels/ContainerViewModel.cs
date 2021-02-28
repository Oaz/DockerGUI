using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Docker.DotNet.Models;
using DockerGUI.Models;
using ReactiveUI;

namespace DockerGUI.ViewModels
{
  public class ContainerViewModel : ViewModelBase
  {
    public ContainerViewModel(DockerService docker, ContainerListResponse container)
    {
      _docker = docker;
      ID = container.ID;
      Name = container.Names[0];
      ImageName = container.Image;
      
      void UpdateContainerDetails() => Task.Run(async () =>
      {
        var d = await docker.Client.Containers.InspectContainerAsync(ID);
        IsStarted = d.State.Running;
        IsRemovable = !IsStarted || !d.HostConfig.AutoRemove;

        string FormatHostPort(PortBinding b) =>
          $"{b.HostIP}:{b.HostPort}".Replace("0.0.0.0",docker.Name);
        PublicPorts = d.NetworkSettings.Ports.Values
          .SelectMany(bs => bs.Select(FormatHostPort))
          .ToArray();
      });
      UpdateContainerDetails();
      docker.Events
        .Where(e => e.Type == DockerEventType.Container && e.Subject == ID)
        .Subscribe( e => UpdateContainerDetails());
    }
    
    public async Task Start()
    {
      await _docker.Client.Containers.StartContainerAsync(
        Name,
        new ContainerStartParameters()
      );
    }
    public async Task Stop()
    {
      await _docker.Client.Containers.StopContainerAsync(
        Name,
        new ContainerStopParameters()
      );
    }
    public async Task Remove()
    {
      await _docker.Client.Containers.RemoveContainerAsync(
        Name,
        new ContainerRemoveParameters()
      );
    }
    
    public string ID { get; }
    public string Name { get; }
    public string ImageName { get; }

    public bool IsRemovable
    {
      get => _isRemovable;
      set => this.RaiseAndSetIfChanged(ref _isRemovable, value);
    }
    private bool _isRemovable;
    public bool IsStarted
    {
      get => _isStarted;
      set => this.RaiseAndSetIfChanged(ref _isStarted, value);
    }
    private bool _isStarted;
    public string[] PublicPorts
    {
      get => _publicPorts;
      set => this.RaiseAndSetIfChanged(ref _publicPorts, value);
    }
    private string[] _publicPorts = null!;
    private DockerService _docker;
  }
}