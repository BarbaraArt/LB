using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Data;

namespace Desktop
{
    public class ViewModel : INotifyPropertyChanged
    {
        private TaskItem _selectedTask;
        private bool _showOnlyCompleted = false;
        public string UserName { get; set; } = "Alex";

        public ObservableCollection<TaskItem> Tasks { get; set; }
        public ObservableCollection<string> Categories { get; set; }
        public ICollectionView FilteredTasks { get; set; }
        public TaskItem SelectedTask
        {
            get => _selectedTask;
            set { _selectedTask = value; OnPropertyChanged(); }
        }

        public ICommand CompleteCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand AddTaskCommand { get; }
        public ICommand ShowTasksCommand { get; }
        public ICommand ShowHistoryCommand { get; }

        public ViewModel()
        {
            Categories = new ObservableCollection<string> { "Дом", "Работа", "Учеба", "Отдых" };

            Tasks = new ObservableCollection<TaskItem>();
            FilteredTasks = CollectionViewSource.GetDefaultView(Tasks);
            FilteredTasks.Filter = TaskFilter;

            Tasks = new ObservableCollection<TaskItem>();

            // Настраиваем фильтрацию
            FilteredTasks = CollectionViewSource.GetDefaultView(Tasks);
            FilteredTasks.Filter = TaskFilter;

            // Команда "Готово"
            CompleteCommand = new RelayCommand(o => {
                if (SelectedTask != null)
                {
                    SelectedTask.IsCompleted = true;
                    FilteredTasks.Refresh(); // Обновляем список, чтобы задача исчезла из текущего вида
                    SelectedTask = null;    // Снимаем выделение
                }
            });

            DeleteCommand = new RelayCommand(o => {
                if (SelectedTask != null) Tasks.Remove(SelectedTask);
            });

            // Команды переключения вкладок
            ShowTasksCommand = new RelayCommand(o => {
                _showOnlyCompleted = false;
                FilteredTasks.Refresh();
            });

            ShowHistoryCommand = new RelayCommand(o => {
                _showOnlyCompleted = true;
                FilteredTasks.Refresh();
            });

            AddTaskCommand = new RelayCommand(o => {
                AddTaskWindow addWindow = new AddTaskWindow();
                if (addWindow.ShowDialog() == true)
                {
                    Tasks.Add(addWindow.NewTask);
                    FilteredTasks.Refresh();
                }
            });
        }
        private bool TaskFilter(object obj)
        {
            if (obj is TaskItem task)
            {
                if (_showOnlyCompleted)
                    return task.IsCompleted; // В истории только выполненные
                else
                    return !task.IsCompleted; // В задачах только невыполненные
            }
            return false;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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

