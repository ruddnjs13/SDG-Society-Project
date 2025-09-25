using System;
using Code.Events;
using Code.Weathers;
using Core.GameEvent;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Code.TimeZones
{
    public class TimeZonePresentation : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO environmentChannel;
        [SerializeField, ColorUsage(false, true)]
        private Color morningColor, nightColor;
        [SerializeField] private ColorAdjustments timeZoneColor;
        [SerializeField] private float changeDuration = 1f;
        private void Awake()
        {
            environmentChannel.AddListener<EnvironmentChangeEvent>(HandleEnvironmentChange);
        }

        private void HandleEnvironmentChange(EnvironmentChangeEvent evt)
        {
            if ((evt.data.TypeBit & (int)TimeZoneType.Morning) == 1)
            {
                DoColor(morningColor, changeDuration);
            }
            else if ((evt.data.TypeBit & (int)TimeZoneType.Night) == 1)
            {
                DoColor(nightColor, changeDuration);
            }
        }

        private void DoColor(Color targetColor, float duration)
        {
            DOTween.To(() => timeZoneColor.colorFilter.value, x => timeZoneColor.colorFilter.value = x, targetColor, duration);
        }
    }
}