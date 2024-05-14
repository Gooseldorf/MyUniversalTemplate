using Controllers;
using UniRx;

namespace UI.Game.HUD
{
    public class HUDController : IHUDController 
    {
        private readonly HUDView hudView;
        private readonly EnemiesController enemiesController;
        
        private int currentScore;
        
        private CompositeDisposable disposes = new CompositeDisposable();
        public HUDController(HUDView hudView, EnemiesController enemiesController)
        {
            this.hudView = hudView;
            this.enemiesController = enemiesController;
        }

        public void Init()
        {
            enemiesController.EnemyKilledStream.Subscribe(OnEnemyKilled).AddTo(disposes);
            currentScore = 0;
        }

        private void OnEnemyKilled(Unit unit)
        {
            currentScore++;
            hudView.SetScore(currentScore);
        }

        private void ResetScore()
        {
            currentScore = 0;
            hudView.SetScore(currentScore);
        }

        public void Dispose()
        {
            disposes.Dispose();
        }

        public void Reset()
        {
            hudView.SetLevel(0);
            ResetScore();
        }

        public void SetLevel(int levelDataIndex)
        {
            hudView.SetLevel(levelDataIndex);
        }
    }
}