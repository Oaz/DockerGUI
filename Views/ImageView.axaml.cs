using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DockerGUI.ViewModels;

namespace DockerGUI.Views
{
  public class ImageView : UserControl
  {
    public ImageView()
    {
      InitializeComponent();
      this.RegisterStandardMessageBox<ImageViewModel>(imageViewModel => imageViewModel.ConfirmDeleteImage);
    }

    private void InitializeComponent()
    {
      AvaloniaXamlLoader.Load(this);
    }
  }
}