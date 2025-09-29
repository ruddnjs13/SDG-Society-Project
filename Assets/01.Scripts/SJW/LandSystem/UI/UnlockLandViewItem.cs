using System;
using Core.GameEvent;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LandSystem.UI
{
    public class UnlockLandViewItem : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO landChannel;

        [Header("Elements")] 
        [SerializeField] private GameObject lockPanelObj;
        [SerializeField] private TextMeshProUGUI indexText;
        [SerializeField] private TextMeshProUGUI coinText;
        [SerializeField] private Button buyButton;

        private int _needCoinCount;
        private int _myIndex;
        public int Index => _myIndex; 
        
        public void Initialize(int idx, int coinCount)
        {
            _myIndex = idx;
            _needCoinCount = coinCount;
            buyButton.onClick.AddListener(HandleBuyUnlockLand);
        }

        private void HandleBuyUnlockLand()
        {
            var evt = LandEvents.BuyUnlockLandEvent.Initializer(_myIndex, _needCoinCount);
            landChannel.RaiseEvent(evt);
        }
        
        public void SetLockUI(bool isLock)
        {
            lockPanelObj.SetActive(isLock);
        }
    }
}