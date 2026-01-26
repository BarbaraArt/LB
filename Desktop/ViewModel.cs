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
using Desktop.View;
using System.Windows.Controls;
using System.Collections.Specialized;




namespace Desktop
{
    public class ViewModel : INotifyPropertyChanged
    {
       
        private TaskStorageService _storageService = new TaskStorageService();

        public Action<Page> NavigateToPage { get; set; }
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
        public void SaveTasksOnExit()
        {
            _storageService.SaveTasks(Tasks);
        }
        public ViewModel()
        {

            Tasks = _storageService.LoadTasks();
            

            foreach (var task in Tasks)
            {
                task.PropertyChanged += Task_PropertyChanged;
            }

            Categories = new ObservableCollection<string> { "Дом", "Работа", "Учеба", "Отдых" };
            
            Tasks.CollectionChanged += Tasks_CollectionChanged;

            Tasks = new ObservableCollection<TaskItem>();

            FilteredTasks = CollectionViewSource.GetDefaultView(Tasks);
            FilteredTasks.Filter = TaskFilter;

            Tasks = new ObservableCollection<TaskItem>();

           
            FilteredTasks = CollectionViewSource.GetDefaultView(Tasks);
            FilteredTasks.Filter = TaskFilter;

           
            CompleteCommand = new RelayCommand(o => {
                if (SelectedTask != null)
                {
                    SelectedTask.IsCompleted = true;
                    FilteredTasks.Refresh(); 
                    SelectedTask = null;   
                }
            });

            DeleteCommand = new RelayCommand(o => {
                if (SelectedTask != null) Tasks.Remove(SelectedTask);
            });

            
            ShowTasksCommand = new RelayCommand(o => {
                _showOnlyCompleted = false;
                FilteredTasks.Refresh();
            });

            ShowHistoryCommand = new RelayCommand(o => {
                _showOnlyCompleted = true;
                FilteredTasks.Refresh();
            });

            AddTaskCommand = new RelayCommand(o =>
            {
                var addPage = new AddTaskWindow();
                addPage.TaskCreated += (sender, newTask) =>
                {
                    Tasks.Add(newTask);
                    FilteredTasks.Refresh();
                };
                NavigateToPage?.Invoke(addPage);
            }); ;
        }
        private bool TaskFilter(object obj)
        {
            if (obj is TaskItem task)
            {
                if (_showOnlyCompleted)
                    return task.IsCompleted; 
                else
                    return !task.IsCompleted; 
            }
            return false;
        }

        private void Tasks_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            _storageService.SaveTasks(Tasks);

            if (e.NewItems != null)
            {
                foreach (TaskItem newTask in e.NewItems)
                    newTask.PropertyChanged += Task_PropertyChanged;
            }

            if (e.OldItems != null)
            {
                foreach (TaskItem oldTask in e.OldItems)
                    oldTask.PropertyChanged -= Task_PropertyChanged;
            }
        }

        private void Task_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _storageService.SaveTasks(Tasks);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        public RelayCommand(Action<object> execute) => _execute = execute;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => _execute(parameter);

        public event EventHandler CanExecuteChanged;
    }


}

