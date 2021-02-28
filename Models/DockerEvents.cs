using System;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace DockerGUI.Models
{
  public class DockerEvents : IObservable<DockerEvent>, IProgress<Message>, IDisposable
  {
    public DockerEvents(IDockerClient docker)
    {
      _observers = new ObservableHelper<DockerEvent>();
      _cancelSource = new CancellationTokenSource();
      Task.Run( () => docker.System.MonitorEventsAsync(
        new ContainerEventsParameters(),
        this,
        _cancelSource.Token
      ));
    }

    public void Report(Message message)
    {
      var ev = new DockerEvent(message);
      _observers.ForEach(o => o.OnNext(ev));
      Console.WriteLine($"{ev.Time.ToLocalTime():O} {ev.Type} {ev.Action} {ev.Subject}");
    }
    
    private readonly CancellationTokenSource _cancelSource;
    private readonly ObservableHelper<DockerEvent> _observers;

    public void Dispose()
    {
      _cancelSource.Cancel();
      _cancelSource.Dispose();
    }

    public IDisposable Subscribe(IObserver<DockerEvent> observer)
    {
      return _observers.Subscribe(observer);
    }
  }
}