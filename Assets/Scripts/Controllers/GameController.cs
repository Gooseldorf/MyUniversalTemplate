using System.Collections.Generic;
using Data;
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
    public class GameController : IGameController, IUpdate
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly PlayerController playerController;
        private readonly CityView cityView;
        private readonly EnemiesController enemiesController;
        private readonly WinWindowController winWindowController;
        private readonly LoseWindowController loseWindowController;
        private readonly HUDController hudController;
        private readonly ITimeController timeController;
        private readonly Updater updater;

        private bool isCanGenerateEnemies = false;
        private float spawnDelay = 2; //TODO: Get from levelData
        private float spawnTimer = 0;
        private int generatedEnemyCount = 0;
        private int killEnemyCount = 0;

        private readonly CompositeDisposable disposes = new CompositeDisposable();
        private List<IDispose> gameDisposes;
        
        public GameController(GameStateMachine gameStateMachine, PlayerController playerController, CityView cityView, EnemiesController enemiesController, 
            WinWindowController winWindowController, LoseWindowController loseWindowController, HUDController hudController, ITimeController timeController, Updater updater)
        {
            this.gameStateMachine = gameStateMachine;
            this.playerController = playerController;
            this.cityView = cityView;
            this.enemiesController = enemiesController;
            this.winWindowController = winWindowController;
            this.loseWindowController = loseWindowController;
            this.hudController = hudController;
            this.timeController = timeController;
            this.updater = updater;
        }

        public void Init(List<IDispose> gameDisposes)
        {
            this.gameDisposes = gameDisposes;
            updater.AddUpdatable(this);
            cityView.CityBreachedStream.Subscribe(Breach).AddTo(disposes);
            enemiesController.EnemyKilledStream.Subscribe(CheckWinCondition).AddTo(disposes);
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
            generatedEnemyCount = levelData.NumberOfEnemies;
            killEnemyCount = levelData.NumberOfEnemies;
            isCanGenerateEnemies = true;
            playerController.Reset();
            enemiesController.Reset();
            hudController.Reset();
            hudController.SetLevel(levelData.Index);
            timeController.Unpause();
        }

        public void Update() //TODO: MoveToEnemiesController
        {
            if(!isCanGenerateEnemies) return;
            
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnDelay && generatedEnemyCount >= 0)
            {
                enemiesController.SpawnEnemy();
                spawnTimer = 0;
                generatedEnemyCount--;
                
                if (generatedEnemyCount <= 0)
                {
                    isCanGenerateEnemies = false;
                }
            }
        }

        private void Breach(Collider collider)
        {
            if(collider.transform.parent != null && collider.transform.parent.TryGetComponent(out EnemyView enemy))
                Lose();
        }

        private void CheckWinCondition(Unit unit)
        {
            killEnemyCount--;
            if(killEnemyCount <= 0)
                Win();
        }
        
        private void Win()
        {
            gameStateMachine.Enter<WinState>();
            timeController.Pause();
            winWindowController.Show();
            isCanGenerateEnemies = false;
        }

        private void Lose()
        {
            timeController.Pause();
            gameStateMachine.Enter<LoseState>();
            loseWindowController.Show();
            isCanGenerateEnemies = false;
        }
    }
}