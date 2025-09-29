using System.Collections.Generic;
using CoinSystem;
using Core.GameEvent;
using DG.Tweening;
using Events;
using LKW.Generators;
using RuddnjsLib.Dependencies;
using UnityEngine;
using UnityEngine.Events;

namespace LandSystem.UI
{
    public class GeneratorStoreViewer : MonoBehaviour
    {
        public UnityEvent buyFailEvent;
        public UnityEvent buySuccessEvent;
        
        [SerializeField] private GameEventChannelSO landChannel;
        [SerializeField] private GameEventChannelSO pointChannel;
        
        [SerializeField] private List<GeneratorDataSO> datas;
        [SerializeField] private GeneratorItemView itemViewPrefab;
        [SerializeField] private Transform contentsParent;
        [SerializeField] private RectTransform panel;
        [SerializeField] private Transform warningText;

        [Inject] private CoinManager coinM;
        private bool isOpen;

        private void Awake()
        {
            datas.ForEach(d =>
            {
                var item = Instantiate(itemViewPrefab, contentsParent);
                item.Initialize(d);
            });

            panel.anchoredPosition = new Vector2(-panel.sizeDelta.x, 0);
            warningText.gameObject.SetActive(false);
            
            landChannel.AddListener<RequestGeneratorBuyEvent>(HandleRequestBuyGenerator);
            pointChannel.AddListener<BuyFailEvent>(HandleBuyFailWarningText);
        }

        private void HandleRequestBuyGenerator(RequestGeneratorBuyEvent evt)
        {
            if (coinM.CheckCurrentCoin(-evt.generatorData.needCoinCount))
            {
                buySuccessEvent?.Invoke();
                var completeEvt = LandEvents.BuyCompleteGeneratorEvent.Initializer(evt.generatorData);
                landChannel.RaiseEvent(completeEvt);
                
                HandlePopupGeneratorStore();
            }
        }

        private void OnDestroy()
        {
            landChannel.RemoveListener<RequestGeneratorBuyEvent>(HandleRequestBuyGenerator);
            pointChannel.RemoveListener<BuyFailEvent>(HandleBuyFailWarningText);
        }
        
        private void HandleBuyFailWarningText(BuyFailEvent evt)
        {
            buyFailEvent?.Invoke();
            DOTween.Kill(warningText);
            warningText.gameObject.SetActive(true);
            warningText.transform.rotation = Quaternion.identity;
            
            warningText.DOShakeRotation(0.5f, 10f).SetEase(Ease.OutCirc).OnComplete(() =>
            {
                warningText.gameObject.SetActive(false);
            });
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