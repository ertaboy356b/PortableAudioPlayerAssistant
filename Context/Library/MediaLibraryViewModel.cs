using Avalonia.ReactiveUI;
using MessageBox.Avalonia;
using PortableAudioPlayerAssistant.Context.Dialogs;
using PortableAudioPlayerAssistant.Services.StorageManager.Model;
using PortableAudioPlayerAssistant.StorageManager.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;

namespace PortableAudioPlayerAssistant.Context.Library
{
    public class MediaLibraryViewModel : ReactiveObject
    {
        public MediaLibraryViewModel(StorageManagerService storageManager)
        {
            StorageManager = storageManager;
        }

        public StorageManagerService StorageManager { get; set; }

        public SongModel SelectedSong { get; set; }

        public void AddSelectedSongToPlaylist(PlaylistModel playlist)
        {
            if (playlist == null) return;
            if (SelectedSong == null) return;

            if (SelectedSong.FilePath.Substring(0, 1) == StorageManager.SelectedDrive.Configuration.RootDirectory.Substring(0, 1))
            {
                if (!playlist.Songs.Contains(SelectedSong))
                {
                    playlist.Songs.Add(SelectedSong);
                }

                playlist.Save();
            }
            else
            {
                MessageBoxManager.GetMessageBoxStandardWindow("Playlist Error", "Cannot add song from another drive.",
                    MessageBox.Avalonia.Enums.ButtonEnum.Ok, MessageBox.Avalonia.Enums.Icon.Forbidden).ShowDialog(AppSession.ShellWindow);
            }
        }

        public void AddSelectedSongToCurrentPlaylist()
        {
            AddSelectedSongToPlaylist(StorageManager.SelectedDrive.SongsManager.SelectedPlaylist);
        }

        public void PlaySelectedSong()
        {
            if (SelectedSong == null) return;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start("explorer", SelectedSong.FilePath);
            } else
            {
                Console.WriteLine("Platform not supported.");
            }
        }

        public void ConfigureSelectedDrive()
        {
            if (StorageManager.SelectedDrive == null) return;

            new StorageConfigurationEditorView().ShowDialog(StorageManager.SelectedDrive.Configuration);
        }
    }
}
