using Core.GameEvent;
using LKW.Generators;

namespace Events
{
    public static class PointEvents
    {
        public static readonly RequestGeneratorBuyEvent RequestGeneratorBuyEvent = new RequestGeneratorBuyEvent();
        public static readonly BuyFailEvent BuyFailEvent = new BuyFailEvent();
        public static readonly AddCoinEvent AddCoinEvent = new AddCoinEvent();
    }

    public class AddCoinEvent : GameEvent
    {
        public int coinValue;
        
        public AddCoinEvent Initializer(int value)
        {
            coinValue = value;
            return this;
        }
    }
    
    public class RequestGeneratorBuyEvent : GameEvent
    {
        public GeneratorDataSO generatorData;
        
        public RequestGeneratorBuyEvent Initializer(GeneratorDataSO data)
        {
            generatorData = data;
            return this;
        }
    }
    
    public class BuyFailEvent : GameEvent
    { }
}