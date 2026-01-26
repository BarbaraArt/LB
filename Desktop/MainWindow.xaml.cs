using Desktop.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Desktop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<TaskItem> TaskList { get; } = new ObservableCollection<TaskItem>();
        public MainWindow()
        {
            InitializeComponent();

            /*MainFrame.Navigate(new AddTaskWindow());
            MainFrame.Navigate(new Main_empty());
            MainFrame.Navigate(new Main());
            MainFrame.Navigate(new Registration());*/
            DataContext = this;

            var vm = new ViewModel();
            this.DataContext = vm;

            
            vm.NavigateToPage = page =>
            {
                MainFrame.Navigate(page);
            };
        }

        public async void NavigateWithFade(Page nextPage)
        {
            
            var fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(300));
            var tcs = new TaskCompletionSource<bool>();

            fadeOut.Completed += (s, e) => tcs.SetResult(true);
            MainFrame.BeginAnimation(Frame.OpacityProperty, fadeOut);

            await tcs.Task;

            
            MainFrame.Navigate(nextPage);

            
            var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(300));
            MainFrame.BeginAnimation(Frame.OpacityProperty, fadeIn);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            
            emailErrorLabel.Visibility = Visibility.Collapsed;
            passwordErrorLabel.Visibility = Visibility.Collapsed;

          
            string email = emailTextBox.Text;
            string password = passwordTextBox.Text;

            bool isValid = true;


            if (!IsValidEmail(email))
            {
                emailErrorLabel.Content = "Некорректный формат email.";
                emailErrorLabel.Visibility = Visibility.Visible;
                isValid = false;
            }

            if (!IsValidPassword(password))
            {
                passwordErrorLabel.Content = "Пароль должен содержать не менее шести символов.";
                passwordErrorLabel.Visibility = Visibility.Visible;
                isValid = false;
            }

            if (isValid)
            {
                ContentPanel.Visibility = Visibility.Collapsed;
                NavigateWithFade(new Main_empty());
            }
        }
        private void OpenAddTaskPage()
        {
            var addTaskPage = new AddTaskWindow();
            addTaskPage.TaskCreated += AddTaskPage_TaskCreated;
            MainFrame.Navigate(addTaskPage);
        }

        private void AddTaskPage_TaskCreated(object sender, TaskItem newTask)
        {
            
            TaskList.Add(newTask);

            
            if (MainFrame.CanGoBack)
                MainFrame.GoBack();

           
        }


        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false; 
            }

            Regex regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
        }

        private bool IsValidPassword(string password)
        {
            return !string.IsNullOrEmpty(password) && password.Length >= 6;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ContentPanel.Visibility = Visibility.Collapsed;
            NavigateWithFade(new Registration());
            

        }
    }
}
   


