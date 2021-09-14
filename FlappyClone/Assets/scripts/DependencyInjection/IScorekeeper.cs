namespace FlappyClone.DependencyInjection
{
    public interface IScorekeeper
    {
        int CurrentScore { get; }
        void Increment();
    }
}
