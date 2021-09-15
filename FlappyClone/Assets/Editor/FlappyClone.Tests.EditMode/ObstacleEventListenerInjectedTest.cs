using FlappyClone.Shared;
using NUnit.Framework;
using UnityEngine;
using Moq;
using FlappyClone.DependencyInjection;

public class ObstacleEventListenerInjectedTest
{
    [Test]
    public void PlayerClear_IncrementsScore()
    {
        // ARRANGE
        var wallRootObj = new GameObject();
        WallData wallData = wallRootObj.AddComponent<WallData>();

        var wallTopObj = new GameObject();
        wallTopObj.transform.parent = wallRootObj.transform;
        wallData.TopAnimator = wallTopObj.AddComponent<Animator>();

        var wallBottomObj = new GameObject();
        wallBottomObj.transform.parent = wallRootObj.transform;
        wallData.BottomAnimator = wallBottomObj.AddComponent<Animator>();

        var scorekeeper = new Mock<IScorekeeper>();

        ObstacleEventListenerInjected obstacleEventListener = new GameObject().AddComponent<ObstacleEventListenerInjected>();
        obstacleEventListener.Inject(
            Mock.Of<IObstacleEventSource>(),
            Mock.Of<IWallLifecycleManager>(),
            Mock.Of<IWorldMover>(),
            scorekeeper.Object,
            Mock.Of<IHighScoreManager>()
        );

        // ACT
        obstacleEventListener.HandlePlayerClearedWall(wallData);

        // ASSERT
        scorekeeper.Verify(x => x.Increment(), Times.Once);
    }
}
