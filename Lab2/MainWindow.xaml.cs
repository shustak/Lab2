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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            ResetTextBoxStyles();
            listBoxResults.Items.Clear();
            bool inputIsValid = true;

            if (!double.TryParse(textBoxStartX.Text, out double startX))
            {
                inputIsValid = false;
                SetTextBoxErrorStyle(textBoxStartX, "Invalid input: Please enter a valid number for Start X.");
            }
            if (!double.TryParse(textBoxEndX.Text, out double endX))
            {
                inputIsValid = false;
                SetTextBoxErrorStyle(textBoxEndX, "Invalid input: Please enter a valid number for End X.");
            }
            if (!double.TryParse(textBoxStepX.Text, out double stepX))
            {
                inputIsValid = false;
                SetTextBoxErrorStyle(textBoxStepX, "Invalid input: Please enter a valid number for Step X.");
            }
            if (!int.TryParse(textBoxN.Text, out int N))
            {
                inputIsValid = false;
                SetTextBoxErrorStyle(textBoxN, "Invalid input: Please enter a valid number for N.");
            }
            if (N < 4)
            {
                inputIsValid = false;
                SetTextBoxErrorStyle(textBoxN, "Invalid input: N must be greater than 3.");
            }
            if (!inputIsValid)
            {
                return;
            }

            string selectedFunction = (comboBoxFunction.SelectedItem as ComboBoxItem)?.Content.ToString();
            for (double x = startX; x <= endX; x += stepX)
            {
                if (selectedFunction == "S(x) = Σ(x^k/k!)" || comboBoxFunction.SelectedIndex == 0)  // Проверяем, какая функция выбрана
                {
                    double sX = CalculateSX(x, N);
                    listBoxResults.Items.Add($"S({x}) = {sX}");
                }
                else if (selectedFunction == "Y(x) = e^x cos(4 cos(x sin(x)))" || comboBoxFunction.SelectedIndex == 1)
                {
                    double yX = CalculateYX(x);
                    listBoxResults.Items.Add($"Y({x}) = {yX}");
                }
            }
        }

        private double CalculateSX(double x, int N)
        {
            double sum = 0;
            for (int k = 0; k <= N; k++)
            {
                sum += Math.Pow(x, k) / Factorial(k);
            }
            return sum;
        }

        private double CalculateYX(double x)
        {
            return Math.Exp(x) * Math.Cos(4 * Math.Cos(x * Math.Sin(x)));
        }

        private static int Factorial(int n)
        {
            int result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }

        private void ResetTextBoxStyles()
        {
            ResetTextBox(textBoxStartX);
            ResetTextBox(textBoxEndX);
            ResetTextBox(textBoxStepX);
            ResetTextBox(textBoxN);
        }

        private void ResetTextBox(TextBox textBox)
        {
            textBox.Background = Brushes.White; // Возвращаем стандартный белый фон
            if (textBox.ToolTip is ToolTip toolTip)
            {
                toolTip.IsOpen = false; // Закрываем подсказку
            }
            textBox.ToolTip = null; // Удаляем подсказку
        }

        private void SetTextBoxErrorStyle(TextBox textBox, string errorMessage)
        {
            textBox.Background = Brushes.LightCoral; // Изменение цвета фона на красный
            ToolTip toolTip = new ToolTip
            {
                Content = errorMessage,
                IsOpen = true, // Устанавливаем подсказку как открытую
                PlacementTarget = textBox, // Указываем, что подсказка должна быть показана рядом с textBox
                Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom // Размещение под textBox
            };
            textBox.ToolTip = toolTip;
        }
    }
}
