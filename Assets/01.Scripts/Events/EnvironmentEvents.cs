using Code.Weathers;
using Core.GameEvent;
using UnityEngine;

namespace Code.Events
{
    public static class EnvironmentEvents
    {
        public static WeatherChangeEvent WeatherChangeEvent = new WeatherChangeEvent();
        public static TimeZoneChangeEvent TimeZoneChangeEvent = new TimeZoneChangeEvent();
    }

    public class WeatherChangeEvent : GameEvent
    {
        public SendWeatherData data;
        public Sprite icon;

        public WeatherChangeEvent Init(SendWeatherData data,Sprite icon)
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