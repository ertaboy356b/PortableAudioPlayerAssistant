using LibVLCSharp.Shared;
using PortableAudioPlayerAssistant.Services.StorageManager.Model;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortableAudioPlayerAssistant.Services.MediaPlayer
{
    public class AudioPlayerService : ReactiveObject, IDisposable
    {
        private LibVLC _libVLC;
        private LibVLCSharp.Shared.MediaPlayer _mediaPlayer;
        private SongModel _nowPlaying;
        private long _position;
        private long _length;

        public AudioPlayerService()
        {
            LibVLCSharp.Shared.Core.Initialize();

            _libVLC = new LibVLC();
            _mediaPlayer = new LibVLCSharp.Shared.MediaPlayer(_libVLC);

            _mediaPlayer.Playing += _mediaPlayer_Playing;
            _mediaPlayer.Paused += _mediaPlayer_Paused;
            _mediaPlayer.Stopped += _mediaPlayer_Stopped;
            _mediaPlayer.TimeChanged += _mediaPlayer_TimeChanged;
        }

        private void _mediaPlayer_TimeChanged(object sender, MediaPlayerTimeChangedEventArgs e)
        {
            Position = e.Time;
            Length = _mediaPlayer.Length;

            this.RaisePropertyChanged("Remaining");
        }

        public SongModel NowPlaying
        {
            get => _nowPlaying;
            set => this.RaiseAndSetIfChanged(ref _nowPlaying, value);
        }

        public bool IsPlaying
        {
            get
            {
                return _mediaPlayer.IsPlaying;
            }
        }

        public long Position
        {
            get => _position;
            set => this.RaiseAndSetIfChanged(ref _position, value);
        }

        public long Length
        {
            get => _length;
            set => this.RaiseAndSetIfChanged(ref _length, value);
        }

        public long Remaining
        {
            get
            {
                return Length - Position;
            }
        }

        public void PlaySong(SongModel song)
        {
            if (_mediaPlayer.Media != null)
            {
                _mediaPlayer.Media?.Dispose();
                _mediaPlayer.Media = null;
            }

            _mediaPlayer.Media = new Media(_libVLC, song.FilePath);
            _mediaPlayer.Media.AddOption(":no-video");
            _mediaPlayer.Play();

            NowPlaying = song;
        }

        public void Play()
        {
            if (_mediaPlayer.Media == null) return;

            _mediaPlayer.Play();
        }

        public void Stop()
        {
            _mediaPlayer.Stop();

            _mediaPlayer.Media?.Dispose();
            _mediaPlayer.Media = null;

            NowPlaying = null;
        }

        public void Pause()
        {
            _mediaPlayer.Pause();

            this.RaisePropertyChanged("IsPlaying");
        }

        public void Dispose()
        {
            _mediaPlayer.Dispose();
            _libVLC.Dispose();
        }

        private void _mediaPlayer_Stopped(object sender, EventArgs e)
        {
            this.RaisePropertyChanged("IsPlaying");
        }

        private void _mediaPlayer_Paused(object sender, EventArgs e)
        {
            this.RaisePropertyChanged("IsPlaying");
        }

        private void _mediaPlayer_Playing(object sender, EventArgs e)
        {
            this.RaisePropertyChanged("IsPlaying");
        }
    }
}
