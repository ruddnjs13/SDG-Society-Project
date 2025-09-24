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
            
            landChannel.AddListener<BuildCompleteEvent>(HandleBuildComplete);
        }

        public bool IsPossibleBuild(Vector2Int point)
        {
            return _grid.Contains(point) && !_powerStationGrid[point];
        }

        public void RequestBuild(GeneratorData data)
        {
            if (!IsPossibleBuild(Vector2Int.RoundToInt(data.position)))
            {
                var evt = LandEvents.BuildFailEvent.Initializer(data.position);
                landChannel.RaiseEvent(evt);
                return;
            }
            
            var requestEvt = LandEvents.BuildRequestEvent.Initializer(data);
            landChannel.RaiseEvent(requestEvt);
        }
        
        private void HandleBuildComplete(BuildCompleteEvent evt)
        {
            Vector2Int pos = Vector2Int.RoundToInt(evt.targetTrm.position);
            
            if (!IsPossibleBuild(pos))
            {
                Debug.LogError("there is not empty place");
                return;
            }

            _powerStationGrid[pos] = evt.targetTrm;
        }
    }
}