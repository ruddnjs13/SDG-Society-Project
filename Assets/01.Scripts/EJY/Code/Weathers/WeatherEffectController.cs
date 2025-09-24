using System;
using System.Collections.Generic;
using Code.Events;
using Core.GameEvent;
using UnityEngine;

namespace Code.Weathers
{
    public class WeatherEffectController : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO environmentChannel;
        private Dictionary<WeatherType, WeatherEffect> _weatherParticles = new Dictionary<WeatherType, WeatherEffect>();
        private WeatherEffect _currentWeatherEffect;
        
        private void Awake()
        {
            foreach (var weatherParticle in GetComponentsInChildren<WeatherEffect>())
            {
                _weatherParticles.Add(weatherParticle.WeatherType, weatherParticle);
            }
            
            environmentChannel.AddListener<WeatherChangeEvent>(HandleWeatherChange);
        }

        private void HandleWeatherChange(WeatherChangeEvent evt)
        {
            _currentWeatherEffect?.StopEffect();
            
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
            
            _currentWeatherEffect = _weatherParticles.GetValueOrDefault(type);
            Debug.Assert(_currentWeatherEffect != null, $"Weather particle is null,type is {type}");
            
            _currentWeatherEffect.PlayEffect();
        }
    }
}