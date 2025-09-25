using Code.Weathers;
using Core.GameEvent;
using UnityEngine;

namespace Code.Events
{
    public static class EnvironmentEvents
    {
        public static EnvironmentChangeEvent EnvironmentChangeEvent = new EnvironmentChangeEvent();
        public static TimeZoneChangeEvent TimeZoneChangeEvent = new TimeZoneChangeEvent();
    }

    public class EnvironmentChangeEvent : GameEvent
    {
        public SendEnvironmentData data;
        public Sprite icon;

        public EnvironmentChangeEvent Init(SendEnvironmentData data,Sprite icon)
        {
            this.data = data;
            this.icon = icon;
            return this;
        }
    }
    
    public class TimeZoneChangeEvent : GameEvent
    {
        public TimeZoneType type;

        public TimeZoneChangeEvent Init(TimeZoneType type)
        {
            this.type = type;
            return this;
        }
    }
}