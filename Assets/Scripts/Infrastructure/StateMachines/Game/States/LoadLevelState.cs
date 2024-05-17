using System.Collections.Generic;
using System.Threading.Tasks;
using Audio;
using Controllers;
using Cysharp.Threading.Tasks;
using Data;
using Game.Enemy;
using Game.Environment;
using Game.Player;
using Game.Projectiles;
using Game.Spawners;
using Game.VFX.Explosion;
using Game.Weapon;
using Game.Weapon.Laser;
using Infrastructure.AssetManagement;
using Infrastructure.DI;
using Infrastructure.Factories;
using Infrastructure.Services.Input;
using Interfaces;
using UI.Game;
using UI.Game.HUD;
using UI.Game.LoseWindow;
using UI.Game.PauseWindow;
using UI.Game.WinWindow;
using UnityEngine;

namespace Infrastructure.StateMachines.Game.States
{
    public class LoadLevelState : IStateWithArg<int>
    {
        private readonly GameStateMachine gameStateMachine;
        private List<IDispose> disposes = new List<IDispose>();
        
        public LoadLevelState(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        public async void Enter(int levelIndex)
        {
            GameInstaller gameInstaller = Object.FindObjectOfType<GameInstaller>();
            ITimeController timeController = gameInstaller.Resolve<ITimeController>();
            IAudioManager audioManager = gameInstaller.Resolve<IAudioManager>();
            IAssetProvider assetProvider = gameInstaller.Resolve<IAssetProvider>();
            Updater updater = await CreateUpdater(assetProvider);
            
            //Load LevelData by index
            LevelData levelData = await assetProvider.LoadAddressable<LevelData>($"Level_{levelIndex}");
            PlayerData playerData = await assetProvider.LoadAddressable<PlayerData>("PlayerData");
            
            //CreateEnvironment
            ILevelFactory levelFactory = gameInstaller.Resolve<ILevelFactory>();
            await levelFactory.WarmUpIfNeeded();
            EnvironmentView environmentView = SetupEnvironment(levelFactory);
            Bounds gameFieldBounds = environmentView.GetGameFieldBounds();
            
            CityView city = SetupCity(levelFactory);
            
            LaserProjectileFactory laserProjectileFactory = new LaserProjectileFactory(assetProvider);
            await laserProjectileFactory.WarmUpIfNeeded();
            GameObject projectileReleaserObj = await assetProvider.InstantiateAddressable("ProjectileReleaser");
            ProjectileReleaser projectileReleaser = projectileReleaserObj.GetComponent<ProjectileReleaser>();
            LaserProjectilePool laserProjectilePool = new LaserProjectilePool(laserProjectileFactory, projectileReleaser, 20);
            
            //CreatePlayer
            IInputService inputService = gameInstaller.Resolve<IInputService>();
            IPlayerFactory playerFactory = gameInstaller.Resolve<IPlayerFactory>();
            IWeaponFactory weaponFactory = gameInstaller.Resolve<IWeaponFactory>();
            await weaponFactory.WarmUpIfNeeded();
            PlayerController playerController = await SetupPlayer(weaponFactory, playerFactory, laserProjectilePool, playerData, gameFieldBounds, inputService, audioManager);
            disposes.Add(playerController);
            
            //CreateEnemies
            ExplosionController explosionController = await SetupExplosions(assetProvider, audioManager);
            disposes.Add(explosionController);
            IEnemyFactory enemyFactory = gameInstaller.Resolve<IEnemyFactory>();
            EnemyData enemyData = await assetProvider.LoadAddressable<EnemyData>("EnemyData");
            enemyFactory.EnemyData = enemyData;
            await enemyFactory.WarmUpIfNeeded();
            EnemiesController enemiesController = SetupEnemies(environmentView, enemyFactory, laserProjectilePool, explosionController, audioManager, assetProvider);
            updater.AddUpdatable(enemiesController);
            disposes.Add(enemiesController);

            //CreateGameUI
            IGameUIFactory gameUIFactory = gameInstaller.Resolve<IGameUIFactory>();
            Canvas windowsCanvas = await gameUIFactory.CreateWindowsCanvas();
            
            HUDController hudController = await SetupHUD(gameUIFactory, enemiesController);
            hudController.Init();
            disposes.Add(hudController);
            
            PauseWindowController pauseWindowController = await SetupPauseWindow(gameUIFactory, windowsCanvas, timeController, inputService);
            disposes.Add(pauseWindowController);
            
            WinWindowController winWindowController = await SetupWinWindow(gameUIFactory, windowsCanvas);
            disposes.Add(winWindowController);
            
            LoseWindowController loseWindowController = await SetupLoseWindow(gameUIFactory, windowsCanvas);
            disposes.Add(loseWindowController);
            
            //Create GameController
            
            GameController gameController = new GameController(gameStateMachine, playerController, city, enemiesController, winWindowController, loseWindowController, hudController, timeController);
            gameController.Init(disposes);
            gameInstaller.BindAsSingleFromInstance<IGameController, GameController>(gameController);
            
            gameStateMachine.Enter<StartState, int>(gameStateMachine.CurrentLevelIndex);
        }

        private static async Task<Updater> CreateUpdater(IAssetProvider assetProvider)
        {
            GameObject updaterObj = await assetProvider.InstantiateAddressable("Updater");
            Updater updater = updaterObj.GetComponent<Updater>();
            return updater;
        }

        private async Task<LoseWindowController> SetupLoseWindow(IGameUIFactory gameUIFactory, Canvas windowsCanvas)
        {
            LoseWindowView loseWindowView = await gameUIFactory.CreateLoseWindow(windowsCanvas);
            LoseWindowController loseWindowController = new LoseWindowController(gameStateMachine, loseWindowView);
            loseWindowController.Init();
            return loseWindowController;
        }

        private async Task<WinWindowController> SetupWinWindow(IGameUIFactory gameUIFactory, Canvas windowsCanvas)
        {
            WinWindowView winWindowView = await gameUIFactory.CreateWinWindow(windowsCanvas);
            WinWindowController winWindowController = new WinWindowController(gameStateMachine, winWindowView);
            winWindowController.Init();
            return winWindowController;
        }

        private async Task<PauseWindowController> SetupPauseWindow(IGameUIFactory gameUIFactory, Canvas windowsCanvas, ITimeController timeController, IInputService inputService)
        {
            PauseWindowView pauseWindowView = await gameUIFactory.CreatePauseWindow(windowsCanvas);
            PauseWindowController pauseWindowController = new PauseWindowController(gameStateMachine, timeController, inputService, pauseWindowView);
            pauseWindowController.Init();
            return pauseWindowController;
        }

        private static async Task<HUDController> SetupHUD(IGameUIFactory gameUIFactory, EnemiesController enemiesController)
        {
            HUDView hudView = await gameUIFactory.CreateHUD();
            HUDController hudController = new HUDController(hudView, enemiesController);
            return hudController;
        }

        private static async Task<PlayerController> SetupPlayer(IWeaponFactory weaponFactory, IPlayerFactory playerFactory, LaserProjectilePool laserProjectilePool, PlayerData playerData, Bounds gameFieldBounds, IInputService inputService, IAudioManager audioManager)
        {
            PlayerView playerView = await playerFactory.CreatePlayer(playerData);
            LaserWeaponView laserWeaponView = weaponFactory.CreatePlayerLaserWeapon(playerView.transform);
            
            LaserWeaponController laserWeaponController = new LaserWeaponController(laserWeaponView, laserProjectilePool, audioManager,true);
            
            PlayerController playerController = new PlayerController(playerView, gameFieldBounds, laserWeaponController, inputService);
            playerController.Init();
            return playerController;
        }

        private static EnemiesController SetupEnemies(EnvironmentView environmentView, IEnemyFactory enemyFactory, LaserProjectilePool laserProjectilePool, ExplosionController explosionController, IAudioManager audioManager, IAssetProvider assetProvider)
        {
            EnemySpawnArea enemySpawnArea = new EnemySpawnArea(environmentView.GetGameFieldBounds());
            EnemyPool enemyPool = new EnemyPool(enemyFactory as EnemyFactory, 10);
            EnemiesController enemiesController = new EnemiesController(enemyPool, enemySpawnArea, laserProjectilePool, explosionController, audioManager, assetProvider);
            return enemiesController;
        }

        private static EnvironmentView SetupEnvironment(ILevelFactory levelFactory)
        {
            EnvironmentView environmentView = levelFactory.CreateEnvironment();
            environmentView.SetUp();
            return environmentView;
        }

        private static CityView SetupCity(ILevelFactory levelFactory)
        {
            CityView city = levelFactory.CreateCity();
            city.Init();
            return city;
        }

        private async UniTask<ExplosionController> SetupExplosions(IAssetProvider assetProvider, IAudioManager audioManager)
        {
            ExplosionFactory explosionFactory = new ExplosionFactory(assetProvider);
            await explosionFactory.WarmUpIfNeeded();
            ExplosionPool explosionPool = new ExplosionPool(explosionFactory, 10);
            ExplosionController explosionController = new ExplosionController(explosionPool, audioManager);
            return explosionController;
        }

        public void Exit()
        {
            
        }
    }
}