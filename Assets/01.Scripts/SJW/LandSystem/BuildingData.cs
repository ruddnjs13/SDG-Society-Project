using System;
using System.Collections.Generic;
using UnityEngine;

namespace LandSystem
{
    [Serializable]
    public struct BuildingData
    {
        private Vector2 _pivotPos;
        public Vector2Int PivotPos => Vector2Int.RoundToInt(_pivotPos);

        public void SetPivot(Vector2 pos) => _pivotPos = pos;
    }
}