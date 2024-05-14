using System.Collections.Generic;
using Data;
using Game.Player;
using Infrastructure.StateMachines.Game;
using Infrastructure.StateMachines.Game.States;
using Interfaces;
using UI.Game.LoseWindow;
using UI.Game.WinWindow;
using UniRx;
using UnityEngine;

namespace Controllers
{
    public class GameController : IGameController, IUpdate
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly PlayerController playerController;
        private readonly CityView cityView;
        private readonly EnemiesController enemiesController;
        private readonly WinWindowController winWindowController;
        private readonly LoseWindowController loseWindowController;
        private readonly ITimeController timeController;
        private readonly Updater updater;

        private bool isPlaying = false;
        private float spawnDelay = 2; //TODO: Get from levelData
        private float spawnTimer = 0;
        private int enemyCount = 0;

        private readonly CompositeDisposable disposes = new CompositeDisposable();
        private List<IDispose> gameDisposes;
        
        public GameController(GameStateMachine gameStateMachine, PlayerController playerController, CityView cityView, EnemiesController enemiesController, 
            WinWindowController winWindowController, LoseWindowController loseWindowController, ITimeController timeController, Updater updater)
        {
            this.gameStateMachine = gameStateMachine;
            this.playerController = playerController;
            this.cityView = cityView;
            this.enemiesController = enemiesController;
            this.winWindowController = winWindowController;
            this.loseWindowController = loseWindowController;
            this.timeController = timeController;
            this.updater = updater;
        }

        public void Init(List<IDispose> gameDisposes)
        {
            this.gameDisposes = gameDisposes;
            updater.AddUpdatable(this);
            cityView.CityBreachedStream.Subscribe(Breach).AddTo(disposes);
            playerController.Dead += Lose;
        }

        public void Dispose()
        {
            playerController.Dead -= Lose;
            disposes.Dispose();
            foreach (var dispose in gameDisposes)
            {
                dispose.Dispose();
            }
        }

        public void Play(LevelData levelData)
        {
            spawnTimer = 0;
            spawnDelay = levelData.EnemySpawnDelay;
            enemyCount = levelData.NumberOfEnemies;
            isPlaying = true;
            playerController.Reset();
            enemiesController.Reset();
            timeController.Unpause();
        }

        public void Update() //TODO: MoveToEnemiesController
        {
            if(!isPlaying) return;
            
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnDelay && enemyCount >= 0)
            {
                enemiesController.SpawnEnemy();
                spawnTimer = 0;
                enemyCount--;
                
                if (enemyCount < 0)
                {
                    Win();
                }
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
            timeController.Pause();
            winWindowController.Show();
            isPlaying = false;
        }

        private void Lose()
        {
            timeController.Pause();
            gameStateMachine.Enter<LoseState>();
            loseWindowController.Show();
            isPlaying = false;
        }
    }
}