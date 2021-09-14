using FlappyClone.Shared;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace FlappyClone.DependencyInjection
{
    public class ObstacleEventListenerInjected : MonoBehaviour
    {
        public const string DefaultClearedWallAnimationState = "Base Layer.wall-clear";

        private IObstacleEventSource _obstacleEventSource;
        private IWallLifecycleManager _wallLifecycleManager;
        private IWorldMover _worldMover;
        private IScorekeeper _scorekeeper;
        private IHighScoreManager _highScoreManager;

        [Header("Wall cleared effects")]
        public ParticleSystem PlayerParticleSystem;
        public string ClearedWallAnimationState = DefaultClearedWallAnimationState;

        public void Inject(
            IObstacleEventSource obstacleEventSource,
            IWallLifecycleManager wallLifecycleManager,
            IWorldMover worldMover,
            IScorekeeper scorekeeper,
            IHighScoreManager highScoreManager
        ) {
            _obstacleEventSource = obstacleEventSource;
            _wallLifecycleManager = wallLifecycleManager;
            _worldMover = worldMover;
            _scorekeeper = scorekeeper;
            _highScoreManager = highScoreManager;
        }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Reset()
        {
            _obstacleEventSource = null;
            _wallLifecycleManager = null;
            _worldMover = null;
            _scorekeeper = null;
            _highScoreManager = null;

            PlayerParticleSystem = null;
            ClearedWallAnimationState = DefaultClearedWallAnimationState;
        }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Awake()
        {
            this.AssertAssociation(_obstacleEventSource, nameof(_obstacleEventSource));
            this.AssertAssociation(_wallLifecycleManager, nameof(_wallLifecycleManager));
            this.AssertAssociation(_worldMover, nameof(_worldMover));
            this.AssertAssociation(_scorekeeper, nameof(_scorekeeper));
            this.AssertAssociation(_highScoreManager, nameof(_highScoreManager));
        }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Start()
        {
            _obstacleEventSource.EventPlayerClearedWall += (sender, e) => HandlePlayerClearedWall(e.WallData);
            _obstacleEventSource.EventPlayerHitObstacle += (sender, e) => HandlePlayerHitObstacle();
        }

        internal void HandlePlayerClearedWall(WallData wallData)
        {
            wallData.TopAnimator.Play(ClearedWallAnimationState);
            wallData.BottomAnimator.Play(ClearedWallAnimationState);
            if (PlayerParticleSystem != null)
                PlayerParticleSystem.Play();

            _scorekeeper.Increment();

            _highScoreManager.CheckForNewHigh();

            _wallLifecycleManager.UpdateWalls();
        }

        internal void HandlePlayerHitObstacle()
        {
            _worldMover.IsEnabled = false;

            _highScoreManager.Save();
        }


    }
}
