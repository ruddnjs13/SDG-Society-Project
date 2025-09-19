using Code.Events;
using Core.GameEvent;
using UnityEngine;

namespace Code.Weathers
{
    public class Weather : MonoBehaviour
    {
        [SerializeField] private WeatherDataSO[] weatherData;
        [SerializeField] private GameEventChannelSO weatherChannel;
        
        public WeatherType CurrentWeather { get; private set; } = WeatherType.Normal;

        private void ChangeWeather()
        {
            WeatherDataSO data = weatherData[Random.Range(0, weatherData.Length)];
            
            weatherChannel.RaiseEvent(WeatherEvents.WeatherChangeEvent.Init(data));
        }
    }
}
