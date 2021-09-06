using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace FlappyClone
{
    public class WallSpawningUpdater : MonoBehaviour
    {
        public const float DefaultSecondsToMaxDifficulty = 300f;
        public static readonly AnimationCurve DefaultMinWallSpacingCurve = AnimationCurve.Constant(0f, 1f, 3f);
        public static readonly AnimationCurve DefaultMaxWallSpacingCurve = AnimationCurve.Constant(0f, 1f, 3f);
        public static readonly AnimationCurve DefaultMinHoleHeightCurve = AnimationCurve.Constant(0f, 1f, 3f);
        public static readonly AnimationCurve DefaultMaxHoleHeightCurve = AnimationCurve.Constant(0f, 1f, 3f);

        public WallLifecycleManager WallLifecycleManager;
        public WallSizer WallSizer;

        public float SecondsToMaxDifficulty = DefaultSecondsToMaxDifficulty;
        public AnimationCurve MinWallSpacingCurve = DefaultMinWallSpacingCurve;
        public AnimationCurve MaxWallSpacingCurve = DefaultMaxWallSpacingCurve;
        public AnimationCurve MinHoleHeightCurve = DefaultMinHoleHeightCurve;
        public AnimationCurve MaxHoleHeightCurve = DefaultMaxHoleHeightCurve;

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
            this.AssertAssociation(WallLifecycleManager, nameof(WallLifecycleManager));
            this.AssertAssociation(WallSizer, nameof(WallSizer));
        }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Update()
        {
            float curveTime = Time.time / SecondsToMaxDifficulty;
            WallLifecycleManager.MinWallSpacing = MinWallSpacingCurve.Evaluate(curveTime);
            WallLifecycleManager.MaxWallSpacing = MaxWallSpacingCurve.Evaluate(curveTime);
            WallSizer.MinHoleHeight = (int)MinHoleHeightCurve.Evaluate(curveTime);
            WallSizer.MaxHoleHeight = (int)MaxHoleHeightCurve.Evaluate(curveTime);
        }
    }
}
