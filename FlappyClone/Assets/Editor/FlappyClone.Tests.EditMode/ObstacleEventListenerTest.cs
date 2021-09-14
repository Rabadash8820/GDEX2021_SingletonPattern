using FlappyClone.Shared;
using FlappyClone.Basic;
using NUnit.Framework;
using UnityEngine;


public class ObstacleEventListenerTest
{
    [Test]
    public void PlayerClear_IncrementsScore()
    {
        // ARRANGE
        var obj = new GameObject();

        var wallRootObj = new GameObject();
        WallData wallData = wallRootObj.AddComponent<WallData>();
        wallRootObj.AddComponent<BoxCollider2D>();
        wallData.ClearTrigger = wallRootObj.AddComponent<CollisionTrigger2D>();

        var wallTopObj = new GameObject();
        wallTopObj.transform.parent = wallRootObj.transform;
        wallTopObj.AddComponent<BoxCollider2D>();
        wallData.TopCollider = wallTopObj.AddComponent<CollisionTrigger2D>();
        wallData.TopSprite = wallTopObj.AddComponent<SpriteRenderer>();
        wallData.TopAnimator = wallTopObj.AddComponent<Animator>();

        var wallBottomObj = new GameObject();
        wallBottomObj.transform.parent = wallRootObj.transform;
        wallBottomObj.AddComponent<BoxCollider2D>();
        wallData.BottomCollider = wallBottomObj.AddComponent<CollisionTrigger2D>();
        wallData.BottomSprite = wallBottomObj.AddComponent<SpriteRenderer>();
        wallData.BottomAnimator = wallBottomObj.AddComponent<Animator>();

        ObstacleEventSource obstacleEventSource = obj.AddComponent<ObstacleEventSource>();
        obstacleEventSource.WallLifecycleManager = obj.AddComponent<WallLifecycleManager>();
        obstacleEventSource.GroundCollisionTrigger = obj.AddComponent<CollisionTrigger2D>();

        WallLifecycleManager wallLifecycleManager = obj.AddComponent<WallLifecycleManager>();
        wallLifecycleManager.WallSizer = obj.AddComponent<WallSizer>();
        wallLifecycleManager.WallPrefab = wallRootObj;
        wallLifecycleManager.WallParent = obj.transform;
        wallLifecycleManager.FillBasis = obj.transform;

        WorldMover worldMover = obj.AddComponent<WorldMover>();
        worldMover.WorldRoot = obj.transform;
        worldMover.GroundSprite = obj.AddComponent<SpriteRenderer>();

        Scorekeeper scorekeeper = obj.AddComponent<Scorekeeper>();

        HighScoreManager highScoreManager = obj.AddComponent<HighScoreManager>();
        highScoreManager.Scorekeeper = scorekeeper;

        ObstacleEventListener obstacleEventListener = obj.AddComponent<ObstacleEventListener>();
        obstacleEventListener.ObstacleEventSource = obstacleEventSource;
        obstacleEventListener.WallLifecycleManager = wallLifecycleManager;
        obstacleEventListener.WorldMover = worldMover;
        obstacleEventListener.Scorekeeper = scorekeeper;
        obstacleEventListener.HighScoreManager = highScoreManager;

        // ACT
        obstacleEventListener.HandlePlayerClearedWall(wallData);

        // ASSERT
        Assert.That(scorekeeper.CurrentScore, Is.EqualTo(scorekeeper.InitialScore + 1));
    }
}
