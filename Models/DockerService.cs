using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;
using DynamicData;

namespace DockerGUI.Models
{
  public class DockerService
  {
    public DockerService()
    {
      Client = new DockerClientConfiguration().CreateClient();
      Events = new DockerEvents(Client);
      Name = string.Empty;
      
      Task.Run(async () =>
      {
        var info = await Client.System.GetSystemInfoAsync();
        Name = info.Name ?? string.Empty;
      });
      
      var images = new SourceCache<ImagesListResponse, string>(x => x.ID);
      Images = images;
      void UpdateImagesCache() => Task.Run(async () => images.EditDiff(
          await Client.Images.ListImagesAsync(new ImagesListParameters {All = true}),
          (i1, i2) => i1.ID == i2.ID
          ));
      UpdateImagesCache();
      Events
        .Where(e => e.Type == DockerEventType.Image)
        .Subscribe( _ => UpdateImagesCache());
      
      var containers = new SourceCache<ContainerListResponse, string>(x => x.ID);
      Containers = containers;
      void UpdateContainersCache() => Task.Run(async () => containers.EditDiff(
        await Client.Containers.ListContainersAsync(new ContainersListParameters { All = true }),
        (c1,c2) => c1.ID == c2.ID
        ));
      UpdateContainersCache();
      Events
        .Where(e => e.Type == DockerEventType.Container)
        .Subscribe( _ => UpdateContainersCache());
    }
    
    public string Name { get; private set; }
    public IDockerClient Client { get; }
    public IObservable<DockerEvent> Events { get; }
    public IObservableCache<ImagesListResponse, string> Images { get; }
    public IObservableCache<ContainerListResponse, string> Containers { get; }
  }
}