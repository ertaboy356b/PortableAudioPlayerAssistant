using MessageBox.Avalonia;
using PortableAudioPlayerAssistant.Services.StorageManager;
using PortableAudioPlayerAssistant.Services.StorageManager.Model;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace PortableAudioPlayerAssistant.StorageManager.Services
{
    public class StorageManagerService : ReactiveObject
    {
        private DriveModel _selectedLibrary;

        public StorageManagerService()
        {
            var drives = GetPortableDrives();
            Drives.AddRange(drives);

            SelectedDrive = Drives.FirstOrDefault();
        }

        public ObservableCollection<DriveModel> Drives { get; } = new ObservableCollection<DriveModel>();

        public DriveModel SelectedDrive
        {
            get => _selectedLibrary;
            set => this.RaiseAndSetIfChanged(ref _selectedLibrary, value);
        }

        public IEnumerable<DriveModel> GetPortableDrives()
        {
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            foreach (System.IO.DriveInfo drive in drives)
            {
                if (drive.DriveType == DriveType.Removable && drive.IsReady)
                {
                    yield return new DriveModel(drive);
                }
            }
        }
    }
}
