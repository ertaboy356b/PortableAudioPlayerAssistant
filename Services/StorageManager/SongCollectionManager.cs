using PortableAudioPlayerAssistant.Services.StorageManager.Model;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PortableAudioPlayerAssistant.Services.StorageManager
{
    public class SongCollectionManager : ReactiveObject, IDisposable
    {
        private readonly StorageConfiguration _storageConfiguration;
        private readonly string[] _searchExtensions = new string[] { ".mp3", ".m4a", ".flac" };
        private readonly string[] _playlistExtensions = new string[] { ".m3u8" };
        private PlaylistModel _selectedPlaylist;

        public SongCollectionManager(StorageConfiguration storageConfiguration)
        {
            _storageConfiguration = storageConfiguration;

            storageConfiguration.OnMusicDirectoryChanged += StorageConfiguration_OnMusicDirectoryChanged;
            storageConfiguration.OnPlaylistDirectoryChanged += StorageConfiguration_OnPlaylistDirectoryChanged;

            RetrieveSongs();
            RetrievePlaylists();

            SelectedPlaylist = Playlists.FirstOrDefault();
        }

        public ObservableCollection<SongModel> Songs { get; set; } = new ObservableCollection<SongModel>();

        public ObservableCollection<PlaylistModel> Playlists { get; set; } = new ObservableCollection<PlaylistModel>();

        public PlaylistModel SelectedPlaylist
        {
            get => _selectedPlaylist;
            set => this.RaiseAndSetIfChanged(ref _selectedPlaylist, value);
        }

        public void RetrieveSongs()
        {
            var musicPath = _storageConfiguration.RootDirectory;
            if (_storageConfiguration.MusicDirectory != null)
            {
                musicPath = Path.Combine(musicPath, _storageConfiguration.MusicDirectory);
            }

            var options = new EnumerationOptions()
            {
                RecurseSubdirectories = true,
                IgnoreInaccessible = true,
            };

            foreach (var ext in _searchExtensions)
            {
                var files = Directory.EnumerateFiles(musicPath, $"*{ext}", options);

                foreach (var file in files)
                {
                    Songs.Add(new SongModel(file));
                }
            }
        }

        public void RetrievePlaylists()
        {
            var playlistPath = _storageConfiguration.RootDirectory;
            if (_storageConfiguration.PlaylistDirectory != null)
            {
                playlistPath = Path.Combine(playlistPath, _storageConfiguration.PlaylistDirectory);
            }

            var options = new EnumerationOptions()
            {
                RecurseSubdirectories = true,
                IgnoreInaccessible = true,
            };

            foreach (var ext in _playlistExtensions)
            {
                var files = Directory.EnumerateFiles(playlistPath, $"*{ext}", options);

                foreach (var file in files)
                {
                    var pl = new PlaylistModel(_storageConfiguration, file, Songs);

                    Playlists.Add(pl);
                }
            }
        }

        public PlaylistModel NewPlaylist()
        {
            var loop = 0;

            while (true)
            {
                try
                {
                    var playlistPath = Path.Combine(_storageConfiguration.PlaylistDirectoryPath, $"New Playlist {(loop == 0 ? string.Empty : loop.ToString())}".Trim() + ".m3u8");
                    if (!File.Exists(playlistPath))
                    {
                        File.CreateText(playlistPath).Dispose();

                        var newPlaylist = new PlaylistModel(_storageConfiguration, playlistPath, Songs);

                        Playlists.Add(newPlaylist);

                        return newPlaylist;
                    }
                }
                catch { }
                loop++;
            }
        }

        private void StorageConfiguration_OnPlaylistDirectoryChanged(string newValue)
        {
            
        }

        private void StorageConfiguration_OnMusicDirectoryChanged(string newValue)
        {
            
        }

        public void Dispose()
        {
            _storageConfiguration.OnMusicDirectoryChanged -= StorageConfiguration_OnMusicDirectoryChanged;
            _storageConfiguration.OnPlaylistDirectoryChanged -= StorageConfiguration_OnPlaylistDirectoryChanged;
        }
    }
}
