using System;
using System.Windows;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace lab_rab_2_FurdinNK_BPI2302
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (rbFormula1.IsChecked == true)
                    CalculateFormula1();
                else if (rbFormula2.IsChecked == true)
                    CalculateFormula2();
                else if (rbFormula3.IsChecked == true)
                    CalculateFormula3();
                else if (rbFormula4.IsChecked == true)
                    CalculateFormula4();
                else if (rbFormula5.IsChecked == true)
                    CalculateFormula5();
                else
                    txtResult.Text = "Сначала выберите формулу!";
            }
            catch (Exception ex)
            {
                txtResult.Text = $"Ошибка: {ex.Message}";
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {

            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"[0-9\.,]");
        }


        private void CalculateFormula1()
        {
            double a = ParseNumber(txtA1.Text);
            int f = int.Parse(((ComboBoxItem)cmbF1.SelectedItem).Content.ToString());
            double result = Math.Sin(f * a);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("1 формула:");
            sb.AppendLine($"a = {a:F2}, f = {f}\n");
            sb.AppendLine($"sin({f} * {a:F2}) = {result:F6}");

            txtResult.Text = sb.ToString();
            this.Title = $"Окно программы — Формула 1 (f={f})";
        }

        
        private void CalculateFormula2()
        {
            double a = ParseNumber(txtA2.Text);
            double b = ParseNumber(txtB2.Text);
            int f = int.Parse(((ComboBoxItem)cmbF2.SelectedItem).Content.ToString());
            double result = Math.Cos(f * a) + Math.Sin(f * b);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("2 формула:");
            sb.AppendLine($"a = {a:F2}, b = {b:F2}, f = {f}\n");
            sb.AppendLine($"cos({f}*{a:F2}) + sin({f}*{b:F2}) = {result:F6}");

            txtResult.Text = sb.ToString();
            this.Title = $"Окно программы — Формула 2 (f={f})";
        }

        
        private void CalculateFormula3()
        {
            double a = ParseNumber(txtA3.Text); //parse v double s obrabotkoy
            double b = ParseNumber(txtB3.Text);
            int c = int.Parse(((ComboBoxItem)cmbC3.SelectedItem).Content.ToString());
            int d = int.Parse(((ComboBoxItem)cmbD3.SelectedItem).Content.ToString());
            double result = c * Math.Pow(a, 2) + d * Math.Pow(b, 2);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("3 формула:");
            sb.AppendLine($"a = {a:F2}, b = {b:F2}, c = {c}, d = {d}\n");
            sb.AppendLine($"{c} * ({a:F2})² + {d} * ({b:F2})² = {result:F6}");

            txtResult.Text = sb.ToString();
            this.Title = $"Формула 3 (результат = {result:F4})";
        }

        
        private void CalculateFormula4()
        {
            double a = ParseNumber(txtA4.Text);
            int c = int.Parse(((ComboBoxItem)cmbC4.SelectedItem).Content.ToString());
            int d = int.Parse(txtD4.Text);
            double result = Math.Pow(c + a, d);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("4 формула:");
            sb.AppendLine($"a = {a:F2}, c = {c}, d = {d}\n");
            sb.AppendLine($"({c} + {a:F2})^{d} = {result:F6}");

            txtResult.Text = sb.ToString();
            this.Title = $"Формула 4 (результат = {result:F4})";
        }

        
        private void CalculateFormula5()
        {
            double x = ParseNumber(txtX5.Text);
            double y = ParseNumber(txtY5.Text);
            int N = int.Parse(((ComboBoxItem)cmbN5.SelectedItem).Content.ToString());
            int K = int.Parse(((ComboBoxItem)cmbK5.SelectedItem).Content.ToString());

            double sum = 0;
            StringBuilder details = new StringBuilder();
            details.AppendLine("Детали вычислений:");

            for (int i = 1; i <= N; i++)
            {
                for (int j = 1; j <= K; j++)
                {
                    double numerator = 2 * Math.Pow(x, i) + 3 * Math.Pow(y, j);
                    double denominator = Math.Pow(i, 2) * Math.Pow(j, 2);
                    double term = numerator / denominator;
                    sum += term;

                    details.AppendLine($"  i={i}, j={j}: (2*{x:F2}^{i} + 3*{y:F2}^{j}) / ({i}²*{j}²) = {term:F6}");
                }
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("5 формула:");
            sb.AppendLine($"Z = Σ(i=1..{N}) Σ(j=1..{K}) (2·xⁱ + 3·yʲ) / (i²·j²)");
            sb.AppendLine($"x = {x:F2}, y = {y:F2}, N = {N}, K = {K}");
            sb.AppendLine();
            sb.AppendLine($"Результат: Z = {sum:F6}");
            sb.AppendLine();
            sb.AppendLine(details.ToString());

            txtResult.Text = sb.ToString();
            this.Title = $"Окно программы — Формула 5 (Z = {sum:F4})";
        }

        
        private double ParseNumber(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new Exception("Пустое поле");

            text = text.Replace('.', ',');
            if (double.TryParse(text, out double result))
                return result;

            throw new Exception($"Некорректное число: {text}");
        }
    }
}