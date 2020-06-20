using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PortableAudioPlayerAssistant.Context.Player
{
    public class PlayerView : UserControl
    {
        public PlayerView()
        {
            this.InitializeComponent();

            DataContext = IOC.Resolve<PlayerViewModel>();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
