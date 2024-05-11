using System.Threading.Tasks;
using Game.Player;
using Infrastructure.StateMachines.Game;
using Infrastructure.StateMachines.Game.States;

namespace Controllers
{
    public class GameController : IGameController
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly PlayerController playerController;
        private readonly EnemiesController enemiesController;
        
        public GameController(GameStateMachine gameStateMachine, PlayerController playerController, EnemiesController enemiesController)
        {
            this.gameStateMachine = gameStateMachine;
            this.playerController = playerController;
            this.enemiesController = enemiesController;
        }

        public void Init()
        {
            playerController.Dead += Lose;
            enemiesController.AllDead += Win;
        }

        public void Dispose()
        {
            playerController.Dead -= Lose;
            enemiesController.AllDead -= Win;
        }

        public async void Play()
        {
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(2000);
                enemiesController.SpawnEnemy();
            }
        }

        private void Win()
        {
            gameStateMachine.Enter<WinState>();
        }

        private void Lose()
        {
            gameStateMachine.Enter<LoseState>();
        }
    }
}