using System;
using System.Collections.Generic;
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

namespace Desktop.View
{
    /// <summary>
    /// Логика взаимодействия для AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Page
    {
        public TaskItem NewTask { get; private set; }
        public event EventHandler<TaskItem> TaskCreated;
       

        public AddTaskWindow()
        {
            InitializeComponent();
        }
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            NewTask = new TaskItem
            {
                Title = TitleBox.Text,
                Category = CategoryCombo.Text,
                Description = DescBox.Text,
                Time = TimeBox.Text,
                Date = TaskCalendar.SelectedDate?.ToShortDateString() ?? DateTime.Now.ToShortDateString(),
                IsCompleted = false
            };

            
            TaskCreated?.Invoke(this, NewTask);


            if (this.NavigationService.CanGoBack)
                this.NavigationService.GoBack();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
                this.NavigationService.GoBack();
        }
    }
}
