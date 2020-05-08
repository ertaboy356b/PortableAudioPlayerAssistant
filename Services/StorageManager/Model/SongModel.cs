using Avalonia.Markup.Xaml.Converters;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using TagLib.Image;

namespace PortableAudioPlayerAssistant.Services.StorageManager.Model
{
    public class SongModel
    {
        public SongModel(string file)
        {
            FileName = Path.GetFileName(file);
            FilePath = file;

            using (var tags = TagLib.File.Create(file))
            {

                Title = tags.Tag.Title;
                Artist = tags.Tag.FirstPerformer;
                AlbumArtist = tags.Tag.FirstAlbumArtist;
                Album = tags.Tag.Album;
                Track = Convert.ToInt32(tags.Tag.Track);
                TrackCount = Convert.ToInt32(tags.Tag.TrackCount);
                Year = Convert.ToInt32(tags.Tag.Year);

                var imageBuffer = tags.Tag.Pictures?.FirstOrDefault()?.Data.Data;
                CreateAndSetImage(imageBuffer);
            }

            if (string.IsNullOrWhiteSpace(Title)) Title = FileName;
        }

        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string AlbumArtist { get; set; }
        public string Album { get; set; }
        public int Track { get; set; }
        public int TrackCount { get; set; }
        public int Year { get; set; }
        public string ImagePath { get; set; }

        void CreateAndSetImage(byte[] imageBuffer)
        {
            if (imageBuffer == null) return;

            var md5 = Convert.ToBase64String(MD5.Create().ComputeHash(imageBuffer));
            var invalidChars = System.IO.Path.GetInvalidFileNameChars();

            foreach (var c in invalidChars)
            {
                md5 = md5.Replace(c, '-');
            }

            var path = Path.Combine("thumbnail", md5 + ".image");

            if (!Directory.Exists("thumbnail"))
            {
                Directory.CreateDirectory("thumbnail");
            }

            if (!System.IO.File.Exists(path))
            {
                using (var ms = new MemoryStream(imageBuffer))
                {
                    var bitmap = System.Drawing.Bitmap.FromStream(ms);
                    var thumb = bitmap.GetThumbnailImage(64, 64, null, IntPtr.Zero);
                    thumb.Save(path);
                }
            }

            ImagePath = path;
        }
    }
}
