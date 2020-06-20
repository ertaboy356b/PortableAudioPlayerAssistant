using DynamicData;
using MessageBox.Avalonia;
using PortableAudioPlayerAssistant.Context.Dialogs;
using PortableAudioPlayerAssistant.Services.MediaPlayer;
using PortableAudioPlayerAssistant.Services.StorageManager.Model;
using PortableAudioPlayerAssistant.StorageManager.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PortableAudioPlayerAssistant.Context.Library
{
    public class MediaPlaylistViewModel : ReactiveObject
    {
        private AudioPlayerService _audioPlayer;

        public MediaPlaylistViewModel(StorageManagerService storageManager, AudioPlayerService audioPlayer)
        {
            StorageManager = storageManager;
            _audioPlayer = audioPlayer;
        }

        public StorageManagerService StorageManager { get; private set; }

        public SongModel SelectedSong { get; set; }

        public PlaylistModel SelectedPlaylist
        {
            get => StorageManager.SelectedDrive?.SongsManager?.SelectedPlaylist;
            set
            {
                StorageManager.SelectedDrive.SongsManager.SelectedPlaylist = value;
            }
        }

        public void PlaySelectedSong()
        {
            if (SelectedSong == null) return;

            _audioPlayer.PlaySong(SelectedSong);

            //if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            //{
            //    Process.Start("explorer", SelectedSong.FilePath);
            //}
            //else
            //{
            //    Console.WriteLine("Platform not supported.");
            //}
        }

        public void Rename()
        {
            if (SelectedPlaylist == null) return;

            var dialog = new RenamePlaylistView();
            dialog.ShowDialog(SelectedPlaylist);
        }

        public void New()
        {
            var newPlaylist = StorageManager.SelectedDrive.SongsManager.NewPlaylist();

            SelectedPlaylist = newPlaylist;
        }

        public async Task DeleteAsync()
        {
            var playlist = SelectedPlaylist;

            if (playlist != null)
            {
                var result = await MessageBoxManager.GetMessageBoxStandardWindow("Delete Playlist", $"Delete '{playlist.Name}'?",
                    MessageBox.Avalonia.Enums.ButtonEnum.YesNo, MessageBox.Avalonia.Enums.Icon.Warning).ShowDialog(AppSession.ShellWindow);

                if (result == MessageBox.Avalonia.Enums.ButtonResult.Yes)
                {
                    StorageManager.SelectedDrive.SongsManager.Playlists.Remove(playlist);
                    playlist.Delete();

                    SelectedPlaylist = StorageManager.SelectedDrive.SongsManager.Playlists.FirstOrDefault();
                }
            }
        }

        public void RemoveSelectedSong()
        {
            if (SelectedSong == null) return;
            if (SelectedPlaylist == null) return;

            var playlist = SelectedPlaylist;

            var index = playlist.Songs.IndexOf(SelectedSong);

            playlist.Songs.Remove(SelectedSong);

            try
            {
                SelectedSong = playlist.Songs[index];
            } catch
            {
                SelectedSong = playlist.Songs.FirstOrDefault();
            }
        }

        public void ShuffleSongs()
        {
            var playList = SelectedPlaylist;
            var shuffleData = playList.Songs.Shuffle().ToList();

            playList.Songs.Clear();
            playList.Songs.AddRange(shuffleData);
            playList.Save();
        }
    }
}
