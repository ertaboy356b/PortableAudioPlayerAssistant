using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PortableAudioPlayerAssistant.Converters
{
    public class TimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is long)
            {
                return TimeSpan.FromMilliseconds((long)value).ToString(@"mm\:ss");
            }

            if (value is int)
            {
                return TimeSpan.FromMilliseconds((int)value).ToString(@"mm\:ss");
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
