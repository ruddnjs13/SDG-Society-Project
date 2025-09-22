using System;
using System.Collections.Generic;
using UnityEngine;

namespace LandSystem
{
    [Serializable]
    public struct BuildingData
    {
        private List<Vector2Int> _offsetPosList;
        public IReadOnlyList<Vector2Int> OffsetPosListList => _offsetPosList;

        private Vector2 _pivotPos;
        public Vector2Int PivotPos => Vector2Int.RoundToInt(_pivotPos);

        private Transform _target;
        public Transform Target => _target;
        
        public void AddOffset(Vector2Int pos)
        {
            _offsetPosList.Add(pos);
        }

        public void RemoveOffset(Vector2Int pos)
        {
            if (_offsetPosList.Contains(pos))
                _offsetPosList.Remove(pos);
        }

        public void SetPivot(Vector2 pos) => _pivotPos = pos;
        public void SetTarget(Transform target) => _target = target;
    }
}