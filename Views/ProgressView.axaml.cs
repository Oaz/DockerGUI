using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DockerGUI.Views
{
  public class ProgressView : UserControl
  {
    public ProgressView()
    {
      InitializeComponent();
    }

    private void InitializeComponent()
    {
      AvaloniaXamlLoader.Load(this);
    }
  }
}