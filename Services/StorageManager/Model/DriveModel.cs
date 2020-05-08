using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PortableAudioPlayerAssistant.Services.StorageManager.Model
{
    public class DriveModel : ReactiveObject, IDisposable
    {
        private DriveInfo _driveInfo;
        private DriveModel _test;

        public DriveModel(DriveInfo driveInfo)
        {
            _driveInfo = driveInfo;

            Configuration = GetStorageConfiguration();
            SongsManager = new SongCollectionManager(Configuration);
        }

        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(Configuration.Name))
                {
                    return $"{_driveInfo.VolumeLabel} ({_driveInfo.Name})";
                } else
                {
                    return $"{Configuration.Name} ({_driveInfo.Name})";
                }
            }
        }

        public StorageConfiguration Configuration { get; set; }
        public SongCollectionManager SongsManager { get; set; }

        StorageConfiguration GetStorageConfiguration()
        {
            var files = _driveInfo.RootDirectory.GetFiles("conf.papa");
            if (files.Length > 0)
            {
                var jsonConf = File.ReadAllText(files[0].FullName);

                var st = jsonConf.Deserialize<StorageConfiguration>();
                st.SetDriveInfo(_driveInfo);

                return st;
            } else
            {
                var st = new StorageConfiguration(_driveInfo);

                if (Directory.Exists(Path.Combine(_driveInfo.RootDirectory.FullName, "MUSIC")))
                {
                    st.MusicDirectory = "MUSIC";
                    st.PlaylistDirectory = "MUSIC";
                    st.Name = "NW-AXX WALKMAN";
                } else
                {
                    st.Name = "GENERIC USB STORAGE";
                }

                st.Save();

                return st;
            }

        }

        public void Dispose()
        {
            SongsManager?.Dispose();
        }
    }
}
