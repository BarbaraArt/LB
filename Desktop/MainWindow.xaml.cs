using Desktop.View;
using System;
using System.Collections.Generic;
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
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new AddTaskWindow());
            MainFrame.Navigate(new Main_empty());
            MainFrame.Navigate(new Main());
            MainFrame.Navigate(new Registration());

            var vm = new ViewModel();
            this.DataContext = vm;

            // Устанавливаем Action для навигации из VM
            vm.NavigateToPage = page =>
            {
                MainFrame.Navigate(page);
            };
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Сбрасываем ошибки
            
            emailErrorLabel.Visibility = Visibility.Collapsed;
            passwordErrorLabel.Visibility = Visibility.Collapsed;

          
            string email = emailTextBox.Text;
            string password = passwordTextBox.Text;

            bool isValid = true; // Флаг для общей валидности


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
                MainFrame.Navigate(new Main_empty());
            }
        }



        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false; // Email пустой
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
            MainFrame.Navigate(new Registration());

        }
    }
}
   


