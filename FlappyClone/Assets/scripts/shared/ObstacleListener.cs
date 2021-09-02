using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Events;

namespace FlappyClone
{
    public class ObstacleListener : MonoBehaviour
    {
        public WallSpawner WallSpawner;
        public CollisionTrigger2D GroundCollisionTrigger;
        public string PlayerTag = "Player";

        public UnityEvent PlayerClearedWall = new UnityEvent();
        public UnityEvent PlayerHitObstacle = new UnityEvent();


        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Awake()
        {
            this.AssertAssociation(WallSpawner, nameof(WallSpawner));
            this.AssertAssociation(GroundCollisionTrigger, nameof(GroundCollisionTrigger));

            GroundCollisionTrigger.CollisionEnter += (sender, collision) => handlePlayerCollision(collision);

            WallSpawner.NewWallSpawned += (sender, e) => {
                e.WallData.TopCollider.CollisionEnter += (sender, collision) => handlePlayerCollision(collision);
                e.WallData.ClearTrigger.TriggerExit += (sender, collider) => handlePlayerClear(collider);
                e.WallData.BottomCollider.CollisionEnter += (sender, collision) => handlePlayerCollision(collision);
            };
        }

        private void handlePlayerCollision(Collision2D collision)
        {
            if (!collision.collider.attachedRigidbody.CompareTag(PlayerTag))
                return;

            PlayerHitObstacle.Invoke();
        }

        private void handlePlayerClear(Collider2D collider)
        {
            if (!collider.attachedRigidbody.CompareTag(PlayerTag))
                return;

            PlayerClearedWall.Invoke();
        }

    }
}
