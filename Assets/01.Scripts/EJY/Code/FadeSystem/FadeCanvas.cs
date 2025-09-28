using Code.Events;
using Core.GameEvent;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace Code.UI.FadeSystem
{
    public class FadeCanvas : MonoBehaviour
    {
        [SerializeField] private Image fadeImage;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private GameEventChannelSO sceneChannel;
        [SerializeField] private float duration = 3f;
        [SerializeField] private bool inStartFadeIn;

        private readonly int _scrollHash = Shader.PropertyToID("_Scroll");

        private void Awake()
        {
            sceneChannel.AddListener<FadeEvent>(HandleFadeEvent);
            SetCanvasGroup(false);
        }

        private void Start()
        {
            fadeImage.material = new Material(fadeImage.material);

            if (inStartFadeIn)
            {
                Fade(false);
            }
        }

        private void OnDestroy()
        {
            sceneChannel.RemoveListener<FadeEvent>(HandleFadeEvent);
        }

        private void HandleFadeEvent(FadeEvent evt)
        {
            Fade(evt.isFadeIn, evt.sceneName);
        }

        private void SetCanvasGroup(bool isStart)
        {
            canvasGroup.alpha = isStart ? 1 : 0;
            canvasGroup.interactable = isStart;
            canvasGroup.blocksRaycasts = isStart;
        }

        private void Fade(bool isFadeIn, string sceneName = null)
        {
            int startValue = isFadeIn ? 0 : 3;
            int endValue = isFadeIn ? 3 : 0;
            
            fadeImage.material.SetFloat(_scrollHash, startValue);
            
            SetCanvasGroup(true);

            fadeImage.material.DOFloat(endValue, _scrollHash, duration)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    if (isFadeIn && !string.IsNullOrEmpty(sceneName))
                    {
                        Time.timeScale = 1;
                        SceneManager.LoadScene(sceneName);
                    }
                    else
                    {
                        SetCanvasGroup(false);
                    }
                });
        }
    }
}