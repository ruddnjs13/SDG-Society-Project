using Code.Events;
using Core.GameEvent;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.UI
{
    public class ClickToTitlePanel : MajorUI, IPointerClickHandler
    {
        [SerializeField] private GameEventChannelSO sceneChannel;
        [SerializeField] private TextMeshProUGUI clickText;
        [SerializeField] private float fadeDuration = 0.2f;
        protected override void Awake()
        {
            base.Awake();
            Open();
            clickText.DOFade(0, fadeDuration).SetLoops(-1, LoopType.Yoyo);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            sceneChannel.RaiseEvent(SceneEvents.FadeEvent.Init(true, "TitleScene"));
        }
    }
}