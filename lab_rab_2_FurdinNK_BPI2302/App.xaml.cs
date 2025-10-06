using System.Windows;

namespace lab_rab_2_FurdinNK_BPI2302
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.DispatcherUnhandledException += (sender, args) =>
            {
                MessageBox.Show($"Необработанное исключение: {args.Exception.Message}",
                              "Ошибка приложения",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
                args.Handled = true;
            };
        }
    }
}