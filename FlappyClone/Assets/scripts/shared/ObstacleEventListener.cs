using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace FlappyClone
{
    public class ObstacleEventListener : MonoBehaviour
    {
        public const string DefaultClearedWallAnimationState = "Base Layer.wall-clear";

        public ObstacleEventSource ObstacleEventSource;

        [Header("Singletons")]
        public WallSpawner WallSpawner;
        public WorldMover WorldMover;
        public Scorekeeper Scorekeeper;
        public HighScoreManager HighScoreManager;
        public Rigidbody2D PlayerRigidbody;
        public GameObject GameoverUiRoot;

        [Header("Wall cleared effects")]
        public ParticleSystem PlayerParticleSystem;
        public string ClearedWallAnimationState = DefaultClearedWallAnimationState;

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Reset()
        {
            ObstacleEventSource = null;

            PlayerParticleSystem = null;
            ClearedWallAnimationState = DefaultClearedWallAnimationState;
        }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Awake()
        {
            this.AssertAssociation(ObstacleEventSource, nameof(ObstacleEventSource));

            ObstacleEventSource.PlayerClearedWall += (sender, e) => handlePlayerClearedWall(e.WallData);

            ObstacleEventSource.PlayerHitObstacle += (sender, e) => handlePlayerHitObstacle();
        }

        private void handlePlayerClearedWall(WallData wallData)
        {
            wallData.TopAnimator.Play(ClearedWallAnimationState);
            wallData.BottomAnimator.Play(ClearedWallAnimationState);
            if (PlayerParticleSystem != null)
                PlayerParticleSystem.Play();

            if (Scorekeeper != null)
                Scorekeeper.Increment();

            if (HighScoreManager != null)
                HighScoreManager.CheckForNewHigh();

            if (WallSpawner != null)
                WallSpawner.Spawn();
        }

        private void handlePlayerHitObstacle()
        {
            if (WorldMover != null)
                WorldMover.enabled = false;

            if (HighScoreManager != null)
                HighScoreManager.Save();

            if (PlayerRigidbody != null)
                PlayerRigidbody.simulated = false;

            if (GameoverUiRoot != null)
                GameoverUiRoot.SetActive(true);
        }


    }
}
