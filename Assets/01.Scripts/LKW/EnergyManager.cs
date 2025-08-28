using System;
using _01.Scripts.LKW.Events;
using Core.GameEvent;
using UnityEngine;
using UnityEngine.Rendering;

namespace _01.Scripts.LKW
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

        private void HandleGetEnergyEvent(GetEnergyEvent obj)
        {
            
        }
    }
}