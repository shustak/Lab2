﻿using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Part_2
{
    public partial class MainWindow : Window
    {
        private List<Employee> employees = new List<Employee>();

        public MainWindow()
        {
            InitializeComponent();
            InitializeComboBoxes();
        }

        private void InitializeComboBoxes()
        {
            // Заполнение ComboBox данными
            comboBoxPosition.Items.Add("Менеджер");
            comboBoxPosition.Items.Add("Разработчик");
            comboBoxCity.Items.Add("Брест");
            comboBoxCity.Items.Add("Минск");
            comboBoxStreet.Items.Add("Скарыны");
            comboBoxStreet.Items.Add("Караткевича");
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var lastName = textBoxLastName.Text;
                var salary = decimal.Parse(textBoxSalary.Text);
                var position = comboBoxPosition.Text;
                var city = comboBoxCity.Text;
                var street = comboBoxStreet.Text;
                var house = textBoxHouse.Text;

                var newEmployee = new Employee(lastName, salary, position, city, street, house);
                employees.Add(newEmployee);
                listBoxEmployees.Items.Add(newEmployee.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void SaveEmployees_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("employees.txt"))
                {
                    foreach (var item in listBoxEmployees.Items)
                    {
                        writer.WriteLine(item.ToString());
                    }
                }
                MessageBox.Show("Данные сохранены успешно.");
                listBoxEmployees.Items.Clear();
                employees.Clear();
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
                listBoxEmployees.Items.Clear();
                employees.Clear();
                using (StreamReader reader = new StreamReader("employees.txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        listBoxEmployees.Items.Add(line);
                        var data = line.Split(new[] { ", " }, StringSplitOptions.None);
                        if (data.Length == 6)
                        {
                            var employee = new Employee(data[0], decimal.Parse(data[1]), data[2], data[3], data[4], data[5]);
                            employees.Add(employee);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
        }

        private void SaveInputToListBox_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var lastName = textBoxLastName.Text;
                var salary = textBoxSalary.Text;
                var position = comboBoxPosition.Text;
                var city = comboBoxCity.Text;
                var street = comboBoxStreet.Text;
                var house = textBoxHouse.Text;

                var employeeData = $"{lastName}, {salary}, {position}, {city}, {street}, {house}";
                listBoxEmployees.Items.Add(employeeData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }

    public class Employee
    {
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public string Position { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }

        public Employee(string lastName, decimal salary, string position, string city, string street, string house)
        {
            LastName = lastName;
            Salary = salary;
            Position = position;
            City = city;
            Street = street;
            House = house;
        }

        public override string ToString()
        {
            return $"{LastName}, {Salary}, {Position}, {City}, {Street}, {House}";
        }
    }
}
