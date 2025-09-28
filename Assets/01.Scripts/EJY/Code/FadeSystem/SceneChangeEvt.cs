using Code.Events;
using Core.GameEvent;
using UnityEngine;

namespace Code.UI.FadeSystem
{
    public class SceneChangeEvt : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO sceneChannel;
        
        private void Awake()
        {
            sceneChannel.AddListener<ChangeSceneEvent>(HandleChangeSceneEvent);
        }

        private void OnDestroy()
        {
            sceneChannel.RemoveListener<ChangeSceneEvent>(HandleChangeSceneEvent);
        }

        public void SceneChange(string targetSceneName) => sceneChannel.RaiseEvent(SceneEvents.FadeEvent.Init(true, targetSceneName));
        
        private void HandleChangeSceneEvent(ChangeSceneEvent evt)
        {
            if(string.IsNullOrEmpty(evt.sceneName)) return;
            SceneChange(evt.sceneName);
        }
    }
}