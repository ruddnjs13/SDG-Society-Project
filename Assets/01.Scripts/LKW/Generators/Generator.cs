using System;
using LKW.Generaters.LKW.Events;
using Code.Weathers;
using Core.GameEvent;
using RuddnjsPool;
using UnityEngine;
using UnityEngine.Serialization;

namespace LKW.Generators
{
    public class Generator : MonoBehaviour
    {
        [SerializeField] private PoolManagerSO poolManager;
        [SerializeField] private GameEventChannelSO energyChannel;
        [SerializeField] private PoolingItemSO energyItemPrefab;
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        
        private  float _generateAmount;
        private  float _amountMultiplier;
        
        public float GenerateTime {get; private set;}
        public float RemainingTime { get; set; } = 0f;
        
        public SendEnvironmentData Data {get; set;}

        public bool isRunning { get; private set; } = false;

        public void Initialize(GeneratorDataSO generatorData)
        {
            int typeBit = (int)generatorData.weatherType | (int)generatorData.timeZoneType;

            Data = new SendEnvironmentData()
            {
                TypeBit = typeBit,
            };
           
            GenerateTime = generatorData.generateTime;
            _generateAmount = generatorData.generateAmount;
            _amountMultiplier = generatorData.amountMultiplier;
            spriteRenderer.sprite = generatorData.generatorVisual;
        }

        public void GenerateEnergy()
        {
            int getAmount = Mathf.RoundToInt(_generateAmount * _amountMultiplier); 
            
            GetEnergyEvent evt = EnergyEvents.GetEnergyEvent.Initializer(getAmount);
            energyChannel.RaiseEvent(evt);
            RemainingTime = 0f;
            
            GetEnergyView energyView = poolManager.Pop(energyItemPrefab) as GetEnergyView;
            energyView.ShowEnergyView(transform.position+ new Vector3(0,0.5f,0));
        }

        public void StartGenerate()
        {
            isRunning = true;
            RemainingTime = 0;
        }

        public void StopGenerate() => isRunning = false;

        public void SetAmountMultiplier(float amount)
        {
            _amountMultiplier = amount;
        }

        [ContextMenu("Add Generator")]
        public void AddGenerator()
        {
            GeneratorManager.Instance.AddGenerator(this);
        }
    }
}
