using Core.GameEvent;

namespace LKW.Generaters.LKW.Events
{
    public static class PollutionEvents
    {
        public static GetPollutionEvent GetPollutionEvent = new GetPollutionEvent();
    }

    public class GetPollutionEvent : GameEvent
    {
        public int pollutionAmount;

        public GetPollutionEvent Initializer(int pollutionAmount)
        {
            this.pollutionAmount = pollutionAmount;
            
            return this;
        }
    }
}