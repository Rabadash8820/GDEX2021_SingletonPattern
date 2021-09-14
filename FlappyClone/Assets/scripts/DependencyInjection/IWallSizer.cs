using FlappyClone.Shared;

namespace FlappyClone.DependencyInjection
{
    public interface IWallSizer
    {
        int MinHoleHeight { get; set; }
        int MaxHoleHeight { get; set; }
        void Resize(ref WallData wallData);
    }
}
