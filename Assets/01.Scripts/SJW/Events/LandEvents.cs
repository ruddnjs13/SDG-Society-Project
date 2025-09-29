using Core.GameEvent;
using LandSystem;
using LKW.Generaters;
using LKW.Generators;
using UnityEngine;

namespace Events
{
    public static class LandEvents
    {
        public static readonly BuildCompleteEvent BuildCompleteEvent = new BuildCompleteEvent();
        public static readonly BuildFailEvent BuildFailEvent = new BuildFailEvent();
        public static readonly BuildRequestEvent BuildRequestEvent = new BuildRequestEvent();
        public static readonly BuyCompleteGeneratorEvent BuyCompleteGeneratorEvent = new BuyCompleteGeneratorEvent();
        public static readonly UnlockLandEvent UnlockLandEvent = new UnlockLandEvent();
        public static readonly BuyUnlockLandEvent BuyUnlockLandEvent = new BuyUnlockLandEvent();
    }

    public class BuildCompleteEvent : GameEvent
    {
        public Generator generator;

        public BuildCompleteEvent Initializer(Generator target)
        {
            generator = target;
            return this;
        }
    }

    public class BuildFailEvent : GameEvent
    {
        public Vector2 position;
        
        public BuildFailEvent Initializer(Vector2 pos)
        {
            position = pos;
            return this;
        }
    }

    public class BuildRequestEvent : GameEvent
    {
        public GeneratorDataSO generatorData;
        public Vector2 position;
        public BuildRequestEvent Initializer(GeneratorDataSO data, Vector2 pos)
        {
            generatorData = data;
            position = pos;
            return this;
        }
    }
    public class BuyCompleteGeneratorEvent : GameEvent
    {
        public GeneratorDataSO generatorData;
        
        public BuyCompleteGeneratorEvent Initializer(GeneratorDataSO data)
        {
            generatorData = data;
            return this;
        }
    }

    public class BuyUnlockLandEvent : GameEvent
    {
        public int index;
        public int needCoin;
        
        public BuyUnlockLandEvent Initializer(int idx, int coin)
        {
            index = idx;
            needCoin = coin;
            return this;
        }
    }
    
    public class UnlockLandEvent : GameEvent
    {
        public int index;
        
        public UnlockLandEvent Initializer(int idx)
        {
            index = idx;
            return this;
        }
    }
}