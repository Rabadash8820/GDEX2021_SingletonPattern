using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace FlappyClone.Shared
{
    [RequireComponent(typeof(Collider2D))]
    public class CollisionTrigger2D : MonoBehaviour
    {
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Awake() => Collider = GetComponent<Collider2D>();

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void OnCollisionEnter2D(Collision2D collision) => CollisionEnter?.Invoke(sender: this, collision);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void OnCollisionExit2D(Collision2D collision) => CollisionExit?.Invoke(sender: this, collision);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void OnTriggerEnter2D(Collider2D collision) => TriggerEnter?.Invoke(sender: this, collision);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]

        private void OnTriggerExit2D(Collider2D collision) => TriggerExit?.Invoke(sender: this, collision);

        public Collider2D Collider { get; private set; }

        public event EventHandler<Collision2D> CollisionEnter;
        public event EventHandler<Collision2D> CollisionExit;

        public event EventHandler<Collider2D> TriggerEnter;
        public event EventHandler<Collider2D> TriggerExit;
    }
}
