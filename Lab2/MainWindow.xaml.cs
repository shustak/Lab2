using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lab2
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private double startX;
        private double endX;
        private double stepX;
        private int n;
        private int selectedFunction;
        private string result;
        private ObservableCollection<string> results;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Results = new ObservableCollection<string>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public double StartX
        {
            get => startX;
            set { startX = value; OnPropertyChanged(); }
        }

        public double EndX
        {
            get => endX;
            set { endX = value; OnPropertyChanged(); }
        }

        public double StepX
        {
            get => stepX;
            set { stepX = value; OnPropertyChanged(); }
        }

        public int N
        {
            get => n;
            set { n = value; OnPropertyChanged(); }
        }

        public int SelectedFunction
        {
            get => selectedFunction;
            set { selectedFunction = value; OnPropertyChanged(); }
        }

        public string Result
        {
            get => result;
            set { result = value; OnPropertyChanged(); }
        }

        public ObservableCollection<string> Results
        {
            get => results;
            set { results = value; OnPropertyChanged(); }
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            ResetTextBoxStyles();
            Results.Clear();
            Result = string.Empty;
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
            if (N < 5)
            {
                inputIsValid = false;
                SetTextBoxErrorStyle(textBoxN, "Invalid input: N must be greater than 4.");
            }
            if (!inputIsValid)
            {
                return;
            }

            for (double x = StartX; x <= EndX; x += StepX)
            {
                if (SelectedFunction == 0)
                {
                    double sX = CalculateSX(x, N);
                    Results.Add($"S({x}) = {sX}");
                    Result = $"S({x}) = {sX}";
                }
                else if (SelectedFunction == 1)
                {
                    double yX = CalculateYX(x);
                    Results.Add($"Y({x}) = {yX}");
                    Result = $"Y({x}) = {yX}";
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
            textBox.Background = Brushes.White;
            if (textBox.ToolTip is ToolTip toolTip)
            {
                toolTip.IsOpen = false;
            }
            textBox.ToolTip = null;
        }

        private void SetTextBoxErrorStyle(TextBox textBox, string errorMessage)
        {
            textBox.Background = Brushes.LightCoral;
            ToolTip toolTip = new ToolTip
            {
                Content = errorMessage,
                IsOpen = true,
                PlacementTarget = textBox,
                Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom
            };
            textBox.ToolTip = toolTip;
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
