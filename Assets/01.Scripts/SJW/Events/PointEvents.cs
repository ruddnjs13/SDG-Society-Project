using Core.GameEvent;

namespace Events
{
    public static class PointEvents
    {
        public static readonly RequestGeneratorBuyEvent RequestGeneratorBuyEvent = new RequestGeneratorBuyEvent();
        public static readonly GeneratorBuyFailEvent GeneratorBuyFailEvent = new GeneratorBuyFailEvent();
    }

    public class RequestGeneratorBuyEvent : GameEvent
    {
        public int wantCoin;
        
        public RequestGeneratorBuyEvent Initializer(int coin)
        {
            wantCoin = coin;
            return this;
        }
    }
    
    public class GeneratorBuyFailEvent : GameEvent
    {
        
    }
    
    
}