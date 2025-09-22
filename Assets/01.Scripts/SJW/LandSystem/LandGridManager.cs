using System.Collections.Generic;
using _01.Scripts.SJW.Events;
using Core.GameEvent;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LandSystem
{
    public class LandGridManager : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO landChannel;
        [SerializeField] private Tilemap canBuildingMap;

        private Dictionary<Vector2Int, Transform> _powerStationGrid;
        private HashSet<Vector2Int> _grid; //설치 가능한 부분인지 확인
        
        private void Awake()
        {
            _grid = new HashSet<Vector2Int>();
            _powerStationGrid = new Dictionary<Vector2Int, Transform>();
            
            foreach (var pos in canBuildingMap.cellBounds.allPositionsWithin)
            {
                var posXY = Vector2Int.RoundToInt((Vector3)pos);
                _grid.Add(posXY);
                _powerStationGrid.Add(posXY, null);
            }
        }

        public bool IsPossibleBuild(BuildingData data)
        {
            foreach (var pos in data.OffsetPosListList)
            {
                Vector2Int position = data.PivotPos + pos;
                if (!_grid.Contains(position) || _powerStationGrid[position])
                {
                    return false;
                }
            }
            
            return true;
        }

        public void SetBuildStation(BuildingData data)
        {
            if (!IsPossibleBuild(data))
            {
                var evt = LandEvents.BuildFailEvent.Initializer(data.PivotPos);
                landChannel.RaiseEvent(evt);
                return;
            }

            foreach (var offset in data.OffsetPosListList)
            {
                Vector2Int pos = data.PivotPos + offset;

                _powerStationGrid[pos] = data.Target;
            }
            
            if(!IsPossibleBuild(data))
            {
                var evt = LandEvents.BuildCompleteEvent.Initializer(data);
                landChannel.RaiseEvent(evt);
            }
        }
    }
}