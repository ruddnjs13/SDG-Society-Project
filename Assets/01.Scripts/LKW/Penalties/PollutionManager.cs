using System;
using Code.Events;
using Core.GameEvent;
using LKW.Generaters.LKW.Events;
using LKW.UI;
using UnityEngine;

namespace LKW.Penalties
{
    public class PollutionManager : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO pollutionChannel;
        [SerializeField] private GameEventChannelSO sceneChannel;
        [SerializeField] private ProgressBar progressBar;
        [SerializeField] private float maxPollution;

        public float currentPollution = 0;

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

            Debug.Log(currentPollution);
            progressBar.SetBar(Mathf.Clamp01(currentPollution/ maxPollution));
            if (currentPollution >= maxPollution)
            {
                var sendEvt = SceneEvents.ChangeSceneEvent.Init("GameOver");
                
                sceneChannel.RaiseEvent(sendEvt);
            }
        }
    }
}