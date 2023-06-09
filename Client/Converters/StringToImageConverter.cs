﻿using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Client.Converters;

public class StringToImageConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string imagePath)
            if (!string.IsNullOrEmpty(imagePath))
            {
                var imageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                if (!File.Exists(imagePath)) return null;
                using var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);

                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
                image.Freeze();
                return image;
            }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}