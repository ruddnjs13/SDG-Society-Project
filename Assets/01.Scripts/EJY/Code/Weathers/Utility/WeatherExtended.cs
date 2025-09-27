namespace Code.Weathers.Utility
{
    public static class WeatherExtended
    {
        /// <summary>
        /// 발전기가 작동 가능한 날씨인지 확인하는 함수
        /// </summary>
        /// <param name="data">현재 날씨의 데이터</param>
        /// <param name="powerStationData">발전기가 방해받는 날씨의 데이터</param>
        /// <returns>현재 날씨가 발전기의 작동을 방해한다면 false, 아니면 true를 리턴한다.</returns>
        public static bool CanWorkByWeather(this SendEnvironmentData data, SendEnvironmentData powerStationData)
        {
            // 현재 날씨가 포함되어 있다 == 0이 아님, 날씨가 포함되어 있지 않다면 0임
            return (data.TypeBit & powerStationData.TypeBit) != 0;
        }
    }
}