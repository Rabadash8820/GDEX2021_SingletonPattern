using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Events;

namespace FlappyClone
{
    public class WallListener : MonoBehaviour
    {
        public WallSpawner WallSpawner;
        public string PlayerTag = "Player";

        public UnityEvent PlayerClearedWall = new UnityEvent();
        public UnityEvent PlayerHitWall = new UnityEvent();


        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Awake()
        {
            this.AssertAssociation(WallSpawner, nameof(WallSpawner));

            WallSpawner.NewWallSpawned += (sender, e) => {
                e.WallData.TopCollider.Collided += (sender, collision) => handlePlayerCollision(collision);
                e.WallData.ClearTrigger.Cleared += (sender, collider) => handlePlayerClear(collider);
                e.WallData.BottomCollider.Collided += (sender, collision) => handlePlayerCollision(collision);
            };
        }

        private void handlePlayerCollision(Collision2D collision)
        {
            if (!collision.collider.attachedRigidbody.CompareTag(PlayerTag))
                return;

            PlayerHitWall.Invoke();
        }

        private void handlePlayerClear(Collider2D collider)
        {
            if (!collider.attachedRigidbody.CompareTag(PlayerTag))
                return;

            PlayerClearedWall.Invoke();
        }

    }
}
