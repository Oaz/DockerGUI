using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DockerGUI.Views
{
  public class PortMappingView : UserControl
  {
    public PortMappingView()
    {
      InitializeComponent();
    }

    private void InitializeComponent()
    {
      AvaloniaXamlLoader.Load(this);
    }
  }
}