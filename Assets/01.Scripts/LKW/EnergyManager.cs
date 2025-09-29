using Core.GameEvent;
using LKW.Generaters.LKW.Events;
using UnityEngine;
using UnityEngine.Events;

namespace LKW
{
    public class EnergyManager : MonoSingleton<EnergyManager>
    {
        public UnityEvent<int, int, bool> OnEnergyChangeValue;
        [SerializeField] private GameEventChannelSO energyChannel;

        private int _energy = 0;

        public int Energy
        {
            get  => _energy;
            set
            {
                int tempValue = Energy;
                
                _energy = value;
                OnEnergyChangeValue?.Invoke(Energy, tempValue, false);
            }
        }

        private void Awake()
        {
            energyChannel.AddListener<GetEnergyEvent>(HandleGetEnergyEvent);
            OnEnergyChangeValue?.Invoke(Energy, Energy, true);
        }

        private void HandleGetEnergyEvent(GetEnergyEvent evt)
        {
            Energy += evt.getAmount;
            
        }
    }
}