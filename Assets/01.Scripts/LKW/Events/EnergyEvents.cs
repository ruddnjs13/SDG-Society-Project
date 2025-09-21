using Core.GameEvent;
using UnityEngine;

namespace LKW.Generaters.LKW.Events
{
    public static class EnergyEvents
    {
        public static GetEnergyEvent GetEnergyEvent = new GetEnergyEvent();
    }

    public class GetEnergyEvent : GameEvent
    {
        public int getAmount;

        public GetEnergyEvent Initializer(int getAmount)
        {
            this.getAmount = getAmount;
            return this;
        }
    }
}