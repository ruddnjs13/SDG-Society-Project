using System;
using Core.GameEvent;
using LKW.Generaters.LKW.Events;
using UnityEngine;
using UnityEngine.Events;

namespace Code.UI
{
    public class EnergyBar : MonoBehaviour
    {
        [SerializeField] private float targetEnergy;
        [SerializeField] private RectTransform barRect;
        [SerializeField] private GameEventChannelSO energyChannel;

        private UnityEvent<bool> onCompleteEvent;
        
        private float _currentEnergy = 0;
        
        private void Awake()
        {
            energyChannel.AddListener<GetEnergyEvent>(HandleEnergyBar);
        }

        private void OnDestroy()
        {
            energyChannel.RemoveListener<GetEnergyEvent>(HandleEnergyBar);
        }

        private void HandleEnergyBar(GetEnergyEvent evt)
        {
            _currentEnergy += evt.getAmount;

            if (Mathf.Approximately(_currentEnergy, targetEnergy))
            {
                onCompleteEvent?.Invoke(true);
            }
            
            barRect.localScale = new Vector3(Mathf.Clamp01(_currentEnergy / targetEnergy),1,1);
        }
    }
}