using PortableAudioPlayerAssistant.Services.MediaPlayer;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortableAudioPlayerAssistant.Context.Player
{
    public class PlayerViewModel : ReactiveObject
    {
        public PlayerViewModel(AudioPlayerService audioPlayer)
        {
            AudioPlayer = audioPlayer;
        }

        public AudioPlayerService AudioPlayer { get; private set; }
    }
}
