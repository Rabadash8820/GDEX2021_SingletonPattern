namespace FlappyClone.DependencyInjection
{
    public interface IHighScoreManager
    {
        void CheckForNewHigh();
        void Delete();
        void Save();
    }
}