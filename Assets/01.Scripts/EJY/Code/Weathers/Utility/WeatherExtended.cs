namespace Code.Weathers.Utility
{
    public static class WeatherExtended
    {
        public static bool CanActionByWeather(this WeatherDataSO data, WeatherType powerStationWeatherType)
        {
            return data.weatherType == powerStationWeatherType;
        }
    }
}