using System.Collections.Generic;
using Data;
using Game.Enemy;
using Game.Player;
using Infrastructure.StateMachines.Game;
using Infrastructure.StateMachines.Game.States;
using Interfaces;
using UI.Game.HUD;
using UI.Game.LoseWindow;
using UI.Game.WinWindow;
using UniRx;
using UnityEngine;

namespace Controllers
{
    public interface IGameController : IDispose
    {
        void Play(LevelData levelData);
    }
    
    public class GameController : IGameController
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly PlayerController playerController;
        private readonly CityView cityView;
        private readonly EnemiesController enemiesController;
        private readonly WinWindowController winWindowController;
        private readonly LoseWindowController loseWindowController;
        private readonly HUDController hudController;
        private readonly ITimeController timeController;

        private readonly CompositeDisposable disposes = new CompositeDisposable();
        private List<IDispose> gameDisposes;
        
        public GameController(GameStateMachine gameStateMachine, PlayerController playerController, CityView cityView, EnemiesController enemiesController, 
            WinWindowController winWindowController, LoseWindowController loseWindowController, HUDController hudController, ITimeController timeController)
        {
            this.gameStateMachine = gameStateMachine;
            this.playerController = playerController;
            this.cityView = cityView;
            this.enemiesController = enemiesController;
            this.winWindowController = winWindowController;
            this.loseWindowController = loseWindowController;
            this.hudController = hudController;
            this.timeController = timeController;
        }

        public void Init(List<IDispose> gameDisposes)
        {
            this.gameDisposes = gameDisposes;
            cityView.CityBreachedStream.Subscribe(Breach).AddTo(disposes);
            enemiesController.AllEnemiesKilledStream.Subscribe(OnAllEnemiesDestroyed).AddTo(disposes);
            playerController.Dead += Lose;
        }

        public void Dispose()
        {
            playerController.Dead -= Lose;
            disposes.Dispose();
            enemiesController.Dispose();
            foreach (var dispose in gameDisposes)
            {
                dispose.Dispose();
            }
        }

        public async void Play(LevelData levelData)
        {
            playerController.SetToInitialState();
            enemiesController.Reset();
            await enemiesController.SetupEnemies(levelData);
            hudController.Reset();
            hudController.SetLevel(levelData.Index);
            timeController.Unpause();
        }

        private void Breach(Collider collider)
        {
            if(collider.transform.parent != null && collider.transform.parent.TryGetComponent(out EnemyView enemy))
                Lose();
        }

        private void OnAllEnemiesDestroyed(Unit unit) => Win();

        private void Win()
        {
            gameStateMachine.Enter<WinState>();
            timeController.Pause();
            winWindowController.Show();
        }

        private void Lose()
        {
            timeController.Pause();
            gameStateMachine.Enter<LoseState>();
            loseWindowController.Show();
        }
    }
}