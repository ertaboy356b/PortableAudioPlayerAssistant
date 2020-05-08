using Newtonsoft.Json;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PortableAudioPlayerAssistant.Services.StorageManager
{
    [JsonObject]
    public class StorageConfiguration : ReactiveObject
    {
        private DriveInfo _driveInfo;
        private string _playlistDirectory;
        private string _musicDirectory;
        private string _name;

        public StorageConfiguration(DriveInfo driveInfo)
        {
            _driveInfo = driveInfo;
        }

        public delegate void MusicDirectoryChangedEventHandler(string newValue);
        public delegate void PlaylistDirectoryChangedEventHandler(string newValue);

        public event MusicDirectoryChangedEventHandler OnMusicDirectoryChanged;
        public event PlaylistDirectoryChangedEventHandler OnPlaylistDirectoryChanged;


        [JsonProperty]
        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        [JsonProperty]
        public string MusicDirectory
        {
            get => _musicDirectory;
            set
            {
                this.RaiseAndSetIfChanged(ref _musicDirectory, value);
                OnMusicDirectoryChanged?.Invoke(value);
            }
        }

        [JsonProperty]
        public string PlaylistDirectory
        {
            get => _playlistDirectory;
            set
            {
                this.RaiseAndSetIfChanged(ref _playlistDirectory, value);
                OnPlaylistDirectoryChanged?.Invoke(value);
            }
        }

        [JsonIgnore]
        public string RootDirectory
        {
            get
            {
                return _driveInfo?.RootDirectory.FullName;
            }
        }

        [JsonIgnore]
        public string MusicDirectoryPath
        {
            get
            {
                return Path.Combine(RootDirectory, MusicDirectory ?? string.Empty);
            }
        }

        [JsonIgnore]
        public string PlaylistDirectoryPath
        {
            get
            {
                return Path.Combine(RootDirectory, PlaylistDirectory ?? string.Empty);
            }
        }

        public void Save()
        {
            try
            {
                if (_driveInfo.IsReady)
                {
                    File.WriteAllText(Path.Combine(_driveInfo.RootDirectory.FullName, "conf.papa"), this.Serialize());
                }
            } catch { }
        }
        
        public void SetDriveInfo(DriveInfo driveInfo)
        {
            _driveInfo = driveInfo;
        }

    }
}
