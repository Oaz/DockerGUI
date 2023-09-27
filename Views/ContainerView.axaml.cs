using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace DockerGUI.Views
{
  public partial class ContainerView : UserControl
  {
    public ContainerView()
    {
      InitializeComponent();
    }

    private void InitializeComponent()
    {
      AvaloniaXamlLoader.Load(this);
    }

    private void OpenWeb(object? sender, RoutedEventArgs e)
    {
      var url = $"http://{((sender as Button)?.Content as TextBlock)?.Text}";
      Console.WriteLine($"Opening {url}");
      if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
        System.Diagnostics.Process.Start("xdg-open", url);
      else
        System.Diagnostics.Process.Start(url);
    }
  }
}