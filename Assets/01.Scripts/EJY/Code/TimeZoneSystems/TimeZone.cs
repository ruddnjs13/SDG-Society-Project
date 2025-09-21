using System;
using Code.Events;
using Code.Weathers;
using Core.GameEvent;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.TimeZones
{
    public class TimeZone : MonoBehaviour
    {
        [SerializeField] private float perDaySeconds = 120f;
        [SerializeField] private GameEventChannelSO environmentChannel;

        private float _currentTime = 0;
        private float _halfDaySeconds;
        private NotifyValue<bool> _isMorning = new NotifyValue<bool>(true);

        private void Awake()
        {
            _halfDaySeconds = perDaySeconds / 2;
            _isMorning.OnValueChanged += HandleValueChanged;
        }

        private void HandleValueChanged(bool prev, bool next)
        {
            environmentChannel.RaiseEvent(EnvironmentEvents.TimeZoneChangeEvent.Init(next ? WeatherType.Morning : WeatherType.Night));
        }

        private void OnDestroy()
        {
            _isMorning.OnValueChanged -= HandleValueChanged;
        }

        private void Update()
        {
            _currentTime += Time.deltaTime;
            if(_currentTime >= perDaySeconds)
                _currentTime = 0;
            _isMorning.Value = _currentTime <= _halfDaySeconds;
        }
    }
}