using System;
using System.Windows.Input;
using MobileTaskEditor.Model;
using MobileTaskEditor.Pages;
using Xamarin.Forms;

namespace MobileTaskEditor.Commands
{
    public sealed class OpenTaskCommand : ICommand
    {
        public bool CanExecute(object parameter) => parameter is ItemTappedEventArgs args && args.Item is TaskInfo;

        public async void Execute(object parameter)
        {
            if (parameter is ItemTappedEventArgs args && args.Item is TaskInfo t)
            {
                await Shell.Current.Navigation.PushAsync(new TaskPage(t));
            }
            else
            {
                throw new ArgumentException($"Expected {nameof(ItemTappedEventArgs)} with a {nameof(TaskInfo)}.", nameof(parameter));
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}