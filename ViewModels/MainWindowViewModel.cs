using DockerGUI.Models;

namespace DockerGUI.ViewModels
{
  public class MainWindowViewModel : ViewModelBase
  {
    public MainWindowViewModel()
    {
      var docker = new DockerService();
      Pull = new PullViewModel(docker);
      Images = new ImagesViewModel(docker);
      Containers = new ContainersViewModel(docker);
    }
    public PullViewModel Pull { get; }
    public ImagesViewModel Images { get; }
    public ContainersViewModel Containers { get; }
  }
}