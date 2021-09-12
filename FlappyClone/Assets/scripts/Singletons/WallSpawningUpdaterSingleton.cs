using FlappyClone.Shared;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace FlappyClone.Singletons
{
    public class WallSpawningUpdaterSingleton : Singleton<WallSpawningUpdaterSingleton>
    {
        public const float DefaultSecondsToMaxDifficulty = 300f;
        public static readonly AnimationCurve DefaultMinWallSpacingCurve = AnimationCurve.Constant(0f, 1f, 3f);
        public static readonly AnimationCurve DefaultMaxWallSpacingCurve = AnimationCurve.Constant(0f, 1f, 3f);
        public static readonly AnimationCurve DefaultMinHoleHeightCurve = AnimationCurve.Constant(0f, 1f, 3f);
        public static readonly AnimationCurve DefaultMaxHoleHeightCurve = AnimationCurve.Constant(0f, 1f, 3f);

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
        private void Update()
        {
            float curveTime = Time.time / SecondsToMaxDifficulty;
            WallLifecycleManagerSingleton.Instance.MinWallSpacing = MinWallSpacingCurve.Evaluate(curveTime);
            WallLifecycleManagerSingleton.Instance.MaxWallSpacing = MaxWallSpacingCurve.Evaluate(curveTime);
            WallSizerSingleton.Instance.MinHoleHeight = (int)MinHoleHeightCurve.Evaluate(curveTime);
            WallSizerSingleton.Instance.MaxHoleHeight = (int)MaxHoleHeightCurve.Evaluate(curveTime);
        }
    }
}
