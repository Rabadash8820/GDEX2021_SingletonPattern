using FlappyClone.Shared;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace FlappyClone.Basic
{
    public class ObstacleEventListener : MonoBehaviour
    {
        public const string DefaultClearedWallAnimationState = "Base Layer.wall-clear";

        [Header("Singletons")]
        public ObstacleEventSource ObstacleEventSource;
        public WallLifecycleManager WallLifecycleManager;
        public WorldMover WorldMover;
        public Scorekeeper Scorekeeper;
        public HighScoreManager HighScoreManager;

        [Header("Wall cleared effects")]
        public ParticleSystem PlayerParticleSystem;
        public string ClearedWallAnimationState = DefaultClearedWallAnimationState;

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Reset()
        {
            ObstacleEventSource = null;
            WallLifecycleManager = null;
            WorldMover = null;
            Scorekeeper = null;
            HighScoreManager = null;

            PlayerParticleSystem = null;
            ClearedWallAnimationState = DefaultClearedWallAnimationState;
        }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Awake()
        {
            this.AssertAssociation(ObstacleEventSource, nameof(ObstacleEventSource));
            this.AssertAssociation(WallLifecycleManager, nameof(WallLifecycleManager));
            this.AssertAssociation(WorldMover, nameof(WorldMover));
            this.AssertAssociation(Scorekeeper, nameof(Scorekeeper));
            this.AssertAssociation(HighScoreManager, nameof(HighScoreManager));

            ObstacleEventSource.EventPlayerClearedWall += (sender, e) => HandlePlayerClearedWall(e.WallData);

            ObstacleEventSource.EventPlayerHitObstacle += (sender, e) => HandlePlayerHitObstacle();
        }

        internal void HandlePlayerClearedWall(WallData wallData)
        {
            wallData.TopAnimator.Play(ClearedWallAnimationState);
            wallData.BottomAnimator.Play(ClearedWallAnimationState);
            if (PlayerParticleSystem != null)
                PlayerParticleSystem.Play();

            Scorekeeper.Increment();

            HighScoreManager.CheckForNewHigh();

            WallLifecycleManager.UpdateWalls();
        }

        internal void HandlePlayerHitObstacle()
        {
            WorldMover.enabled = false;

            HighScoreManager.Save();
        }


    }
}
