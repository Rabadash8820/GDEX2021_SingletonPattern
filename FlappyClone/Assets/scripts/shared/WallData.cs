using UnityEngine;

namespace FlappyClone
{
    public class WallData : MonoBehaviour
    {
        public WallCollider TopCollider;
        public SpriteRenderer TopSprite;
        public WallClearTrigger ClearTrigger;
        public WallCollider BottomCollider;
        public SpriteRenderer BottomSprite;
    }
}
