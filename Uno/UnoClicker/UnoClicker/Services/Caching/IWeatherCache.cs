using System.Collections.Immutable;
using UnoClicker.DataContracts;

namespace UnoClicker.Services.Caching
{
    public interface IWeatherCache
    {
        ValueTask<IImmutableList<WeatherForecast>> GetForecast(CancellationToken token);
    }
}