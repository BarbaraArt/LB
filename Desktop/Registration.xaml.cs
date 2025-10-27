using Desktop.Repository;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Desktop
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private UserRepository userRepository = new UserRepository();
        public Registration()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            emailError.Visibility = Visibility.Collapsed;
            passwordError.Visibility = Visibility.Collapsed;
            nameErrorLabel.Visibility = Visibility.Collapsed;

            string email = emailTextBox.Text;
            string password = passwordTextBox.Text;
            string name = nameTextBox.Text;

            bool isValid = true;

            if (!IsValidEmail(email))
            {
                emailError.Content = "Некорректный формат email.";
                emailError.Visibility = Visibility.Visible;
                isValid = false;
            }

            if (!IsValidPassword(password))
            {
                passwordError.Content = "Пароль должен содержать не менее шести символов.";
                passwordError.Visibility = Visibility.Visible;
                isValid = false;
            }

            if(!IsValidName(name))
            {
                nameErrorLabel.Content = "Имя введено некорректно.";
                nameErrorLabel.Visibility = Visibility.Visible;
                isValid = false;
            }

            if (isValid)
            {
                bool registrationSuccessful = userRepository.RegisterUser(email, password);

                if (registrationSuccessful)
                {
                    MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    Main_empty mainEmptyWindow = new Main_empty();
                    mainEmptyWindow.Show(); // Открываем новое окно
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Пользователь с таким email уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                // Если не все поля валидны, выводим общее сообщение об ошибке.
                MessageBox.Show("Пожалуйста, проверьте правильность заполнения всех полей.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private bool IsValidName(string name)
        {
            return !string.IsNullOrEmpty(name) && name.Length >= 3;
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
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show(); // Открываем новое окно
            this.Close();
        }
    }
    
}
