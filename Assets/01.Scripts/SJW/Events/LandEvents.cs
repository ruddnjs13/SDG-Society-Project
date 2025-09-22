using Core.GameEvent;
using LandSystem;
using UnityEngine;

namespace _01.Scripts.SJW.Events
{
    public static class LandEvents
    {
        public static readonly BuildCompleteEvent BuildCompleteEvent = new BuildCompleteEvent();
        public static readonly BuildFailEvent BuildFailEvent = new BuildFailEvent();
    }

    public class BuildCompleteEvent : GameEvent
    {
        public BuildingData buildData;

        public BuildCompleteEvent Initializer(BuildingData data)
        {
            buildData = data;
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
}