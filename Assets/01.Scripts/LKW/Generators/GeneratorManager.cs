using System;
using System.Collections.Generic;
using _01.Scripts.SJW.Events;
using Code.Events;
using Code.Weathers;
using Code.Weathers.Utility;
using Core.GameEvent;
using UnityEngine;

namespace LKW.Generators
{
    public class GeneratorManager : MonoSingleton<GeneratorManager>
    {
        [SerializeField] private GameEventChannelSO landChannel;
        [SerializeField] private GameEventChannelSO environmentChannel;
        
        [field:SerializeField] public SendEnvironmentData EnvironmentData { get; private set; }
        
        public List<Generator> generators = new List<Generator>();

        private void OnEnable()
        {
            environmentChannel.AddListener<EnvironmentChangeEvent>(HandleEnvironmentChangeEvent);
            environmentChannel.AddListener<BuildCompleteEvent>(HandleBuildCompleteEvent);
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
            //AddGenerator(evt.);
        }

        private void HandleEnvironmentChangeEvent(EnvironmentChangeEvent evt)
        {
            EnvironmentData = evt.data;
            
            foreach (var generator in generators)
            {
               generator.UpdateEnvironment(evt.data);
            }
        }

    }
}