using System;

namespace Code.Weathers
{
    [Flags]
    public enum WeatherType
    {
        ///<summary>평범한 날씨</summary>
        Normal = 1,
        ///<summary>밝은 날씨</summary>
        Sunny = 2,
        ///<summary>비오는 날씨</summary>
        Rainy = 4,
        ///<summary>바람부는 날씨</summary>
        Windy = 8,
        ///<summary>눈오는 날씨</summary>
        Snowy = 16,
        ///<summary>흐린 날씨</summary>
        Cloudy = 32,
        ///<summary>Morning</summary>
        Morning = 64,
        ///<summary>밤</summary>
        Night = 128,
        
        Max
    }

    public struct SendWeatherData
    {
        public WeatherType Type;
    }
}