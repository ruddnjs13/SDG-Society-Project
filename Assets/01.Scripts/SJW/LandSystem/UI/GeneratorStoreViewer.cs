using System;
using System.Collections.Generic;
using Core.GameEvent;
using DG.Tweening;
using Events;
using LKW.Generators;
using UnityEngine;

namespace LandSystem.UI
{
    public class GeneratorStoreViewer : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO pointChannel; //임시
        
        [SerializeField] private List<GeneratorDataSO> datas;
        [SerializeField] private GeneratorItemView itemViewPrefab;
        [SerializeField] private Transform contentsParent;
        [SerializeField] private RectTransform panel;
        private bool isOpen;

        private void Awake()
        {
            datas.ForEach(d =>
            {
                var item = Instantiate(itemViewPrefab, contentsParent);
                item.Initialize(d);
            });

            panel.anchoredPosition = new Vector2(-panel.sizeDelta.x, 0);
            
            pointChannel.AddListener<RequestGeneratorBuyEvent>(HandlePopupStore);
        }

        private void OnDestroy()
        {
            pointChannel.RemoveListener<RequestGeneratorBuyEvent>(HandlePopupStore);
        }

        private void HandlePopupStore(RequestGeneratorBuyEvent evt)
        {
            HandlePopupGeneratorStore();
        }

        public void HandlePopupGeneratorStore()
        {
            isOpen = !isOpen;
            panel.DOAnchorPosX(isOpen ? 0 : -panel.sizeDelta.x, 0.3f).SetEase(Ease.OutCubic);
        }

        [ContextMenu("TestPopup")]
        private void TestPopupUI()
        {
            HandlePopupGeneratorStore();
        }
    }
}