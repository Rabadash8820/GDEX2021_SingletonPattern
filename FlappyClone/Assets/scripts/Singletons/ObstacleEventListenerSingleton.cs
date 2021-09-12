using FlappyClone.Shared;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace FlappyClone.Singletons
{
    public class ObstacleEventListenerSingleton : Singleton<ObstacleEventListenerSingleton>
    {
        public const string DefaultClearedWallAnimationState = "Base Layer.wall-clear";

        [Header("Wall cleared effects")]
        public ParticleSystem PlayerParticleSystem;
        public string ClearedWallAnimationState = DefaultClearedWallAnimationState;

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Reset()
        {
            PlayerParticleSystem = null;
            ClearedWallAnimationState = DefaultClearedWallAnimationState;
        }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Start()
        {
            ObstacleEventSourceSingleton.Instance.EventPlayerClearedWall += (sender, e) => HandlePlayerClearedWall(e.WallData);

            ObstacleEventSourceSingleton.Instance.EventPlayerHitObstacle += (sender, e) => HandlePlayerHitObstacle();
        }

        internal void HandlePlayerClearedWall(WallData wallData)
        {
            wallData.TopAnimator.Play(ClearedWallAnimationState);
            wallData.BottomAnimator.Play(ClearedWallAnimationState);
            if (PlayerParticleSystem != null)
                PlayerParticleSystem.Play();

            ScorekeeperSingleton.Instance.Increment();

            HighScoreManagerSingleton.Instance.CheckForNewHigh();

            WallLifecycleManagerSingleton.Instance.UpdateWalls();
        }

        internal void HandlePlayerHitObstacle()
        {
            WorldMoverSingleton.Instance.enabled = false;

            HighScoreManagerSingleton.Instance.Save();
        }


    }
}
