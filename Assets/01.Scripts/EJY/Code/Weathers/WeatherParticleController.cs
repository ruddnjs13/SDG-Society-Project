using System;
using System.Collections.Generic;
using Code.Events;
using Core.GameEvent;
using UnityEngine;

namespace Code.Weathers
{
    public class WeatherParticleController : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO environmentChannel;
        private Dictionary<WeatherType, WeatherParticle> _weatherParticles = new Dictionary<WeatherType, WeatherParticle>();
        private WeatherParticle _currentWeatherParticle;
        
        private void Awake()
        {
            foreach (var weatherParticle in GetComponentsInChildren<WeatherParticle>())
            {
                _weatherParticles.Add(weatherParticle.WeatherType, weatherParticle);
            }
            
            environmentChannel.AddListener<WeatherChangeEvent>(HandleWeatherChange);
        }

        private void HandleWeatherChange(WeatherChangeEvent evt)
        {
            _currentWeatherParticle?.StopParticle();
            
            WeatherType type = WeatherType.None;
            foreach (WeatherType flag in Enum.GetValues(typeof(WeatherType)))
            {
                if (((WeatherType)evt.data.TypeBit & flag) == flag && flag != WeatherType.None)
                {
                    type = flag;
                    break;
                }
            }
            
            if(type == WeatherType.None) return;
            
            _currentWeatherParticle = _weatherParticles.GetValueOrDefault(type);
            Debug.Assert(_currentWeatherParticle != null, $"Weather particle is null,type is {type}");
            
            _currentWeatherParticle.PlayParticle();
        }
    }
}