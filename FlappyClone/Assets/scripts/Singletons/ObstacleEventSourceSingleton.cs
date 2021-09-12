using FlappyClone.Shared;
using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Events;

namespace FlappyClone.Singletons
{
    public class ObstacleEventSourceSingleton : Singleton<ObstacleEventSourceSingleton>
    {
        public const string DefaultPlayerTag= "Player";

        public CollisionTrigger2D GroundCollisionTrigger;
        public string PlayerTag = DefaultPlayerTag;

        public event EventHandler<WallEventArgs> EventPlayerClearedWall;
        public event EventHandler EventPlayerHitObstacle;

        public UnityEvent PlayerClearedWall = new UnityEvent();
        public UnityEvent PlayerHitObstacle = new UnityEvent();

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Reset()
        {
            GroundCollisionTrigger = null;
            PlayerTag = DefaultPlayerTag;
        }

        protected override void OnAwake()
        {
            this.AssertAssociation(GroundCollisionTrigger, nameof(GroundCollisionTrigger));
        }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Start()
        {
            GroundCollisionTrigger.CollisionEnter += (sender, collision) => handlePlayerCollision(collision);

            WallLifecycleManagerSingleton.Instance.WallSpawned += (sender, e) => {
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

            Debug.Log($"Wall '{wallData.gameObject.name}' cleared in frame {Time.frameCount}");
            EventPlayerClearedWall?.Invoke(sender: this, new WallEventArgs { WallData = wallData });
            PlayerClearedWall.Invoke();
        }

    }
}
