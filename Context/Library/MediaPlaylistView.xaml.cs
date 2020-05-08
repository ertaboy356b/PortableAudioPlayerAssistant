using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PortableAudioPlayerAssistant.Context.Library
{
    public class MediaPlaylistView : UserControl
    {
        public MediaPlaylistView()
        {
            this.InitializeComponent();

            DataContext = IOC.Resolve<MediaPlaylistViewModel>();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
