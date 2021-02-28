using System;
using Docker.DotNet.Models;
using ReactiveUI;

namespace DockerGUI.ViewModels
{
  public class ProgressViewModel : ViewModelBase, IProgress<JSONMessage>
  {
    public void Report(JSONMessage value)
    {
      Status = value.Status;
      Message = value.ProgressMessage;
    }
    
    public string Status
    {
      get => _status;
      set => this.RaiseAndSetIfChanged(ref _status, value);
    }
    private string _status = null!;
    
    public string Message
    {
      get => _message;
      set => this.RaiseAndSetIfChanged(ref _message, value);
    }
    private string _message = null!;
  }
}