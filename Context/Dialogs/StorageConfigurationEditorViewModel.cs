using Avalonia.Controls;
using MessageBox.Avalonia;
using PortableAudioPlayerAssistant.Services.StorageManager;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PortableAudioPlayerAssistant.Context.Dialogs
{
    public class StorageConfigurationEditorViewModel : ReactiveObject
    {
        private StorageConfiguration _configuration;
        private string _musicDirectory;
        private string _playlistDirectory;
        private string _name;
        private bool _closed;

        public StorageConfigurationEditorViewModel()
        {

        }

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public string MusicDirectory
        {
            get => _musicDirectory;
            set => this.RaiseAndSetIfChanged(ref _musicDirectory, value);
        }

        public string PlaylistDirectory
        {
            get => _playlistDirectory;
            set => this.RaiseAndSetIfChanged(ref _playlistDirectory, value);
        }

        public StorageConfiguration Configuration
        {
            get => _configuration;
            set => this.RaiseAndSetIfChanged(ref _configuration, value);
        }

        public bool Closed
        {
            get => _closed;
            set => this.RaiseAndSetIfChanged(ref _closed, value);
        }

        public void SetConfiguration(StorageConfiguration storageConfiguration)
        {
            Configuration = storageConfiguration;

            Name = Configuration.Name;
            MusicDirectory = Configuration.MusicDirectory;
            PlaylistDirectory = Configuration.PlaylistDirectory;
        }

        public async Task LocateMusicDirectoryAsync()
        {
            var defaultDirectory = Configuration.RootDirectory;
            if (MusicDirectory != null)
            {
                defaultDirectory = Path.Combine(defaultDirectory, MusicDirectory);
            }

            var dialog = new OpenFolderDialog()
            {
                Directory = defaultDirectory
            };

            var result = await dialog.ShowAsync(AppSession.ShellWindow);

            if (result == null) return;

            if (result.Substring(0, 1)?.ToUpper() != Configuration.RootDirectory.ToUpper().Substring(0, 1))
            {
                await MessageBoxManager.GetMessageBoxStandardWindow("Locate Music Directory", "Cannot select folders outside of drive.",
                    MessageBox.Avalonia.Enums.ButtonEnum.Ok, MessageBox.Avalonia.Enums.Icon.Forbidden).ShowDialog(AppSession.ShellWindow);

                return;
            }

            MusicDirectory = result.Substring(3);
        }

        public async Task LocatePlaylistDirectoryAsync()
        {
            var defaultDirectory = Configuration.RootDirectory;
            if (PlaylistDirectory != null)
            {
                defaultDirectory = Path.Combine(defaultDirectory, PlaylistDirectory);
            }

            var dialog = new OpenFolderDialog()
            {
                Directory = defaultDirectory
            };

            var result = await dialog.ShowAsync(AppSession.ShellWindow);

            if (result == null) return;

            if (result.Substring(0, 1)?.ToUpper() != Configuration.RootDirectory.ToUpper().Substring(0, 1))
            {
                await MessageBoxManager.GetMessageBoxStandardWindow("Locate Playlist Directory", "Cannot select folders outside of drive.",
                    MessageBox.Avalonia.Enums.ButtonEnum.Ok, MessageBox.Avalonia.Enums.Icon.Forbidden).ShowDialog(AppSession.ShellWindow);

                return;
            }

            PlaylistDirectory = result.Substring(3);
        }

        public void Save()
        {
            Configuration.MusicDirectory = MusicDirectory;
            Configuration.PlaylistDirectory = PlaylistDirectory;
            Configuration.Name = Name;

            Configuration.Save();

            Closed = true;
        }

        public void Cancel()
        {
            Closed = true;
        }
    }
}
