using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace FlappyClone
{
    public class WallSpawnEvent
    {
        public WallData WallData;
    }

    public class WallSpawner : MonoBehaviour
    {
        public const float DefaultSpawnOffset = 0f;
        public const float DefaultMinWallSpacing = 1f;
        public const float DefaultMaxWallSpacing = 5f;
        public const int DefaultMinHoleHeight = 2;
        public const int DefaultMaxHoleHeight = 8;
        public const float DefaultWallColliderOffset = 0.1f;
        public const int DefaultWorldHeight = 10;
        public const bool DefaultDeleteOldestWallOnSpawn = true;
        public const int DefaultMaxWalls = 5;

        public GameObject WallPrefab;
        public Transform WallParent;
        public float SpawnOffset = DefaultSpawnOffset;
        public float MinWallSpacing = DefaultMinWallSpacing;
        public float MaxWallSpacing = DefaultMaxWallSpacing;
        public int MinHolelHeight = DefaultMinHoleHeight;
        public int MaxHoleHeight = DefaultMaxHoleHeight;
        public float WallColliderOffset = DefaultWallColliderOffset;
        public int WorldHeight = DefaultWorldHeight;
        public bool DeleteOldestWallOnSpawn = DefaultDeleteOldestWallOnSpawn;
        public int MaxWalls = DefaultMaxWalls;

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Reset()
        {
            WallPrefab = null;
            WallParent = null;
            SpawnOffset = DefaultSpawnOffset;
            MinWallSpacing = DefaultMinWallSpacing;
            MaxWallSpacing = DefaultMaxWallSpacing;
            MinHolelHeight = DefaultMinHoleHeight;
            MaxHoleHeight = DefaultMaxHoleHeight;
            WallColliderOffset = DefaultWallColliderOffset;
            WorldHeight = DefaultWorldHeight;
            DeleteOldestWallOnSpawn = DefaultDeleteOldestWallOnSpawn;
            MaxWalls = DefaultMaxWalls;
        }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Start()
        {
            this.AssertAssociation(WallPrefab, nameof(WallPrefab));
            this.AssertAssociation(WallParent, nameof(WallParent));
        }

        public event System.EventHandler<WallSpawnEvent> OldWallDeleted;
        public event System.EventHandler<WallSpawnEvent> NewWallSpawned;

        public void Spawn()
        {
            Vector3 pos = new Vector2(SpawnOffset + Random.Range(MinWallSpacing, MaxWallSpacing), WallParent.position.y);
            GameObject wallObj = Instantiate(WallPrefab, pos, Quaternion.identity, WallParent);
            WallData wallData = wallObj.GetComponentInChildren<WallData>();

            int randHoleHeight = Random.Range(MinHolelHeight, MaxHoleHeight + 1);   // Random.Range max is exclusive with integer args, hence the +1s
            int randOffset = Random.Range(1, WorldHeight - randHoleHeight);         // Missing +1 here is intentional, so there is wall on either side of hole
            float offset = 2f * WallColliderOffset;

            wallData.TopSprite.transform.localPosition = (WorldHeight - randOffset) * Vector3.up;
            wallData.TopSprite.size = new Vector2(wallData.TopSprite.size.x, randOffset);
            wallData.TopCollider.Collider.offset = randOffset / 2f * Vector2.up;
            if (wallData.TopCollider.Collider is BoxCollider2D topBox)
                topBox.size = new Vector2(topBox.size.x - offset, randOffset - offset);

            wallData.ClearTrigger.transform.localPosition = (WorldHeight - randOffset - randHoleHeight / 2f) * Vector3.up;
            if (wallData.ClearTrigger.Collider is BoxCollider2D clearTrigger)
                clearTrigger.size = new Vector2(clearTrigger.size.x, randHoleHeight);

            wallData.BottomSprite.size = new Vector2(wallData.BottomSprite.size.x, WorldHeight - randOffset - randHoleHeight);
            wallData.BottomCollider.Collider.offset = (WorldHeight - randOffset - randHoleHeight) / 2f * Vector2.up;
            if (wallData.BottomCollider.Collider is BoxCollider2D bottomBox)
                bottomBox.size = new Vector2(bottomBox.size.x - offset, WorldHeight - randOffset - randHoleHeight - offset);

            var newEvent = new WallSpawnEvent { WallData = wallData };
            NewWallSpawned?.Invoke(sender: this, newEvent);

            if (DeleteOldestWallOnSpawn && WallParent.childCount > MaxWalls) {
                WallData oldestWallData = WallParent.GetChild(0).GetComponentInChildren<WallData>();
                var oldEvent = new WallSpawnEvent { WallData = oldestWallData };
                OldWallDeleted?.Invoke(sender: this, oldEvent);
                Destroy(oldestWallData.gameObject);
            }
        }
    }
}
