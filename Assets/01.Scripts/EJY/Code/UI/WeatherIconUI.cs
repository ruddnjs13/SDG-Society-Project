using System;
using Code.Events;
using Core.GameEvent;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class WeatherIconUI : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO environmentChannel;
        [SerializeField] private Image weatherIcon;

        private void Awake()
        {
            environmentChannel.AddListener<EnvironmentChangeEvent>(HandleEnvironmentChange);
        }

        private void HandleEnvironmentChange(EnvironmentChangeEvent evt)
        {
            weatherIcon.sprite = evt.icon;
        }
    }
}