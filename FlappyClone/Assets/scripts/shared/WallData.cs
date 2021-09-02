using UnityEngine;

namespace FlappyClone
{
    public class WallData : MonoBehaviour
    {
        public CollisionTrigger2D TopCollider;
        public SpriteRenderer TopSprite;
        public CollisionTrigger2D ClearTrigger;
        public CollisionTrigger2D BottomCollider;
        public SpriteRenderer BottomSprite;
    }
}
