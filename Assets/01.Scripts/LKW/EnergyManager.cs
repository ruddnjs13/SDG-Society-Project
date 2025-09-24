using System;
using Core.GameEvent;
using LKW.Generaters.LKW.Events;
using UnityEngine;
using UnityEngine.Rendering;

namespace LKW.Generators.LKW
{
    public class EnergyManager : MonoSingleton<EnergyManager>
    {
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
        }

        private void HandleGetEnergyEvent(GetEnergyEvent evt)
        {
            Energy += evt.getAmount;
        }
    }
}