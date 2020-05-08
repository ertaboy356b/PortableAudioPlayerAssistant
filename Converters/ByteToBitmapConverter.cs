using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml.Converters;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace PortableAudioPlayerAssistant.Converters
{
    public class ByteToBitmapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            if (value is byte[])
            {
                using(var ms = new MemoryStream((byte[])value))
                {
                    return new Bitmap(ms);
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
