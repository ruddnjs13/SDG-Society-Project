using Code.Events;
using Core.GameEvent;
using UnityEngine;

namespace Code.UI.FadeSystem
{
    public class FadeEventSender : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO sceneChannel;

        public void SendFadeInEvent(string sceneName) =>
            sceneChannel.RaiseEvent(SceneEvents.FadeEvent.Init(true, sceneName));
        public void SendFadeOutEvent() => sceneChannel.RaiseEvent(SceneEvents.FadeEvent.Init(false, string.Empty));
    }
}