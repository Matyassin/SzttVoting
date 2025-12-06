using System.Globalization;

namespace ViewModel.Converter;

public class RemainingTimeConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not DateTime deadline)
            return "";

        TimeSpan remainingTime = deadline - DateTime.Now;

        if (remainingTime.TotalDays >= 1)
        {
            return $"{(int)remainingTime.TotalDays} days left";
        }
        else if (remainingTime.TotalHours >= 1)
        {
            return $"{(int)remainingTime.TotalHours} hours left";
        }
        else if (remainingTime.TotalMinutes >= 1)
        {
            return $"{(int)remainingTime.TotalMinutes} minutes left";
        }

        return $"{(int)remainingTime.TotalSeconds} seconds left";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
