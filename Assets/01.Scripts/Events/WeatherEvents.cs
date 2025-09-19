using Code.Weathers;
using Core.GameEvent;

namespace Code.Events
{
    public static class WeatherEvents
    {
        public static WeatherChangeEvent WeatherChangeEvent = new WeatherChangeEvent();
    }

    public class WeatherChangeEvent : GameEvent
    {
        public WeatherDataSO data;

        public WeatherChangeEvent Init(WeatherDataSO data)
        {
            this.data = data;
            return this;
        }
        
    }
}