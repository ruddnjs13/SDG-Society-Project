using System;

namespace Code.Weathers
{
    [Flags]
    public enum WeatherType
    {
        None = 0,
        ///<summary>평범한 날씨</summary>
        Normal = 1,
        ///<summary>맑은 날씨</summary>
        Clear = 2,
        ///<summary>비오는 날씨</summary>
        Rainy = 4,
        ///<summary>바람부는 날씨</summary>
        Windy = 8,
        ///<summary>흐린 날씨</summary>
        Cloudy = 16,
        
        Max
    }
    
    public enum TimeZoneType
    {
        None = 0,
        ///<summary>아침</summary>
        Morning = 32,
        ///<summary>밤</summary>
        Night = 64,
    }

    public struct SendEnvironmentData
    {
        public int TypeBit;
    }
}