using Core.GameEvent;
using UnityEngine;

namespace _01.Scripts.LKW.Events
{
    public static class EnergyEvents
    {
        public static GetEnergyEvent GetEnergyEvent = new GetEnergyEvent();
    }

    public class GetEnergyEvent : GameEvent
    {
        private int getAmount;

        public GetEnergyEvent Initializer(int getAmount)
        {
            this.getAmount = getAmount;
            return this;
        }
    }
}