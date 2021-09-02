using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace FlappyClone
{
    public class WorldMover : MonoBehaviour
    {
        public static readonly float DefaultSpeed = -5f;

        private float _totalGroundDelta = 0f;
        private SpriteRenderer _groundSprite1;
        private SpriteRenderer _groundSprite2;

        public Transform WorldRoot;
        public float Speed = DefaultSpeed;
        public SpriteRenderer GroundSprite;

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Reset()
        {
            WorldRoot = null;
            Speed = DefaultSpeed;
            GroundSprite = null;
        }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Awake()
        {
            this.AssertAssociation(WorldRoot, nameof(WorldRoot));
            this.AssertAssociation(WorldRoot, nameof(GroundSprite));
        }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Start()
        {
            SpriteRenderer groundClone = Instantiate(GroundSprite, -Mathf.Sign(Speed) * GroundSprite.size.x * Vector2.right, Quaternion.identity, GroundSprite.transform.parent);
            _groundSprite1 = GroundSprite;
            _groundSprite2 = groundClone;
        }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Update()
        {
            float delta = Speed * Time.deltaTime;
            WorldRoot.Translate(delta * Vector2.right);

            _totalGroundDelta += Mathf.Abs(delta);
            float groundWidth = _groundSprite1.size.x;
            if (_totalGroundDelta >= groundWidth) {
                _totalGroundDelta -= groundWidth;

                _groundSprite1.transform.position -= Mathf.Sign(Speed) * 2f * groundWidth * Vector3.right;

                SpriteRenderer tempSprite = _groundSprite1;
                _groundSprite1 = _groundSprite2;
                _groundSprite2 = tempSprite;
            }
        }
    }
}
