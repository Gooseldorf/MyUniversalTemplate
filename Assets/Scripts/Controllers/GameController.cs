using System.Threading.Tasks;
using Game.Player;
using Infrastructure.StateMachines.Game;
using Infrastructure.StateMachines.Game.States;
using UniRx;
using UnityEngine;

namespace Controllers
{
    public class GameController : IGameController
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly PlayerController playerController;
        private readonly CityView cityView;
        private readonly EnemiesController enemiesController;
        
        private CompositeDisposable disposes = new CompositeDisposable();
        
        public GameController(GameStateMachine gameStateMachine, PlayerController playerController, CityView cityView, EnemiesController enemiesController)
        {
            this.gameStateMachine = gameStateMachine;
            this.playerController = playerController;
            this.cityView = cityView;
            this.enemiesController = enemiesController;
        }

        public void Init()
        {
            cityView.CityBreachedStream.Subscribe(Breach).AddTo(disposes);
            playerController.Dead += Lose;
            enemiesController.AllDead += Win;
        }

        public void Dispose()
        {
            playerController.Dead -= Lose;
            enemiesController.AllDead -= Win;
            disposes.Dispose();
        }

        public async void Play()
        {
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(2000);
                enemiesController.SpawnEnemy();
            }
        }
        
        private void Breach(Collider collider)
        {
            if(collider.transform.parent != null && collider.transform.parent.TryGetComponent(out EnemyView enemy))
                Lose();
        }

        private void Win()
        {
            gameStateMachine.Enter<WinState>();
        }

        private void Lose()
        {
            gameStateMachine.Enter<LoseState>();
            Debug.Log("Lose");
        }
    }
}