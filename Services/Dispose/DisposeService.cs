using PortableAudioPlayerAssistant.Services.MediaPlayer;
using PortableAudioPlayerAssistant.StorageManager.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortableAudioPlayerAssistant.Services.Dispose
{
    public class DisposeService
    {
        private StorageManagerService _storageManager;
        private AudioPlayerService _audioPlayer;

        public DisposeService(StorageManagerService storageManager, AudioPlayerService audioPlayer)
        {
            _storageManager = storageManager;
            _audioPlayer = audioPlayer;
        }

        public void Run()
        {
            _audioPlayer.Dispose();
        }
    }
}
