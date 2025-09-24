using System;
using System.Collections.Generic;
using Core.GameEvent;
using UnityEngine;

namespace LKW.Generators
{
    public class GeneratorManager : MonoSingleton<GeneratorManager>
    {
        //[SerializeField] private GameEventChannelSO 
        
        public List<Generator> generators = new List<Generator>();
        
        private void Update()
        {
            foreach (var generator in generators)
            {
                generator.RemainingTime += Time.deltaTime;
                if (generator.RemainingTime >= generator.GenerateTime)
                {
                    generator.GenerateEnergy();
                    generator.RemainingTime = 0;
                }
            }
        }
        
        public void AddGenerator(Generator generator)
        => generators.Add(generator);
        
        
        
    }
}