using Core.GameEvent;
using LKW.Generators;

namespace Events
{
    public static class PointEvents
    {
        public static readonly RequestGeneratorBuyEvent RequestGeneratorBuyEvent = new RequestGeneratorBuyEvent();
        public static readonly GeneratorBuyFailEvent GeneratorBuyFailEvent = new GeneratorBuyFailEvent();
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
    
    public class GeneratorBuyFailEvent : GameEvent
    {
        
    }
    
    
}