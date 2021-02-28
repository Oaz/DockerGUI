using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DockerGUI.Views
{
  public class NewContainerView : UserControl
  {
    public NewContainerView()
    {
      InitializeComponent();
    }

    private void InitializeComponent()
    {
      AvaloniaXamlLoader.Load(this);
    }
  }
}