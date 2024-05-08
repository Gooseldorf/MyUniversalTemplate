using UnityEngine;

namespace Controllers
{
    public class TimeController : ITimeController
    {
        public bool IsPaused => isPaused;

        private bool isPaused = false;

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