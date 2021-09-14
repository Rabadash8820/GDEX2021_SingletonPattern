using FlappyClone.Shared;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace FlappyClone.DependencyInjection
{
    public class WallSpawningUpdaterInjected : MonoBehaviour
    {
        public const float DefaultSecondsToMaxDifficulty = 300f;
        public static readonly AnimationCurve DefaultMinWallSpacingCurve = AnimationCurve.Constant(0f, 1f, 3f);
        public static readonly AnimationCurve DefaultMaxWallSpacingCurve = AnimationCurve.Constant(0f, 1f, 3f);
        public static readonly AnimationCurve DefaultMinHoleHeightCurve = AnimationCurve.Constant(0f, 1f, 3f);
        public static readonly AnimationCurve DefaultMaxHoleHeightCurve = AnimationCurve.Constant(0f, 1f, 3f);

        private IWallLifecycleManager _wallLifecycleManager;
        private IWallSizer _wallSizer;

        public float SecondsToMaxDifficulty = DefaultSecondsToMaxDifficulty;
        public AnimationCurve MinWallSpacingCurve = DefaultMinWallSpacingCurve;
        public AnimationCurve MaxWallSpacingCurve = DefaultMaxWallSpacingCurve;
        public AnimationCurve MinHoleHeightCurve = DefaultMinHoleHeightCurve;
        public AnimationCurve MaxHoleHeightCurve = DefaultMaxHoleHeightCurve;

        public void Inject(IWallLifecycleManager wallLifecycleManager, IWallSizer wallSizer)
        {
            _wallLifecycleManager = wallLifecycleManager;
            _wallSizer = wallSizer;
        }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Reset()
        {
            SecondsToMaxDifficulty = DefaultSecondsToMaxDifficulty;
            MinWallSpacingCurve = DefaultMinWallSpacingCurve;
            MaxWallSpacingCurve = DefaultMaxWallSpacingCurve;
            MinHoleHeightCurve = DefaultMinHoleHeightCurve;
            MaxHoleHeightCurve = DefaultMaxHoleHeightCurve;
        }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Awake()
        {
            this.AssertAssociation(_wallLifecycleManager, nameof(_wallLifecycleManager));
            this.AssertAssociation(_wallSizer, nameof(_wallSizer));
        }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Update()
        {
            float curveTime = Time.time / SecondsToMaxDifficulty;
            _wallLifecycleManager.MinWallSpacing = MinWallSpacingCurve.Evaluate(curveTime);
            _wallLifecycleManager.MaxWallSpacing = MaxWallSpacingCurve.Evaluate(curveTime);
            _wallSizer.MinHoleHeight = (int)MinHoleHeightCurve.Evaluate(curveTime);
            _wallSizer.MaxHoleHeight = (int)MaxHoleHeightCurve.Evaluate(curveTime);
        }
    }
}
