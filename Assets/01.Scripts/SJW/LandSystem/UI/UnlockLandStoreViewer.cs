using System.Linq;
using CoinSystem;
using Core.GameEvent;
using Events;
using RuddnjsLib.Dependencies;
using UnityEngine;

namespace LandSystem.UI
{
    public class UnlockLandStoreViewer : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO landChannel;
        [SerializeField] private UnlockLandViewItem[] unlockViewItems;
        [SerializeField] private int defaultNeedCoinCount;
        [SerializeField] private float coinAddMultiplier;
        [Inject] private CoinManager coinM;

        private void Awake()
        {
            landChannel.AddListener<BuyUnlockLandEvent>(HandleBuyUnlockLand);

            for (int idx = 1; idx <= unlockViewItems.Length; idx++)
            {
                unlockViewItems[idx - 1].SetLockUI(true);

                unlockViewItems[idx - 1].Initialize(idx, defaultNeedCoinCount);
                defaultNeedCoinCount = Mathf.RoundToInt(defaultNeedCoinCount * coinAddMultiplier);
            }
        }

        private void OnDestroy()
        {
            landChannel.RemoveListener<BuyUnlockLandEvent>(HandleBuyUnlockLand);
        }

        private void HandleBuyUnlockLand(BuyUnlockLandEvent evt)
        {
            if (!coinM.CheckCurrentCoin(evt.needCoin))
            {
                return;
            }
            
            var unlockEvt = LandEvents.UnlockLandEvent.Initializer(evt.index);
            landChannel.RaiseEvent(unlockEvt);

            coinM.SetCurrentCoin(evt.needCoin);
            var viewer = unlockViewItems.ToList().FirstOrDefault(item => item.Index == evt.index);
            viewer.SetLockUI(false);
        }
    }
}