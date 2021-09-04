using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace FlappyClone
{
    public class ObstacleEventSource : MonoBehaviour
    {
        public const string DefaultPlayerTag= "Player";

        public WallSpawner WallSpawner;
        public CollisionTrigger2D GroundCollisionTrigger;
        public string PlayerTag = DefaultPlayerTag;

        public event EventHandler<WallEvent> PlayerClearedWall;
        public event EventHandler PlayerHitObstacle;

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Reset()
        {
            WallSpawner = null;
            GroundCollisionTrigger = null;
            PlayerTag = DefaultPlayerTag;

            PlayerClearedWall = null;
            PlayerHitObstacle = null;
        }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Awake()
        {
            this.AssertAssociation(WallSpawner, nameof(WallSpawner));
            this.AssertAssociation(GroundCollisionTrigger, nameof(GroundCollisionTrigger));

            GroundCollisionTrigger.CollisionEnter += (sender, collision) => handlePlayerCollision(collision);

            WallSpawner.NewWallSpawned += (sender, e) => {
                e.WallData.TopCollider.CollisionEnter += (sender, collision) => handlePlayerCollision(collision);
                e.WallData.ClearTrigger.TriggerExit += (sender, collider) => handlePlayerClear(e.WallData, collider);
                e.WallData.BottomCollider.CollisionEnter += (sender, collision) => handlePlayerCollision(collision);
            };
        }

        private void handlePlayerCollision(Collision2D collision)
        {
            if (!collision.collider.attachedRigidbody.CompareTag(PlayerTag))
                return;

            PlayerHitObstacle?.Invoke(sender: this, EventArgs.Empty);
        }

        private void handlePlayerClear(WallData wallData, Collider2D collider)
        {
            if (!collider.attachedRigidbody.CompareTag(PlayerTag))
                return;

            PlayerClearedWall?.Invoke(sender: this, new WallEvent { WallData = wallData });
        }

    }
}
