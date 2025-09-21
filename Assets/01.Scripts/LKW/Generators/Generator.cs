using LKW.Generaters.LKW.Events;
using Code.Weathers;
using Core.GameEvent;
using UnityEngine;
using UnityEngine.Serialization;

namespace LKW.Generaters
{
    public class Generator : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO energyChannel;
        [SerializeField] private GameObject getImage;
        
        private  float _generateAmount;
        private  float _amountMultiplier;
        public  WeatherType WeatherType { get; private set; }
        public float GenerateTime {get; private set;}
        public float RemainingTime { get; set; } = 0f;
        
        public void Initialize(GeneratorDataSO generatorData)
        {
            GenerateTime = generatorData.generateTime;
            WeatherType = generatorData.weatherType;
            _generateAmount = generatorData.generateAmount;
            _amountMultiplier = generatorData.amountMultiplier;
        }

        public void GenerateEnergy()
        {
            int getAmount = Mathf.RoundToInt(_generateAmount * _amountMultiplier); 
            
            GetEnergyEvent evt = EnergyEvents.GetEnergyEvent.Initializer(getAmount);
            energyChannel.RaiseEvent(evt);
            RemainingTime = 0f;
        }
    }
}
