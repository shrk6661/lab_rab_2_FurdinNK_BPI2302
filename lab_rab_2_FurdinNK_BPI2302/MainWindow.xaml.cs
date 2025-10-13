using System;
using System.Windows;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace lab_rab_2_FurdinNK_BPI2302
{
    public partial class MainWindow : Window
    {
        // Кисти для нормального и ошибочного состояния
        private readonly SolidColorBrush normalBorderBrush = new SolidColorBrush(Colors.Gray);
        private readonly SolidColorBrush errorBorderBrush = new SolidColorBrush(Colors.Red);
        private readonly SolidColorBrush normalBackgroundBrush = new SolidColorBrush(Colors.White);
        private readonly SolidColorBrush errorBackgroundBrush = new SolidColorBrush(Color.FromArgb(30, 255, 0, 0));

        public MainWindow()
        {
            InitializeComponent();
            SubscribeToTextChanges();
        }

        private void SubscribeToTextChanges()
        {
            // Подписываемся на все TextBox'ы для валидации в реальном времени
            txtA1.TextChanged += OnTextChanged;
            txtA2.TextChanged += OnTextChanged;
            txtB2.TextChanged += OnTextChanged;
            txtA3.TextChanged += OnTextChanged;
            txtB3.TextChanged += OnTextChanged;
            txtA4.TextChanged += OnTextChanged;
            txtD4.TextChanged += OnTextChanged;
            txtX5.TextChanged += OnTextChanged;
            txtY5.TextChanged += OnTextChanged;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            // Сбрасываем ошибку и подсветку при изменении текста
            var textBox = (TextBox)sender;
            ResetTextBoxStyle(textBox);

            txtError.Visibility = Visibility.Collapsed;
            txtError.Text = "";
        }

        private void ResetTextBoxStyle(TextBox textBox)
        {
            // Сбрасываем стиль к нормальному
            textBox.BorderBrush = normalBorderBrush;
            textBox.Background = normalBackgroundBrush;
            textBox.ToolTip = null;
        }

        private void ResetAllTextBoxStyles()
        {
            // Сбрасываем все TextBox'ы к нормальному состоянию
            ResetTextBoxStyle(txtA1);
            ResetTextBoxStyle(txtA2);
            ResetTextBoxStyle(txtB2);
            ResetTextBoxStyle(txtA3);
            ResetTextBoxStyle(txtB3);
            ResetTextBoxStyle(txtA4);
            ResetTextBoxStyle(txtD4);
            ResetTextBoxStyle(txtX5);
            ResetTextBoxStyle(txtY5);
        }

        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Сбрасываем все стили перед проверкой
                ResetAllTextBoxStyles();

                // Сбрасываем сообщение об ошибке
                txtError.Visibility = Visibility.Collapsed;
                txtError.Text = "";

                // Проверяем валидность перед вычислениями
                if (!ValidateInputs())
                {
                    return;
                }

                // Если валидация прошла успешно - выполняем вычисления
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

        private bool ValidateInputs()
        {
            bool isValid = true;
            TextBox firstErrorField = null;

            // Проверяем заполненность полей в зависимости от выбранной формулы
            if (rbFormula1.IsChecked == true)
            {
                if (string.IsNullOrWhiteSpace(txtA1.Text))
                {
                    HighlightErrorField(txtA1, "Заполните параметр 'a' для формулы 1");
                    firstErrorField ??= txtA1;
                    isValid = false;
                }
            }
            else if (rbFormula2.IsChecked == true)
            {
                if (string.IsNullOrWhiteSpace(txtA2.Text))
                {
                    HighlightErrorField(txtA2, "Заполните параметр 'a' для формулы 2");
                    firstErrorField ??= txtA2;
                    isValid = false;
                }
                if (string.IsNullOrWhiteSpace(txtB2.Text))
                {
                    HighlightErrorField(txtB2, "Заполните параметр 'b' для формулы 2");
                    firstErrorField ??= txtB2;
                    isValid = false;
                }
            }
            else if (rbFormula3.IsChecked == true)
            {
                if (string.IsNullOrWhiteSpace(txtA3.Text))
                {
                    HighlightErrorField(txtA3, "Заполните параметр 'a' для формулы 3");
                    firstErrorField ??= txtA3;
                    isValid = false;
                }
                if (string.IsNullOrWhiteSpace(txtB3.Text))
                {
                    HighlightErrorField(txtB3, "Заполните параметр 'b' для формулы 3");
                    firstErrorField ??= txtB3;
                    isValid = false;
                }
            }
            else if (rbFormula4.IsChecked == true)
            {
                if (string.IsNullOrWhiteSpace(txtA4.Text))
                {
                    HighlightErrorField(txtA4, "Заполните параметр 'a' для формулы 4");
                    firstErrorField ??= txtA4;
                    isValid = false;
                }
                if (string.IsNullOrWhiteSpace(txtD4.Text))
                {
                    HighlightErrorField(txtD4, "Заполните параметр 'd' для формулы 4");
                    firstErrorField ??= txtD4;
                    isValid = false;
                }
            }
            else if (rbFormula5.IsChecked == true)
            {
                if (string.IsNullOrWhiteSpace(txtX5.Text))
                {
                    HighlightErrorField(txtX5, "Заполните параметр 'x' для формулы 5");
                    firstErrorField ??= txtX5;
                    isValid = false;
                }
                if (string.IsNullOrWhiteSpace(txtY5.Text))
                {
                    HighlightErrorField(txtY5, "Заполните параметр 'y' для формулы 5");
                    firstErrorField ??= txtY5;
                    isValid = false;
                }
            }

            if (!isValid && firstErrorField != null)
            {
                ShowError("Заполните все обязательные поля");
                firstErrorField.Focus();
            }

            return isValid;
        }

        private void ShowError(string message)
        {
            txtError.Text = message;
            txtError.Visibility = Visibility.Visible;
        }

        private void HighlightErrorField(TextBox textBox, string errorMessage)
        {
            // Подсвечиваем поле с ошибкой
            textBox.BorderBrush = errorBorderBrush;
            textBox.Background = errorBackgroundBrush;
            textBox.ToolTip = errorMessage;
        }

        // Остальные методы CalculateFormula1-5 и ParseNumber остаются без изменений
        // ... (ваш существующий код вычислений)

        // ---------- Формула 1 ----------
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

        // ---------- Формула 2 ----------
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

        // ---------- Формула 3 ----------
        private void CalculateFormula3()
        {
            double a = ParseNumber(txtA3.Text);
            double b = ParseNumber(txtB3.Text);
            int c = int.Parse(((ComboBoxItem)cmbC3.SelectedItem).Content.ToString());
            int d = int.Parse(((ComboBoxItem)cmbD3.SelectedItem).Content.ToString());
            double result = c * Math.Pow(a, 2) + d * Math.Pow(b, 2);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("3 формула:");
            sb.AppendLine($"a = {a:F2}, b = {b:F2}, c = {c}, d = {d}\n");
            sb.AppendLine($"{c} * ({a:F2})² + {d} * ({b:F2})² = {result:F6}");

            txtResult.Text = sb.ToString();
            this.Title = $"Окно программы — Формула 3 (результат = {result:F4})";
        }

        // ---------- Формула 4 ----------
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
            this.Title = $"Окно программы — Формула 4 (результат = {result:F4})";
        }

        // ---------- Формула 5 ----------
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

        // ---------- Парсер ----------
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