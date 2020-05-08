using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PortableAudioPlayerAssistant.Context.Library
{
    public class MediaStorageSelectorView : UserControl
    {
        public MediaStorageSelectorView()
        {
            this.InitializeComponent();

            DataContext = IOC.Resolve<MediaStorageSelectorViewModel>();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
