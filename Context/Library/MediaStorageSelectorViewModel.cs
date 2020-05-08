using Avalonia.Controls;
using PortableAudioPlayerAssistant.Services.StorageManager.Model;
using PortableAudioPlayerAssistant.StorageManager.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PortableAudioPlayerAssistant.Context.Library
{
    public class MediaStorageSelectorViewModel : ReactiveObject
    {
        private ListBoxItem _selectedDriveDummy;
        private PlaylistModel _selectedPlaylistDummy;

        public MediaStorageSelectorViewModel(StorageManagerService storageManagerService)
        {
            StorageManager = storageManagerService;
        }

        public StorageManagerService StorageManager { get; private set; }

        public Avalonia.Controls.ListBoxItem SelectedDriveDummy
        {
            get => _selectedDriveDummy;
            set
            {
                if (value == null) return;
                this.RaiseAndSetIfChanged(ref _selectedDriveDummy, value);
            }
        }

        public PlaylistModel SelectedPlaylistDummy
        {
            get => _selectedPlaylistDummy;
            set
            {
                if (value == null) return;
                this.RaiseAndSetIfChanged(ref _selectedPlaylistDummy, value);
            }
        }
    }
}
