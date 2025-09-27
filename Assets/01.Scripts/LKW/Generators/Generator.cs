using System;
using _01.Scripts.LKW.Generators;
using LKW.Generaters.LKW.Events;
using Code.Weathers;
using Core.GameEvent;
using LandSystem;
using RuddnjsPool;
using UnityEngine;
using UnityEngine.Serialization;
using VHierarchy.Libs;

namespace LKW.Generators
{
    public class Generator : MonoBehaviour
    {
        [SerializeField] private PoolManagerSO poolManager;
        [SerializeField] private GameEventChannelSO energyChannel;
        [SerializeField] private PoolingItemSO energyItemPrefab;
        
        private GeneratorRenderer _generatorRenderer;
        
        private  float _generateAmount;
        private  float _amountMultiplier;
        
        public float GenerateTime {get; private set;}
        public float RemainingTime { get; set; } = 0f;
        
        public SendEnvironmentData Data {get; set;}

        public bool IsRunning { get; private set; } = false;

        private void Awake()
        {
            _generatorRenderer = GetComponent<GeneratorRenderer>();
        }

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
            _generatorRenderer.SetVisual(generatorData.generatorVisual, IsRunning, _amountMultiplier);
        }

        public void GenerateEnergy()
        {
            int getAmount = Mathf.RoundToInt(_generateAmount * _amountMultiplier); 
            
            GetEnergyEvent evt = EnergyEvents.GetEnergyEvent.Initializer(getAmount);
            energyChannel.RaiseEvent(evt);
            RemainingTime = 0f;
            
            GetEnergyView energyView = poolManager.Pop(energyItemPrefab) as GetEnergyView;
            energyView.ShowEnergyView(transform.position+ new Vector3(0,0.5f,0), getAmount);
        }
        
        
        public void StartGenerate()
        {
            IsRunning = true;
            RemainingTime = 0;
        }

        public void StopGenerate() => IsRunning = false;

        public void SetAmountMultiplier(float amount) => _amountMultiplier = amount;

        [ContextMenu("Add Generator")]
        public void AddGenerator()
        {
            GeneratorManager.Instance.AddGenerator(this);
        }
    }
}
