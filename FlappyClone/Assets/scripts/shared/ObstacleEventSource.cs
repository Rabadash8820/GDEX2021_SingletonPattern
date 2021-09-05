using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Events;

namespace FlappyClone
{
    public class ObstacleEventSource : MonoBehaviour
    {
        public const string DefaultPlayerTag= "Player";

        public WallLifecycleManager WallLifecycleManager;
        public CollisionTrigger2D GroundCollisionTrigger;
        public string PlayerTag = DefaultPlayerTag;

        public event EventHandler<WallEventArgs> EventPlayerClearedWall;
        public event EventHandler EventPlayerHitObstacle;

        public UnityEvent PlayerClearedWall = new UnityEvent();
        public UnityEvent PlayerHitObstacle = new UnityEvent();

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Reset()
        {
            WallLifecycleManager = null;
            GroundCollisionTrigger = null;
            PlayerTag = DefaultPlayerTag;

            EventPlayerClearedWall = null;
            EventPlayerHitObstacle = null;

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

            EventPlayerHitObstacle?.Invoke(sender: this, EventArgs.Empty);
            PlayerHitObstacle.Invoke();
        }

        private void handlePlayerClear(WallData wallData, Collider2D collider)
        {
            if (!collider.attachedRigidbody.CompareTag(PlayerTag))
                return;

            EventPlayerClearedWall?.Invoke(sender: this, new WallEventArgs { WallData = wallData });
            PlayerClearedWall.Invoke();
        }

    }
}
