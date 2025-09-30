using System.Collections.Generic;
using Code.Events;
using Code.Weathers;
using Core.GameEvent;
using Events;
using UnityEngine;

namespace LKW.Generators
{
    public class GeneratorManager : MonoSingleton<GeneratorManager>
    {
        [SerializeField] private GameEventChannelSO landChannel;
        [SerializeField] private GameEventChannelSO environmentChannel;
        
        [field:SerializeField] public SendEnvironmentData EnvironmentData { get; private set; }
        
        public List<Generator> generators = new List<Generator>();

        private void Awake()
        {
            environmentChannel.AddListener<EnvironmentChangeEvent>(HandleEnvironmentChangeEvent);
            landChannel.AddListener<BuildCompleteEvent>(HandleBuildCompleteEvent);
            landChannel.AddListener<BreakGeneratorEvent>(HandleBreakGeneratorEvent);
        }

        private void OnDestroy()
        {
            environmentChannel.RemoveListener<EnvironmentChangeEvent>(HandleEnvironmentChangeEvent);
            landChannel.RemoveListener<BuildCompleteEvent>(HandleBuildCompleteEvent);
            landChannel.RemoveListener<BreakGeneratorEvent>(HandleBreakGeneratorEvent);
        }

        private void Update()
        {
            RunGenerator();
        }

        private void RunGenerator()
        {
            foreach (var generator in generators)
            {
                generator.RemainingTime += Time.deltaTime;
                if (generator.RemainingTime >= generator.GenerateTime && generator.IsRunning)
                {
                    generator.GenerateEnergy();
                    generator.RemainingTime = 0;
                }
            }
        }

        public void AddGenerator(Generator generator)
        {
            generators.Add(generator);
            generator.UpdateEnvironment(EnvironmentData);
        }
        
        private void HandleBuildCompleteEvent(BuildCompleteEvent evt)
        {
            Debug.Log("설치후 추가");
            AddGenerator(evt.generator);
        }

        private void HandleBreakGeneratorEvent(BreakGeneratorEvent evt)
        {
            generators.Remove(evt.generator);
            Destroy(evt.generator.gameObject);
        }

        private void HandleEnvironmentChangeEvent(EnvironmentChangeEvent evt)
        {
            EnvironmentData = evt.data;
            
            foreach (var generator in generators)
            {
               generator.UpdateEnvironment(evt.data);
            }
        }

        public int GetTotalGenerateAmount()
        {
            int total = 0;

            foreach (var generator in generators)
            {
                total += Mathf.CeilToInt(generator.generateAmount / generator.GenerateTime);
            }
            
            return total;
        }

    }
}