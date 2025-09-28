using System;
using Code.Events;
using Code.Weathers;
using Core.GameEvent;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class TimeZoneIconUI : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO environmentChannel;
        [SerializeField] private Sprite morning, night;
        [SerializeField] private Image timeZoneIcon;

        private void Awake()
        {
            environmentChannel.AddListener<EnvironmentChangeEvent>(HandleEnvironmentChange);
        }
        private void OnDestroy()
        {
            environmentChannel.RemoveListener<EnvironmentChangeEvent>(HandleEnvironmentChange);
        }
        private void HandleEnvironmentChange(EnvironmentChangeEvent evt)
        {
            timeZoneIcon.sprite = (evt.data.TypeBit & (int)TimeZoneType.Morning) != 0 ? morning : night;
        }
    }
}