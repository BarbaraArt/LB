using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Globalization;
using Desktop.View; 
using Desktop; 


namespace Desktop.View
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {

        private ViewModel _vm;

        public Main()
        {
            InitializeComponent();
            _vm = new ViewModel();
            DataContext = _vm;
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var addTaskWindow = new AddTaskWindow();
            addTaskWindow.TaskCreated += AddTaskWindow_TaskCreated;
            this.NavigationService.Navigate(addTaskWindow);
        }

        private void AddTaskWindow_TaskCreated(object sender, TaskItem newTask)
        {
            
            _vm.Tasks.Add(newTask);
        }
    }
}
