using UnityEngine;

namespace _01.Scripts.LKW.PowerStation
{
    [CreateAssetMenu(fileName = "StationData", menuName = "SO/StationData", order = 0)]
    public class StationDataSO : ScriptableObject
    {
        public float generateTime;
        public float generateAmount;
        public float amountMultiplier;

        public bool isStopGenerate;
    }
}