using Core.GameEvent;
using LKW.Generaters.LKW.Events;
using UnityEngine;

namespace LKW.Penalties
{
    public class PollutionPenalty : MonoBehaviour, IPenalty
    {
        [SerializeField] private GameEventChannelSO pollutionChannel;

        public void Penalty()
        {
            var evt = PollutionEvents.GetPollutionEvent.Initializer(1);
            
            pollutionChannel.RaiseEvent(evt);
        }
    }
}