using FlappyClone.Shared;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace FlappyClone.DependencyInjection
{
    public class Bootstrapper : MonoBehaviour
    {
        public ObstacleEventSourceInjected ObstacleEventSource;
        public ObstacleEventListenerInjected ObstacleEventListener;
        public WallLifecycleManagerInjected WallLifecycleManager;
        public WallSizerInjected WallSizer;
        public WorldMoverInjected WorldMover;
        public ScorekeeperInjected Scorekeeper;
        public HighScoreManagerInjected HighScoreManager;
        public WallSpawningUpdaterInjected WallSpawningUpdater;

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Awake()
        {
            this.AssertAssociation(ObstacleEventSource, nameof(ObstacleEventSource));
            this.AssertAssociation(ObstacleEventListener, nameof(ObstacleEventListener));
            this.AssertAssociation(WallLifecycleManager, nameof(WallLifecycleManager));
            this.AssertAssociation(WallSizer, nameof(WallSizer));
            this.AssertAssociation(WorldMover, nameof(WorldMover));
            this.AssertAssociation(Scorekeeper, nameof(Scorekeeper));
            this.AssertAssociation(HighScoreManager, nameof(HighScoreManager));
            this.AssertAssociation(WallSpawningUpdater, nameof(WallSpawningUpdater));

            ObstacleEventSource.Inject(WallLifecycleManager);
            ObstacleEventListener.Inject(ObstacleEventSource, WallLifecycleManager, WorldMover, Scorekeeper, HighScoreManager);
            WallLifecycleManager.Inject(WallSizer);
            HighScoreManager.Inject(Scorekeeper);
            WallSpawningUpdater.Inject(WallLifecycleManager, WallSizer);
        }
    }
}
