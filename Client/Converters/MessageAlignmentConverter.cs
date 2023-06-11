﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Client.Stores;

namespace Client.Converters;

public class MessageAlignmentConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values != null && values.Length == 2 &&
            values[0] is Guid receiver &&
            values[1] is UserStore user)
            if (receiver == user.User.Id)
                return HorizontalAlignment.Left;

        return HorizontalAlignment.Right;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}