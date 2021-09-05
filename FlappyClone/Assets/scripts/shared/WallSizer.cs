using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace FlappyClone
{
    public class WallSizer : MonoBehaviour
    {
        public const int DefaultMinHoleHeight = 2;
        public const int DefaultMaxHoleHeight = 8;
        public const float DefaultWallColliderOffset = 0.1f;
        public const int DefaultWorldHeight = 10;

        public int MinHolelHeight = DefaultMinHoleHeight;
        public int MaxHoleHeight = DefaultMaxHoleHeight;
        public float WallColliderOffset = DefaultWallColliderOffset;
        public int WorldHeight = DefaultWorldHeight;

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Reset()
        {
            MinHolelHeight = DefaultMinHoleHeight;
            MaxHoleHeight = DefaultMaxHoleHeight;
            WallColliderOffset = DefaultWallColliderOffset;
            WorldHeight = DefaultWorldHeight;
        }

        public void Resize(ref WallData wallData)
        {
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
        }
    }
}
