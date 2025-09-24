using System;
using System.Collections.Generic;
using UnityEngine;

namespace LKW.Generators
{
    public class GeneratorManager : MonoSingleton<GeneratorManager>
    {
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

        // buildEVent에서 generate 발행 해주면 그걸로 리스트에 추가함
        
    }
}