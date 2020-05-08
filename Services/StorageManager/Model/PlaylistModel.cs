using ReactiveUI;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace PortableAudioPlayerAssistant.Services.StorageManager.Model
{
    public class PlaylistModel : ReactiveObject
    {
        StorageConfiguration _storageConfiguration;
        private string _name;

        public PlaylistModel()
        {

        }

        public PlaylistModel(StorageConfiguration storageConfiguration, string file, IList<SongModel> songLibrary)
        {
            _storageConfiguration = storageConfiguration;

            Name = Path.GetFileNameWithoutExtension(file);
            FilePath = file;

            var lines = File.ReadAllLines(file);
            var basePath = _storageConfiguration.RootDirectory;
            if (_storageConfiguration.PlaylistDirectory != null)
            {
                basePath = Path.Combine(basePath, _storageConfiguration.PlaylistDirectory);
            }
            foreach(var line in lines)
            {
                if (line.StartsWith("#EXTM3U") || line.StartsWith("#EXTINF") || line.StartsWith("#EXT")) continue;

                var filePath = Path.Combine(basePath, line);

                var song = songLibrary.FirstOrDefault(x => Path.GetFullPath(x.FilePath) == Path.GetFullPath(filePath));
                if (song != null) Songs.Add(song);
            }
        }

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }
        public string FilePath { get; set; }
        public ObservableCollection<SongModel> Songs { get; set; } = new ObservableCollection<SongModel>();

        public void Save()
        {
            var sb = new StringBuilder();

            sb.AppendLine("#EXTM3U");

            foreach(var song in Songs)
            {
                var songPath = song.FilePath.Remove(0, Path.Combine(_storageConfiguration.RootDirectory, _storageConfiguration.PlaylistDirectory ?? string.Empty).Length).TrimStart('\\', '/');

                sb.AppendLine(songPath);
            }

            var fileName = Path.GetFileNameWithoutExtension(FilePath);
            if (fileName != Name)
            {
                try
                {
                    File.Delete(FilePath);
                } catch { }

                FilePath = Path.Combine(_storageConfiguration.RootDirectory, _storageConfiguration.PlaylistDirectory, Name + ".m3u8");
            }


            File.WriteAllText(FilePath, sb.ToString());
        }

        public void Delete()
        {
            try
            {
                File.Delete(FilePath);
            }
            catch { }
        }
    }
}
