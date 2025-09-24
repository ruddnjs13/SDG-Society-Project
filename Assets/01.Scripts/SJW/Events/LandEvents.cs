using Core.GameEvent;
using LandSystem;
using LKW.Generaters;
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
        public Transform targetTrm;

        public BuildCompleteEvent Initializer(Transform target)
        {
            targetTrm = target;
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
        public GeneratorData generatorData;

        public BuildRequestEvent Initializer(GeneratorData data)
        {
            generatorData = data;
            return this;
        }
    }
}