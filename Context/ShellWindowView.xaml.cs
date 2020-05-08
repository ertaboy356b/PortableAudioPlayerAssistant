using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using PortableAudioPlayerAssistant.Services;

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
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
