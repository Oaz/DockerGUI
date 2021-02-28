using System;
using System.Threading;
using Docker.DotNet.Models;
using DockerGUI.Models;
using ReactiveUI;

namespace DockerGUI.ViewModels
{
  public class PullViewModel : ViewModelBase
  {
    public PullViewModel(DockerService docker)
    {
      _docker = docker;
      Progress = new ProgressViewModel();
      _name = "nginx:latest";
      IsDownloading = false;
    }

    public async void Pull()
    {
      _cancelSource = new CancellationTokenSource();
      IsDownloading = true;
      try
      {
        await _docker.Client.Images.CreateImageAsync(
          new ImagesCreateParameters
          {
            FromImage = Name
          },
          null,
          Progress,
          _cancelSource.Token
        );
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
      finally
      {
        IsDownloading = false;        
      }
    }
    
    public void Cancel()
    {
      _cancelSource.Cancel();
    }

    private ProgressViewModel Progress { get; }
    private CancellationTokenSource _cancelSource = null!;

    public string Name
    {
      get => _name;
      set => this.RaiseAndSetIfChanged(ref _name, value);
    }
    private string _name;
    public bool IsDownloading
    {
      get => _isDownloading;
      set => this.RaiseAndSetIfChanged(ref _isDownloading, value);
    }
    private bool _isDownloading;

    private readonly DockerService _docker;
  }
}