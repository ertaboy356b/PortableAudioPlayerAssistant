using PortableAudioPlayerAssistant.Services.StorageManager.Model;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortableAudioPlayerAssistant.Context.Dialogs
{
    public class RenamePlaylistViewModel : ReactiveObject
    {
        private PlaylistModel _playlist;
        private string _name;
        private bool _closed;

        public RenamePlaylistViewModel()
        {

        }

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public bool Closed
        {
            get => _closed;
            set => this.RaiseAndSetIfChanged(ref _closed, value);
        }

        public void SetPlaylist(PlaylistModel playlist)
        {
            _playlist = playlist;
            Name = playlist.Name;
        }

        public void Save()
        {
            _playlist.Name = Name;
            _playlist.Save();

            Closed = true;
        }

        public void Cancel()
        {
            Closed = true;
        }
    }
}
