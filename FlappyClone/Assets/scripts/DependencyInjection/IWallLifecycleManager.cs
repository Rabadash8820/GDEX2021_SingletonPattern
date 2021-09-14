using FlappyClone.Shared;
using System;

namespace FlappyClone.DependencyInjection
{
    public interface IWallLifecycleManager
    {
        event EventHandler<WallEventArgs> WallSpawned;

        float MinWallSpacing { get; set; }
        float MaxWallSpacing { get; set; }
        void UpdateWalls();
    }
}
