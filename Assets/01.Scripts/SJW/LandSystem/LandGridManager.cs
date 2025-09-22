using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LandSystem
{
    public class LandGridManager : MonoBehaviour
    {
        [SerializeField] private Tilemap canBuildingMap;

        private Dictionary<Vector2Int, GameObject> _powerStationGrid;
        private HashSet<Vector2Int> _grid; //설치 가능한 부분인지 확인
        
        private void Awake()
        {
            _grid = new HashSet<Vector2Int>();
            _powerStationGrid = new Dictionary<Vector2Int, GameObject>();
            
            foreach (var pos in canBuildingMap.cellBounds.allPositionsWithin)
            {
                var posXY = Vector2Int.RoundToInt((Vector3)pos);
                _grid.Add(posXY);
                _powerStationGrid.Add(posXY, null);
            }
        }

        public bool IsPossibleBuild(Vector2Int pos)
        {
            return _grid.Contains(pos) && _powerStationGrid.GetValueOrDefault(pos) == null;
        }
    }
}