using FlappyClone.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace FlappyClone.DependencyInjection
{
    public class WallLifecycleManagerInjected : MonoBehaviour, IWallLifecycleManager
    {
        public const float DefaultFillAheadDistance = 10f;
        public const float DefaultFillBehindDistance = 10f;
        public const float DefaultMinWallSpacing = 1f;
        public const float DefaultMaxWallSpacing = 5f;
        public const string DefaultWallNameFormatString = "wall-{0}";

        private IWallSizer _wallSizer;

        private readonly Queue<WallData> _wallPool = new Queue<WallData>();
        private readonly Queue<WallData> _activeWalls = new Queue<WallData>();
        private WallData _newestWall;

        public GameObject WallPrefab;
        public Transform WallParent;
        public Transform FillBasis;
        public string WallNameFormatString = DefaultWallNameFormatString;

        [Header("Wall Spacing")]
        [Min(1f)]
        public float FillAheadDistance = DefaultFillAheadDistance;
        public float FillBehindDistance = DefaultFillBehindDistance;

        [field: SerializeField]
        public float MinWallSpacing { get; set; } = DefaultMinWallSpacing;

        [field: SerializeField]
        public float MaxWallSpacing { get; set; } = DefaultMaxWallSpacing;

        public void Inject(IWallSizer wallSizer)
        {
            _wallSizer = wallSizer;
        }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Reset()
        {
            _wallSizer = null;
            WallPrefab = null;
            WallParent = null;
            WallNameFormatString = DefaultWallNameFormatString;

            FillAheadDistance = DefaultFillAheadDistance;
            FillBehindDistance = DefaultFillBehindDistance;
            MinWallSpacing = DefaultMinWallSpacing;
            MaxWallSpacing = DefaultMaxWallSpacing;
        }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Awake()
        {
            this.AssertAssociation(_wallSizer, nameof(_wallSizer));
            this.AssertAssociation(WallPrefab, nameof(WallPrefab));
            this.AssertAssociation(WallParent, nameof(WallParent));
            this.AssertAssociation(FillBasis, nameof(FillBasis));
        }

        public event EventHandler<WallEventArgs> WallSpawned;

        public void UpdateWalls()
        {
            // Deactivate walls that are too far behind
            WallData oldestWall = _activeWalls.Count == 0 ? null : _activeWalls.Peek();
            while (oldestWall != null && FillBasis.position.x - oldestWall.transform.position.x > FillBehindDistance) {
                deactivateWall();
                oldestWall = _activeWalls.Peek();
            }

            // Activate/spawn walls until far enough ahead
            if (_newestWall == null)
                _newestWall = activateOrSpawnWall(previousDistance: 0f);
            while (_newestWall.transform.position.x - FillBasis.position.x < FillAheadDistance)
                _newestWall = activateOrSpawnWall(_newestWall.transform.position.x);
        }

        private void deactivateWall()
        {
            WallData oldestWall = _activeWalls.Dequeue();
            oldestWall.gameObject.SetActive(false);
            _wallPool.Enqueue(oldestWall);
        }

        private WallData activateOrSpawnWall(float previousDistance)
        {
            WallData wallData;
            bool spawned = false;
            if (_wallPool.Count > 0) {
                wallData = _wallPool.Dequeue();
                wallData.gameObject.SetActive(true);
            }
            else {
                wallData = Instantiate(WallPrefab, WallParent).GetComponent<WallData>();
                wallData.name = string.Format(WallNameFormatString, _wallPool.Count + _activeWalls.Count);
                spawned = true;
            }

            _activeWalls.Enqueue(wallData);

            _wallSizer.Resize(ref wallData);
            float newX = previousDistance + UnityEngine.Random.Range(MinWallSpacing, MaxWallSpacing);
            wallData.transform.position = new Vector2(newX, WallParent.position.y);

            if (spawned) {
                Debug.Log($"Spawned new wall '{wallData.gameObject.name}' in frame {Time.frameCount}.");
                WallSpawned?.Invoke(sender: this, new WallEventArgs { WallData = wallData });
            }

            return wallData;
        }
    }
}
