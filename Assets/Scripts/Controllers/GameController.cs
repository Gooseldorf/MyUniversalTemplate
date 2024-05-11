using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Game.Player;
using Infrastructure.StateMachines.Game;
using Infrastructure.StateMachines.Game.States;
using UI.Game.LoseWindow;
using UI.Game.WinWindow;
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
        private readonly WinWindowController winWindowController;
        private readonly LoseWindowController loseWindowController;
        private readonly TimeController timeController;

        private UniTask PlayTask;
        private CancellationTokenSource cts;

        private CompositeDisposable disposes = new CompositeDisposable();
        
        public GameController(GameStateMachine gameStateMachine, PlayerController playerController, CityView cityView, EnemiesController enemiesController, 
            WinWindowController winWindowController, LoseWindowController loseWindowController, TimeController timeController)
        {
            this.gameStateMachine = gameStateMachine;
            this.playerController = playerController;
            this.cityView = cityView;
            this.enemiesController = enemiesController;
            this.winWindowController = winWindowController;
            this.loseWindowController = loseWindowController;
            this.timeController = timeController;
        }

        public void Init()
        {
            cityView.CityBreachedStream.Subscribe(Breach).AddTo(disposes);
            playerController.Dead += Lose;
        }

        public void Dispose()
        {
            playerController.Dead -= Lose;
            disposes.Dispose();
        }

        public async void Play()
        {
            cts = new CancellationTokenSource();
            PlayTask = GetPlayTask(cts.Token);
            await PlayTask;
        }
        
        private async UniTask GetPlayTask(CancellationToken cancellationToken)
        {
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    await Task.Delay(2000, cancellationToken);
                    enemiesController.SpawnEnemy();
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Play task was cancelled");
            }
        }
        
        private void Breach(Collider collider)
        {
            if(collider.transform.parent != null && collider.transform.parent.TryGetComponent(out EnemyView enemy))
                Lose();
        }

        private void Win()
        {
            cts.Cancel();
            gameStateMachine.Enter<WinState>();
            timeController.Pause();
            winWindowController.Show();
        }

        private void Lose()
        {
            cts.Cancel();
            timeController.Pause();
            gameStateMachine.Enter<LoseState>();
            loseWindowController.Show();
            Debug.Log("Lose");
        }
    }
}