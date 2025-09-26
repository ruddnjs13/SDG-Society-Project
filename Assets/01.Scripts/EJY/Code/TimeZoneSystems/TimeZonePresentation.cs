using System;
using Code.Events;
using Code.Weathers;
using Core.GameEvent;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Code.TimeZones
{
    public class TimeZonePresentation : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO environmentChannel;
        [SerializeField, ColorUsage(false, true)]
        private Color morningColor, nightColor;

        [SerializeField] private Volume volume;
        [SerializeField] private float changeDuration = 1f;
        private ColorAdjustments _timeZoneColorAdjustments;
        private void Awake()
        {
            if (volume.profile.TryGet(out ColorAdjustments colorAdjustments))
            {
                _timeZoneColorAdjustments = colorAdjustments;
            }
            
            environmentChannel.AddListener<EnvironmentChangeEvent>(HandleEnvironmentChange);
        }

        private void HandleEnvironmentChange(EnvironmentChangeEvent evt)
        {
            if ((evt.data.TypeBit & (int)TimeZoneType.Morning) != 0)
            {
                Debug.Log("Current timeZone is morning");
                DoColor(morningColor, changeDuration);
            }
            else if ((evt.data.TypeBit & (int)TimeZoneType.Night) != 0)
            {
                Debug.Log("Current timeZone is night");
                DoColor(nightColor, changeDuration);
            }
        }

        private void DoColor(Color targetColor, float duration)
        {
            DOTween.To(() => _timeZoneColorAdjustments.colorFilter.value, x => _timeZoneColorAdjustments.colorFilter.value = x, targetColor, duration);
        }
    }
}