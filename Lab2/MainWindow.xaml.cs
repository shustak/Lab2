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
            listBoxResults.Items.Clear();
            if (!double.TryParse(textBoxStartX.Text, out double startX) ||
                !double.TryParse(textBoxEndX.Text, out double endX) ||
                !double.TryParse(textBoxStepX.Text, out double stepX))
            {
                MessageBox.Show("Please enter valid numbers for start, end, and step values.");
                return;
            }

            for (double x = startX; x <= endX; x += stepX)
            {
                double sX = CalculateSX(x);
                double yX = CalculateYX(x);
                listBoxResults.Items.Add($"S({x}) = {sX}, Y({x}) = {yX}");
            }
        }

        private double CalculateSX(double x)
        {
            double sum = 0;
            for (int k = 0; k <= 4; k++)
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
    }
}
