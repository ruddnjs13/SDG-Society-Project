using UnityEngine;

namespace Code.UI
{
    public class ResultPanel : MajorUI
    {
        [SerializeField] private CanvasGroup clearGroup, failGroup;
        [SerializeField] private GradeImage gradeImage;

        public void GenerateResult(bool isClear)
        {
            if (isClear)
            {
                //gradeImage.ScaleEffect();
                SetCanvasGroup(clearGroup, true);
                SetCanvasGroup(failGroup, false);
            }
            else
            {
                SetCanvasGroup(clearGroup, false);
                SetCanvasGroup(failGroup, true);
            }
        }

        private void SetCanvasGroup(CanvasGroup targetCanvasGroup, bool isOn)
        {
            targetCanvasGroup.alpha = isOn ? 1 : 0;
            targetCanvasGroup.interactable = isOn;
            targetCanvasGroup.blocksRaycasts = isOn;
        }
    }
}