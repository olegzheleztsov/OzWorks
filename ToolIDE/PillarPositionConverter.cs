#region

using System;
using System.Globalization;
using System.Windows.Data;

#endregion

namespace ToolIDE
{
    public class PillarPositionConverter : IValueConverter
    {
        public int PillarOrder { get; set; } = 0;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var width = (double) value;
            var w3 = width / 3;
            return PillarOrder switch
            {
                0 => w3 / 2,
                1 => w3 + w3 / 2,
                2 => 2 * w3 + w3 / 2,
                _ => throw new InvalidOperationException($"Invalid value of {nameof(PillarOrder)} = {PillarOrder}")
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}