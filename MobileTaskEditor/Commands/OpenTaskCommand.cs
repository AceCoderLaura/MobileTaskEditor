using System;
using System.Windows.Input;
using MobileTaskEditor.Pages;
using Xamarin.Forms;

namespace MobileTaskEditor.Commands
{
    public sealed class OpenTaskCommand : ICommand
    {
        public bool CanExecute(object parameter) => true;
        
        public async void Execute(object parameter)
        {
            await Shell.Current.Navigation.PushAsync(new TaskPage());
        }

        public event EventHandler CanExecuteChanged;

        public void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}