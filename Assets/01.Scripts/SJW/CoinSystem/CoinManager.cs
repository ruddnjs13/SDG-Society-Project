using System;
using _01.Scripts.SJW.Events;
using Core.GameEvent;
using Events;
using UnityEngine;

namespace CoinSystem
{
    public class CoinManager : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO pointChannel;
        [SerializeField] private GameEventChannelSO landChannel;

        [SerializeField] private int _currentCoin;
        
        private void Awake()
        {
            pointChannel.AddListener<RequestGeneratorBuyEvent>(HandleGeneratorBuy);
            pointChannel.AddListener<AddCoinEvent>(HandleAddCoin);
        }
        private void OnDestroy()
        {
            pointChannel.RemoveListener<RequestGeneratorBuyEvent>(HandleGeneratorBuy);
            pointChannel.RemoveListener<AddCoinEvent>(HandleAddCoin);
        }

        private void HandleGeneratorBuy(RequestGeneratorBuyEvent evt)
        {
            if (CheckCurrentCoin(-evt.generatorData.needCoinCount))
            {
                var completeEvt = LandEvents.BuyCompleteGeneratorEvent.Initializer(evt.generatorData);
                landChannel.RaiseEvent(completeEvt);
            }
        }
        
        private void HandleAddCoin(AddCoinEvent evt)
        {
            SetCurrentCoin(evt.coinValue);
        }

        public void SetCurrentCoin(int value)
        {
            if (CheckCurrentCoin(value))
            {
                _currentCoin += value;
            }
        }

        public bool CheckCurrentCoin(int value)
        {
            if (_currentCoin + value < 0)
            {
                var evt = PointEvents.BuyFailEvent;
                pointChannel.RaiseEvent(evt);
                return false;
            }

            return true;
        }
    }
}