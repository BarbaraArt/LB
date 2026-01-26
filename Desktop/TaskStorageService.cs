using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using Newtonsoft.Json;


namespace Desktop
{
    public class TaskStorageService
    {
        private const string FileName = "tasks.json";

        public void SaveTasks(ObservableCollection<TaskItem> tasks)
        {
            try
            {
                string jsonString = JsonConvert.SerializeObject(tasks, Formatting.Indented);
                File.WriteAllText(FileName, jsonString);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения задач: {ex.Message}");
            }
        }

        public ObservableCollection<TaskItem> LoadTasks()
        {
            try
            {
                if (!File.Exists(FileName))
                    return new ObservableCollection<TaskItem>();

                string jsonString = File.ReadAllText(FileName);
                var tasks = JsonConvert.DeserializeObject<ObservableCollection<TaskItem>>(jsonString);
                return tasks ?? new ObservableCollection<TaskItem>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки задач: {ex.Message}");
                return new ObservableCollection<TaskItem>();
            }
        }
    }
}
