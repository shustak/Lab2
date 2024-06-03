using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Part_2
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Employee> Employees { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Employees = new ObservableCollection<Employee>();
            DataContext = this;
            InitializeComboBoxes();
        }

        private void InitializeComboBoxes()
        {
            comboBoxPosition.Items.Add("Менеджер");
            comboBoxPosition.Items.Add("Разработчик");
            comboBoxCity.Items.Add("Брест");
            comboBoxCity.Items.Add("Минск");
            comboBoxStreet.Items.Add("Скарыны");
            comboBoxStreet.Items.Add("Караткевича");
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInputs())
            {
                var newEmployee = new Employee
                {
                    LastName = textBoxLastName.Text,
                    Salary = decimal.Parse(textBoxSalary.Text),
                    Position = comboBoxPosition.Text,
                    City = comboBoxCity.Text,
                    Street = comboBoxStreet.Text,
                    House = textBoxHouse.Text
                };
                Employees.Add(newEmployee);
                listBoxEmployees.Items.Add(newEmployee.ToString());
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(textBoxLastName.Text))
            {
                MessageBox.Show("Фамилия не может быть пустой.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!decimal.TryParse(textBoxSalary.Text, out decimal salary) || salary <= 0)
            {
                MessageBox.Show("Зарплата должна быть положительным числовым значением.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(comboBoxPosition.Text))
            {
                MessageBox.Show("Должность не может быть пустой.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(comboBoxCity.Text))
            {
                MessageBox.Show("Город не может быть пустым.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(comboBoxStreet.Text))
            {
                MessageBox.Show("Улица не может быть пустой.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxHouse.Text))
            {
                MessageBox.Show("Дом не может быть пустым.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void SaveEmployees_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("employees.txt"))
                {
                    foreach (var employee in Employees)
                    {
                        writer.WriteLine(employee.ToString());
                    }
                }
                MessageBox.Show("Данные сохранены успешно.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}");
            }
        }

        private void LoadEmployees_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Employees.Clear();
                listBoxEmployees.Items.Clear();
                using (StreamReader reader = new StreamReader("employees.txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var data = line.Split(new[] { ", " }, StringSplitOptions.None);
                        if (data.Length == 6)
                        {
                            var employee = new Employee
                            {
                                LastName = data[0],
                                Salary = decimal.Parse(data[1]),
                                Position = data[2],
                                City = data[3],
                                Street = data[4],
                                House = data[5]
                            };
                            Employees.Add(employee);
                            listBoxEmployees.Items.Add(employee.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
        }
    }

    public class Employee : INotifyPropertyChanged
    {
        private string lastName;
        private decimal salary;
        private string position;
        private string city;
        private string street;
        private string house;

        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public decimal Salary
        {
            get => salary;
            set
            {
                salary = value;
                OnPropertyChanged(nameof(Salary));
            }
        }

        public string Position
        {
            get => position;
            set
            {
                position = value;
                OnPropertyChanged(nameof(Position));
            }
        }

        public string City
        {
            get => city;
            set
            {
                city = value;
                OnPropertyChanged(nameof(City));
            }
        }

        public string Street
        {
            get => street;
            set
            {
                street = value;
                OnPropertyChanged(nameof(Street));
            }
        }

        public string House
        {
            get => house;
            set
            {
                house = value;
                OnPropertyChanged(nameof(House));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return $"{LastName}, {Salary}, {Position}, {City}, {Street}, {House}";
        }
    }
}
