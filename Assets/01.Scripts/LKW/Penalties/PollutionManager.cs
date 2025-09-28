using System;
using Core.GameEvent;
using LKW.Generaters.LKW.Events;
using UnityEngine;

namespace LKW.Penalties
{
    public class PollutionManager : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO pollutionChannel;
        
        [SerializeField] private int maxPollution;

        public int currentPollution = 0;

        private void OnEnable()
        {
            pollutionChannel.AddListener<GetPollutionEvent>(HandlePollution);
        }
        private void OnDestroy()
        {
            pollutionChannel.RemoveListener<GetPollutionEvent>(HandlePollution);
        }

        private void HandlePollution(GetPollutionEvent evt)
        {
            currentPollution += evt.pollutionAmount;

            if (currentPollution >= maxPollution)
            {
                
            }
        }
    }
}