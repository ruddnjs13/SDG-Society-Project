using Core.GameEvent;
using LandSystem;
using LKW.Generaters;
using LKW.Generators;
using UnityEngine;

namespace _01.Scripts.SJW.Events
{
    public static class LandEvents
    {
        public static readonly BuildCompleteEvent BuildCompleteEvent = new BuildCompleteEvent();
        public static readonly BuildFailEvent BuildFailEvent = new BuildFailEvent();
        public static readonly BuildRequestEvent BuildRequestEvent = new BuildRequestEvent();
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
}