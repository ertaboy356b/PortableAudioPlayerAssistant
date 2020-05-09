using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MessageBox.Avalonia;
using PortableAudioPlayerAssistant.Services;
using PortableAudioPlayerAssistant.StorageManager.Services;

namespace PortableAudioPlayerAssistant.Context
{
    public class ShellWindowView : Window
    {
        public ShellWindowView()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            AppSession.ShellWindow = this;

            DataContext = IOC.Resolve<ShellWindowViewModel>();

            if (IOC.Resolve<StorageManagerService>().SelectedDrive == null)
            {
                MessageBoxManager.GetMessageBoxStandardWindow("Startup Error", "No Drives Detected. Plug your device then restart the application.").ShowDialog(this).ConfigureAwait(false);
            }

            var titleBar = this.FindControl<Grid>("TitleBar");
            titleBar.PointerPressed += TitleBar_PointerPressed;
            titleBar.DoubleTapped += TitleBar_DoubleTapped;
        }

        private void TitleBar_DoubleTapped(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        private void TitleBar_PointerPressed(object sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            BeginMoveDrag(e);

            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
