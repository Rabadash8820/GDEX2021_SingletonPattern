using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace FlappyClone.Shared
{
    public class BirdMover : MonoBehaviour
    {
        public const string DefaultJumpButtonName = "Jump";
        public const float DefaultForceMagnitude = 2f;
        public const ForceMode2D DefaultForceMode = ForceMode2D.Impulse;

        public Rigidbody2D BirdRigidbody;
        public string JumpButtonName = DefaultJumpButtonName;
        public float ForceMagnitude = DefaultForceMagnitude;
        public ForceMode2D ForceMode = DefaultForceMode;

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Reset()
        {
            BirdRigidbody = null;
            JumpButtonName = DefaultJumpButtonName;
            ForceMagnitude = DefaultForceMagnitude;
            ForceMode = DefaultForceMode;
        }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Awake()
        {
            this.AssertAssociation(BirdRigidbody, nameof(BirdRigidbody));
        }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Update()
        {
            if (Input.GetButtonDown(JumpButtonName))
                BirdRigidbody.AddForce(ForceMagnitude * BirdRigidbody.mass * Vector2.up, ForceMode);
        }
    }
}
