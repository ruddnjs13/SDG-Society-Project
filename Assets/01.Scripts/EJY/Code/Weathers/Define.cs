namespace Code.Weathers
{
    public enum WeatherType
    {
        None = -1,
        
        ///<summary>평범한 날씨</summary>
        Normal = 0,
        ///<summary>밝은 날씨</summary>
        Sunny = 1,
        ///<summary>비오는 날씨</summary>
        Rainy = 2,
        ///<summary>바람부는 날씨</summary>
        Windy = 3,
        ///<summary>눈오는 날씨</summary>
        Snowy = 4,
        ///<summary>흐린 날씨</summary>
        Cloudy = 5,
        
        Max
    }
}