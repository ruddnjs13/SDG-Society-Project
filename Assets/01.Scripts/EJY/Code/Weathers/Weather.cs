using System;
using System.Collections.Generic;
using Code.Events;
using Core.GameEvent;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Code.Weathers
{
    public class Weather : MonoBehaviour
    {
        public UnityEvent rainyEvent;
        public UnityEvent windyEvent;
        public UnityEvent bgmEvent;
        
        [SerializeField] private WeatherDataSO[] weatherData;
        [SerializeField] private GameEventChannelSO environmentChannel;
        [SerializeField] private float perChangeWeatherSeconds = 40f;
        
        private Dictionary<WeatherType, WeatherDataSO> _weatherDataDict = new Dictionary<WeatherType, WeatherDataSO>();

        private float _currentTime = 0;
        
        public WeatherType CurrentWeather { get; private set; } = WeatherType.Normal;
        public TimeZoneType CurrentTimeZone { get; private set; } = TimeZoneType.Morning;

        private void Awake()
        {
            foreach (var data in weatherData)
            {
                _weatherDataDict.Add(data.weatherType, data);
            }
            
            environmentChannel.AddListener<TimeZoneChangeEvent>(HandleTimeZoneChange);
        }

        private void Start()
        {
            ChangeWeather();
        }

        private void Update()
        {
            _currentTime += Time.deltaTime;

            if (_currentTime >= perChangeWeatherSeconds)
            {
                _currentTime = 0;
                ChangeWeather();
            }
        }

        private void HandleTimeZoneChange(TimeZoneChangeEvent evt)
        {
            CurrentTimeZone = evt.type;
            SendEnvironmentData data = new SendEnvironmentData{TypeBit = (int)CurrentWeather | (int)CurrentTimeZone};
            WeatherDataSO weatherData = _weatherDataDict.GetValueOrDefault(CurrentWeather);
            if(weatherData != null)
                SendEvent(data, weatherData.weatherIcon);
            else
                Debug.Log($"WeatherDataDict has not Key, Key {CurrentWeather}");
        }

        private void ChangeWeather()
        {
            WeatherDataSO randData = weatherData[Random.Range(0, weatherData.Length)];
            CurrentWeather = randData.weatherType;
            
            if(CurrentWeather == WeatherType.Rainy)
                rainyEvent?.Invoke();
            else if(CurrentWeather == WeatherType.Windy)
                windyEvent?.Invoke();
            else
                bgmEvent?.Invoke();
            
            SendEnvironmentData data = new SendEnvironmentData{TypeBit = (int)CurrentWeather | (int)CurrentTimeZone};
            SendEvent(data, randData.weatherIcon);
        }

        private void SendEvent(SendEnvironmentData data, Sprite icon)
        {
            environmentChannel.RaiseEvent(EnvironmentEvents.EnvironmentChangeEvent.Init(data, icon));
        }
    }
}
