using Core.GameEvent;
using Events;
using RuddnjsLib.Dependencies;
using UnityEngine;
using UnityEngine.Events;

namespace CoinSystem
{
    [Provide]
    public class CoinManager : MonoBehaviour, IDependencyProvider
    {
        public UnityEvent<int, int, bool> OnCoinChangeValue;
        
        [SerializeField] private GameEventChannelSO pointChannel;
        [SerializeField] private GameEventChannelSO landChannel;

        [SerializeField] private int _currentCoin;
        
        private void Awake()
        {
            pointChannel.AddListener<AddCoinEvent>(HandleAddCoin);
            
            OnCoinChangeValue?.Invoke(_currentCoin,_currentCoin, true);
        }
        private void OnDestroy()
        {
            pointChannel.RemoveListener<AddCoinEvent>(HandleAddCoin);
        }
        
        private void HandleAddCoin(AddCoinEvent evt)
        {
            SetCurrentCoin(evt.coinValue);
        }

        public void SetCurrentCoin(int value)
        {
            if (CheckCurrentCoin(value))
            {
                int tempValue = _currentCoin;
                
                _currentCoin += value;
                OnCoinChangeValue?.Invoke(_currentCoin, tempValue, false);
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