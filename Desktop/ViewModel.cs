using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Desktop
{
    public class ViewModel : INotifyPropertyChanged
    {
        private TaskItem _selectedTask;
        public string UserName { get; set; } = "Alex";

        public ObservableCollection<TaskItem> Tasks { get; set; }
        public ObservableCollection<string> Categories { get; set; }

        public TaskItem SelectedTask
        {
            get => _selectedTask;
            set { _selectedTask = value; OnPropertyChanged(); }
        }

        public ICommand CompleteCommand { get; }
        public ICommand DeleteCommand { get; }

        public ViewModel()
        {
            Categories = new ObservableCollection<string> { "Дом", "Работа", "Учеба", "Отдых" };

            Tasks = new ObservableCollection<TaskItem>
            {
                new TaskItem { Title = "Go fishing with Stephen", Time = "9:00am", Date = "01 Января 2022", Description = "Поехать на рыбалку с друзьями в субботу.", Category = "Отдых" },
                new TaskItem { Title = "Read the book Zlatan", Time = "11:00am", Date = "02 Января 2022", Description = "Прочитать 50 страниц биографии Ибрагимовича.", IsCompleted = true },
                new TaskItem { Title = "Meet with design team", Time = "14:00pm", Date = "03 Января 2022", Description = "Обсудить новый макет приложения." }
            };

            CompleteCommand = new RelayCommand(o => {
                if (SelectedTask != null) SelectedTask.IsCompleted = true;
            });

            DeleteCommand = new RelayCommand(o => {
                if (SelectedTask != null) Tasks.Remove(SelectedTask);
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    // Простая реализация ICommand
    public class RelayCommand : ICommand
    {
        private readonly System.Action<object> _execute;
        public RelayCommand(System.Action<object> execute) => _execute = execute;
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter) => _execute(parameter);
        public event System.EventHandler CanExecuteChanged;
    }
}

