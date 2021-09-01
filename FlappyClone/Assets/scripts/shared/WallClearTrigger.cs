using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace FlappyClone
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class WallClearTrigger : MonoBehaviour
    {
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Awake()
        {
            Trigger = GetComponent<BoxCollider2D>();
        }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void OnTriggerExit2D(Collider2D collision)
        {
            Cleared?.Invoke(sender: this, collision);
        }

        public BoxCollider2D Trigger { get; private set; }

        public event EventHandler<Collider2D> Cleared;
    }
}
