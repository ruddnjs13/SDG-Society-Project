using System;
using Code.Events;
using Core.GameEvent;
using UnityEngine;

namespace Code.Weathers
{
    public class Tester : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO environmentChannel;

        private void Awake()
        {
            environmentChannel.AddListener<WeatherChangeEvent>(HandleWeatherChange);
        }

        private void HandleWeatherChange(WeatherChangeEvent evt)
        {
            Debug.Log(evt.data.Type);
        }
    }
}