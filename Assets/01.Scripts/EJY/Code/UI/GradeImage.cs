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
        [SerializeField] private Color startColor = Color.black;

        private RectTransform Rect => transform as RectTransform;
        private Vector3 _originSize;
        private Sequence _seq;

        private void Awake()
        {
            _seq = DOTween.Sequence();
            _originSize = transform.localScale;
        }

        private void Update()
        {
            if (UnityEngine.InputSystem.Keyboard.current.digit1Key.wasPressedThisFrame)
            {
                ScaleEffect();
            }
        }

        public void ScaleEffect()
        {
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