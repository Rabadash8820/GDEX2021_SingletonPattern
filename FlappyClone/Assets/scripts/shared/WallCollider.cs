using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace FlappyClone
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class WallCollider : MonoBehaviour
    {
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Awake()
        {
            Collider = GetComponent<BoxCollider2D>();
        }


        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Collided?.Invoke(sender: this, collision);
        }

        public BoxCollider2D Collider { get; private set; }

        public event EventHandler<Collision2D> Collided;
    }
}
