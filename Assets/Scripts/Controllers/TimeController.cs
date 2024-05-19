using UnityEngine;

namespace Controllers
{
    public interface ITimeController
    {
        bool IsPaused { get; }
        void Pause();
        void Unpause();
    }
    
    public class TimeController : ITimeController
    {
        public bool IsPaused => isPaused;

        private bool isPaused = true;

        public void Pause()
        {
            if (isPaused) return;

            Time.timeScale = 0;
            isPaused = true;
        }

        public void Unpause()
        {
            if (!isPaused) return;

            Time.timeScale = 1;
            isPaused = false;
        }
    }
}