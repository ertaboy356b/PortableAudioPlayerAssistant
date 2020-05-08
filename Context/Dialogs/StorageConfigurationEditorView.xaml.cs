using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using PortableAudioPlayerAssistant.Services.StorageManager;

namespace PortableAudioPlayerAssistant.Context.Dialogs
{
    public class StorageConfigurationEditorView : Window
    {
        public StorageConfigurationEditorView()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            DataContext = IOC.Resolve<StorageConfigurationEditorViewModel>();
        }

        public void ShowDialog(StorageConfiguration storageConfiguration)
        {
            (DataContext as StorageConfigurationEditorViewModel).SetConfiguration(storageConfiguration);
            base.ShowDialog(AppSession.ShellWindow);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
