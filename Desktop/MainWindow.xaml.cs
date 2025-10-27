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
                Main_empty mainEmptyWindow = new Main_empty();
                mainEmptyWindow.Show(); // Открываем новое окно
                this.Close();
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
            Registration registrationWindow = new Registration();
            registrationWindow.Show(); // Открываем новое окно
            this.Close();

        }
    }
}
   


