using Code.Events;
using Core.GameEvent;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class GradeImage : MonoBehaviour
    {
        [SerializeField] private float duration = 0.2f;
        [SerializeField] private float sizeMultiplier = 3;
        [SerializeField] private Image gradeImage;
        [SerializeField] private GameEventChannelSO sceneChannel;
        [SerializeField] private Sprite aImage, bImage, cImage;
        [SerializeField] private Color startColor = Color.black;

        private RectTransform Rect => transform as RectTransform;
        private Vector3 _originSize;
        private Sequence _seq;

        private void Awake()
        {
            _seq = DOTween.Sequence();
            _originSize = transform.localScale;
        }

        public void ScaleEffect(float percentage)
        {
            if (Mathf.Approximately(0f, percentage))
            {
                gradeImage.enabled = false;
                sceneChannel.RaiseEvent(SceneEvents.FadeEvent.Init(true, "GameClear"));
            }
            else if (percentage is <= 10 and > 0)
            {
                gradeImage.sprite = aImage;
            }
            else if (percentage is <= 40 and > 10)
            {
                gradeImage.sprite = bImage;
            }
            else if (percentage is <= 70 and > 40)
            {
                gradeImage.sprite = cImage;
            }
            
            if (_seq != null && _seq.IsActive())
            {
                _seq.Kill();
            }

            if (!_seq.IsActive())
                _seq = DOTween.Sequence();

            Rect.localScale = _originSize * sizeMultiplier;
            gradeImage.color = startColor;
            _seq.Append(Rect.DOScale(_originSize, duration).SetEase(Ease.InCirc));
            _seq.Join(gradeImage.DOColor(Color.white, duration).SetEase(Ease.InCirc));
        }
    }
}