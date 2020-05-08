using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

namespace PortableAudioPlayerAssistant.Context.Library
{
    public class MediaLibraryView : ReactiveUserControl<MediaLibraryViewModel>
    {
        public MediaLibraryView()
        {
            this.InitializeComponent();

            DataContext = IOC.Resolve<MediaLibraryViewModel>();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
