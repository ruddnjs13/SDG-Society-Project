namespace Code.Weathers.Utility
{
    public static class WeatherExtended
    {
        public static bool CanWorkByWeather(this WeatherDataSO data, WeatherType disturbanceWeatherType)
        {
            return data.weatherType != disturbanceWeatherType;
        }
    }
}