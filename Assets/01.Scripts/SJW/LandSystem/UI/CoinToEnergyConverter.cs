using CoinSystem;
using LKW;
using RuddnjsLib.Dependencies;
using UnityEngine;

namespace LandSystem.UI
{
    public class CoinToEnergyConverter : MonoBehaviour
    {
        [Inject] private CoinManager _coinM;

        public void ChangeEnergy()
        {
            int value = EnergyManager.Instance.Energy / 10;
            _coinM.AddCoin(value);
            EnergyManager.Instance.Energy %= 10;
        }
    }
}