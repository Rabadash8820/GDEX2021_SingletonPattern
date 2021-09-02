using UnityEngine;

namespace FlappyClone
{
    public class WallData : MonoBehaviour
    {
        public CollisionTrigger2D ClearTrigger;

        [Header("Top Part")]
        public CollisionTrigger2D TopCollider;
        public SpriteRenderer TopSprite;
        public Animator TopAnimator;

        [Header("Bottom Part")]
        public CollisionTrigger2D BottomCollider;
        public SpriteRenderer BottomSprite;
        public Animator BottomAnimator;
    }
}
