namespace Controllers
{
    public interface ITimeController
    {
        void Pause();
        void Unpause();
        bool IsPaused { get; }
    }
}