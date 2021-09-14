using FlappyClone.Shared;
using System;

namespace FlappyClone.DependencyInjection
{
    public interface IObstacleEventSource
    {
        event EventHandler<WallEventArgs> EventPlayerClearedWall;
        event EventHandler EventPlayerHitObstacle;
    }
}
