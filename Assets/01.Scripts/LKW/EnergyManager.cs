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
                _energy += value;
            }
        }

        private void Awake()
        {
            energyChannel.AddListener<GetEnergyEvent>(HandleGetEnergyEvent);
            OnEnergyChangeValue?.Invoke(Energy, Energy, true);
        }

        private void HandleGetEnergyEvent(GetEnergyEvent evt)
        {
            int tempValue = Energy;
            
            Energy += evt.getAmount;
            OnEnergyChangeValue?.Invoke(Energy, tempValue, false);
        }
    }
}