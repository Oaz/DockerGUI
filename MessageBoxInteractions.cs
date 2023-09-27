using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using MsBox.Avalonia;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;
using ReactiveUI;

namespace DockerGUI
{
  public static class MessageBoxInteractions
  {
    public static void RegisterStandardMessageBox<T>(this Control control, Func<T, Interaction<MessageBoxStandardParams, ButtonResult>> getInteraction)
      where T:ReactiveObject
    {
      control.DataContextChanged += (sender, args) =>
      {
        var viewModel = control.DataContext as T;
        if (viewModel == null)
          return;
        getInteraction(viewModel).RegisterHandler(async interaction =>
        {
          var result = await MessageBoxManager
            .GetMessageBoxStandard(interaction.Input)
            .ShowAsPopupAsync((Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow);
          interaction.SetOutput(result);
        });
      };
    }
  }
}