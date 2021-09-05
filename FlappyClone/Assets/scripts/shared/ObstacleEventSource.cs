using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace FlappyClone
{
    public class ObstacleEventSource : MonoBehaviour
    {
        public const string DefaultPlayerTag= "Player";

        public WallLifecycleManager WallLifecycleManager;
        public CollisionTrigger2D GroundCollisionTrigger;
        public string PlayerTag = DefaultPlayerTag;

        public event EventHandler<WallEventArgs> PlayerClearedWall;
        public event EventHandler PlayerHitObstacle;

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Reset()
        {
            WallLifecycleManager = null;
            GroundCollisionTrigger = null;
            PlayerTag = DefaultPlayerTag;

            PlayerClearedWall = null;
            PlayerHitObstacle = null;
        }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Awake()
        {
            this.AssertAssociation(WallLifecycleManager, nameof(WallLifecycleManager));
            this.AssertAssociation(GroundCollisionTrigger, nameof(GroundCollisionTrigger));

            GroundCollisionTrigger.CollisionEnter += (sender, collision) => handlePlayerCollision(collision);

            WallLifecycleManager.WallSpawned += (sender, e) => {
                WallData wallData = e.WallData;
                wallData.TopCollider.CollisionEnter += (sender, collision) => handlePlayerCollision(collision);
                wallData.ClearTrigger.TriggerExit += (sender, collider) => handlePlayerClear(wallData, collider);
                wallData.BottomCollider.CollisionEnter += (sender, collision) => handlePlayerCollision(collision);
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

            PlayerClearedWall?.Invoke(sender: this, new WallEventArgs { WallData = wallData });
        }

    }
}
