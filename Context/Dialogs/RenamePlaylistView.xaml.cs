using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using PortableAudioPlayerAssistant.Services.StorageManager.Model;

namespace PortableAudioPlayerAssistant.Context.Dialogs
{
    public class RenamePlaylistView : Window
    {
        public RenamePlaylistView()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            DataContext = IOC.Resolve<RenamePlaylistViewModel>();
        }

        public void ShowDialog(PlaylistModel playlist)
        {
            (DataContext as RenamePlaylistViewModel).SetPlaylist(playlist);
            base.ShowDialog(AppSession.ShellWindow);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
