using Code.Events;
using Core.GameEvent;
using InputSystem;
using UnityEngine;

namespace Code.UI
{
    public class PauseMenu : MajorUI
    {
        [SerializeField] private InputControllerSO playerInput;
        [SerializeField] private GameEventChannelSO sceneChannel;

        protected override void Awake()
        {
            base.Awake();
            playerInput.OnPausePressed += ControlMenu;
        }

        public void OnInGameExit(string sceneName)
        {
            FadeEvent evt = SceneEvents.FadeEvent;
            evt.sceneName = sceneName;
            evt.isFadeIn = true;
            sceneChannel.RaiseEvent(evt);
        }

        protected override void OnDestroy()
        {
            playerInput.OnPausePressed -= ControlMenu;
            base.OnDestroy();
        }

        protected override void Open()
        {
            base.Open();
            Time.timeScale = 0f;
        }

        protected override void OnCloseCallback()
        {
            base.OnCloseCallback();
            Time.timeScale = 1f;
        }
    }
}