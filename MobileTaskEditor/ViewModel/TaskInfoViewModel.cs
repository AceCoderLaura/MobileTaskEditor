﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using MobileTaskEditor.Annotations;
using MobileTaskEditor.Commands;
using MobileTaskEditor.Model;

namespace MobileTaskEditor.ViewModel
{
    public sealed class TaskInfoViewModel : INotifyPropertyChanged
    {
        private TaskInfo _taskInfo;
        
        public SaveTaskCommand SaveTaskCommand { get; }

        public TaskInfoViewModel()
        {
            SaveTaskCommand = new SaveTaskCommand(this);
        }

        /// <summary>The data component of this view model.</summary>
        /// <remarks>
        /// I think in traditional MVVM you're supposed to make your view model inherit from the data model.
        /// I find doing it that way makes getting data from a server more annoying because you have to copy the fields across to the view model.
        /// For that reason I usually use a property instead.
        /// </remarks>
        public TaskInfo TaskInfo
        {
            get => _taskInfo;
            set
            {
                if (Equals(value, _taskInfo)) return;

                if (_taskInfo != null) _taskInfo.PropertyChanged -= TaskInfoOnPropertyChanged;
                _taskInfo = value;
                if (_taskInfo != null) _taskInfo.PropertyChanged += TaskInfoOnPropertyChanged;
                
                OnPropertyChanged();
                SaveTaskCommand.OnCanExecuteChanged();
            }
        }

        /// <remarks>This kinda sucks. How can I do this better?</remarks>
        private void TaskInfoOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SaveTaskCommand.OnCanExecuteChanged();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}