using System;
using UnityEngine;

namespace LKW.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private RectTransform rect;

        public void SetBar(float bar)
        {
            rect.localScale = new Vector3(bar, 1, 1);
        }
    }
}