using FlappyClone.Shared;
using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Events;

namespace FlappyClone.DependencyInjection
{
    public class ObstacleEventSourceInjected : MonoBehaviour, IObstacleEventSource
    {
        public const string DefaultPlayerTag = "Player";

        private IWallLifecycleManager _wallLifecycleManager;

        public CollisionTrigger2D GroundCollisionTrigger;
        public string PlayerTag = DefaultPlayerTag;

        public event EventHandler<WallEventArgs> EventPlayerClearedWall;
        public event EventHandler EventPlayerHitObstacle;

        public UnityEvent PlayerClearedWall = new UnityEvent();
        public UnityEvent PlayerHitObstacle = new UnityEvent();

        public void Inject(IWallLifecycleManager wallLifecycleManager)
        {
            _wallLifecycleManager = wallLifecycleManager;
        }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Reset()
        {
            _wallLifecycleManager = null;
            GroundCollisionTrigger = null;
            PlayerTag = DefaultPlayerTag;
        }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Awake()
        {
            this.AssertAssociation(_wallLifecycleManager, nameof(_wallLifecycleManager));
            this.AssertAssociation(GroundCollisionTrigger, nameof(GroundCollisionTrigger));
        }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Start()
        {
            GroundCollisionTrigger.CollisionEnter += (sender, collision) => handlePlayerCollision(collision);

            _wallLifecycleManager.WallSpawned += (sender, e) => {
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
