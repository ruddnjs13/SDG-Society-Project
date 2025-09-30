using System.Collections.Generic;
using Events;
using Core.GameEvent;
using LKW.Generators;
using RuddnjsLib.Dependencies;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LandSystem
{
    [Provide]
    public class LandGridManager : MonoBehaviour, IDependencyProvider
    {
        [SerializeField] private GameEventChannelSO landChannel;
        [SerializeField] private GameEventChannelSO pointChannel;
        [SerializeField] private Tilemap[] canBuildingMaps;

        [Header("Generators")] 
        [SerializeField] private Generator generatorPrefab;
        [SerializeField] private Transform generatorGroupParent;
        
        private Dictionary<Vector2Int, Generator> _powerStationGrid;
        
        ///설치 가능한 부분인지 확인
        private HashSet<Vector2Int> _grid; 
        private HashSet<Vector2Int> _nearWaterGrid; 

        private void Awake()
        {
            _grid = new HashSet<Vector2Int>();
            _nearWaterGrid = new HashSet<Vector2Int>();
            _powerStationGrid = new Dictionary<Vector2Int, Generator>();

            SetupGrid(0);

            landChannel.AddListener<BuildRequestEvent>(HandleBuildRequest);
            landChannel.AddListener<UnlockLandEvent>(HandleUnlockLand);
        }
        
        private void OnDestroy()
        {
            landChannel.RemoveListener<BuildRequestEvent>(HandleBuildRequest);
            landChannel.RemoveListener<UnlockLandEvent>(HandleUnlockLand);
        }
        
        private void HandleUnlockLand(UnlockLandEvent evt)
        {
            SetupGrid(evt.index);
        }

        private bool CheckNearWater(Vector2Int pos)
        {
            int[] dx = { 0, 1, 1, 1, 0, -1, -1, -1 };
            int[] dy = { 1, 1, 0, -1, -1, -1, 0,  1 };
            
            for (int i = 0; i < 8; i++)
            {
                var position = pos + new Vector2Int(dx[i], dy[i]);
                if (!_grid.Contains(position)) return true;
            }
            
            return false;
        }

        private void HandleBuildRequest(BuildRequestEvent evt)
        {
            RequestBuild(evt.generatorData, evt.position);
        }

        /// <summary>
        /// 그리드에 설치할 수 있는지 확인합니다.
        /// </summary>
        /// <param name="point">확인할 좌표의 반올림값</param>
        /// <param name="isNearWater">근처에 물이 있는지 까지 확인해야 한다면 true</param>
        /// <returns>설치 할 수 있는가?</returns>
        public bool IsPossibleBuild(Vector2Int point, bool isNearWater = false)
        {
            if(isNearWater)
                return _nearWaterGrid.Contains(point) && !_powerStationGrid[point];
            
            return _grid.Contains(point) && !_powerStationGrid[point];
        }

        public void RequestBuild(GeneratorDataSO data, Vector2 position)
        {
            Vector2Int pos = GetVectorRound(position);
            if (!IsPossibleBuild(pos, data.isNeedWater))
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

            if (!IsPossibleBuild(pos, data.isNeedWater))
            {
                var completeEvt = LandEvents.BuildCompleteEvent.Initializer(item);
                landChannel.RaiseEvent(completeEvt);
                
                var buyEvt = PointEvents.AddCoinEvent.Initializer(-data.needCoinCount);
                pointChannel.RaiseEvent(buyEvt);

            }
            else
            {
                Debug.LogError("Not build in the grid");
            }
        }

        public bool BreakGenerator(Vector2Int pos)
        {
            if (!_grid.Contains(pos) || _powerStationGrid[pos] == null) return false;

            var item = _powerStationGrid[pos];
            _powerStationGrid[pos] = null;

            var evt = LandEvents.BreakGeneratorEvent.Initializer(item);
            landChannel.RaiseEvent(evt);
            return true;
        }
        
        private void SetupGrid(int idx)
        {
            foreach (var pos in canBuildingMaps[idx].cellBounds.allPositionsWithin)
            {
                if(!canBuildingMaps[idx].HasTile(pos)) continue;
                
                var posXY = GetVectorRound((Vector3)pos);
                if(_grid.Contains(posXY) && _powerStationGrid.ContainsKey(posXY)) continue;
                
                _grid.Add(posXY);
                _powerStationGrid.Add(posXY, null);
            }

            foreach (var tilePos in _grid)
            {
                if (CheckNearWater(tilePos))
                {
                    _nearWaterGrid.Add(tilePos);
                }
            }
        }

        private Vector2Int GetVectorRound(Vector2 vec)
        {
            return Vector2Int.RoundToInt(vec);
        }
    }
}