using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DockerGUI.Views
{
  public partial class ImagesView : UserControl
  {
    public ImagesView()
    {
      InitializeComponent();
    }

    private void InitializeComponent()
    {
      AvaloniaXamlLoader.Load(this);
    }
  }
}