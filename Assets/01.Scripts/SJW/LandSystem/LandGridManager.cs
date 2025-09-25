using System.Collections.Generic;
using _01.Scripts.SJW.Events;
using Core.GameEvent;
using LKW.Generators;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LandSystem
{
    public class LandGridManager : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO landChannel;
        [SerializeField] private Tilemap canBuildingMap;

        [Header("Generators")] 
        [SerializeField] private Generator generatorPrefab;
        [SerializeField] private Transform generatorGroupParent;
        
        private Dictionary<Vector2Int, Generator> _powerStationGrid;
        
        ///설치 가능한 부분인지 확인
        private HashSet<Vector2Int> _grid; 

        private void Awake()
        {
            _grid = new HashSet<Vector2Int>();
            _powerStationGrid = new Dictionary<Vector2Int, Generator>();

            foreach (var pos in canBuildingMap.cellBounds.allPositionsWithin)
            {
                var posXY = GetVectorRound((Vector3)pos);
                _grid.Add(posXY);
                _powerStationGrid.Add(posXY, null);
            }
            
            landChannel.AddListener<BuildRequestEvent>(HandleBuildRequest);
        }

        private void OnDestroy()
        {
            landChannel.RemoveListener<BuildRequestEvent>(HandleBuildRequest);
        }

        private void HandleBuildRequest(BuildRequestEvent evt)
        {
            RequestBuild(evt.generatorData, evt.position);
        }

        /// <summary>
        /// 그리드에 설치할 수 있는지 확인합니다.
        /// </summary>
        /// <param name="point">확인할 좌표의 반올림값</param>
        /// <returns>설치 할 수 있는가?</returns>
        public bool IsPossibleBuild(Vector2Int point)
        {
            return _grid.Contains(point) && !_powerStationGrid[point];
        }

        public void RequestBuild(GeneratorDataSO data, Vector2 position)
        {
            Vector2Int pos = GetVectorRound(position);
            if (!IsPossibleBuild(pos))
            {
                var evt = LandEvents.BuildFailEvent.Initializer(position);
                landChannel.RaiseEvent(evt);
                return;
            }

            var item = Instantiate(
                generatorPrefab,
                (Vector2)pos,
                Quaternion.identity,
                generatorGroupParent
            );
            
            item.Initialize(data);
            _powerStationGrid[pos] = item;

            if (!IsPossibleBuild(pos))
            {
                var evt = LandEvents.BuildCompleteEvent.Initializer(item);
                landChannel.RaiseEvent(evt);
            }
            else
            {
                Debug.LogError("Not build in the grid");
            }
        }

        private Vector2Int GetVectorRound(Vector2 vec)
        {
            return Vector2Int.RoundToInt(vec);
        }
    }
}