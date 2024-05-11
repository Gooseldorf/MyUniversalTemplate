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
using Infrastructure.StateMachines.Main;
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

        public LoadLevelState(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        public async void Enter(int levelIndex)
        {
            GameInstaller gameInstaller = Object.FindObjectOfType<GameInstaller>();
            TimeController timeController = new TimeController();

            //Load LevelData by index
            IAssetProvider assetProvider = gameInstaller.Resolve<IAssetProvider>();
            LevelData levelData = await assetProvider.LoadAddressable<LevelData>($"Level_{levelIndex}");
            
            //CreateEnvironment
            ILevelFactory levelFactory = gameInstaller.Resolve<ILevelFactory>();
            await levelFactory.WarmUp();
            EnvironmentView environmentView = levelFactory.CreateEnvironment();
            environmentView.SetUp();
            Bounds gameFieldBounds = environmentView.GetGameFieldBounds();
            CityView city = levelFactory.CreateCity();
            city.Init();

            ExplosionFactory explosionFactory = new ExplosionFactory(assetProvider);
            await explosionFactory.WarmUp();
            ExplosionPool explosionPool = new ExplosionPool(explosionFactory, 10);
            ExplosionController explosionController = new ExplosionController(explosionPool);
            
            EnemySpawner enemySpawner = new EnemySpawner(environmentView.GetGameFieldBounds());
            IEnemyFactory enemyFactory = gameInstaller.Resolve<IEnemyFactory>();
            await enemyFactory.WarmUp();
            EnemyPool enemyPool = new EnemyPool(enemyFactory as EnemyFactory, 10);
            EnemiesController enemiesController = new EnemiesController(enemyPool, enemySpawner, explosionController);
            
            //TODO: Create GameController(enemyPool)
            
            //CreatePlayer
            IInputService inputService = gameInstaller.Resolve<IInputService>();
            IPlayerFactory playerFactory = gameInstaller.Resolve<IPlayerFactory>();
            PlayerView playerView = await playerFactory.CreatePlayer(levelData);

            IWeaponFactory weaponFactory = gameInstaller.Resolve<IWeaponFactory>();
            LaserWeaponView laserWeaponView = await weaponFactory.CreateLaserWeapon();
            laserWeaponView.transform.SetParent(playerView.transform);
            laserWeaponView.transform.localPosition = Vector3.zero;
            
            LaserProjectileFactory laserProjectileFactory = new LaserProjectileFactory(assetProvider);
            await laserProjectileFactory.WarmUp();
            GameObject projectileReleaserObj = await assetProvider.InstantiateAddressable("ProjectileReleaser");
            ProjectileReleaser projectileReleaser = projectileReleaserObj.GetComponent<ProjectileReleaser>();
            LaserProjectilePool laserProjectilePool = new LaserProjectilePool(laserProjectileFactory, projectileReleaser, 20);
            
            LaserWeaponController laserWeaponController = new LaserWeaponController(laserWeaponView, laserProjectilePool, true);
            
            PlayerController playerController = new PlayerController(playerView, gameFieldBounds, laserWeaponController, inputService);
            playerController.Init();
            
            //CreateGameUI
            IGameUIFactory gameUIFactory = gameInstaller.Resolve<IGameUIFactory>();
            Canvas windowsCanvas = await gameUIFactory.CreateWindowsCanvas();
            
            HUDView hudView = await gameUIFactory.CreateHUD();
            HUDController hudController = new HUDController(hudView);
            
            PauseWindowView pauseWindowView = await gameUIFactory.CreatePauseWindow(windowsCanvas);
            PauseWindowController pauseWindowController = new PauseWindowController(gameStateMachine, timeController, inputService, pauseWindowView);
            pauseWindowController.Init();
            
            WinWindowView winWindowView = await gameUIFactory.CreateWinWindow(windowsCanvas);
            WinWindowController winWindowController = new WinWindowController(gameStateMachine, winWindowView);
            winWindowController.Init();
            
            LoseWindowView loseWindowView = await gameUIFactory.CreateLoseWindow(windowsCanvas);
            LoseWindowController loseWindowController = new LoseWindowController(gameStateMachine, loseWindowView);
            loseWindowController.Init();            
            
            GameController gameController = new GameController(gameStateMachine, playerController, city, enemiesController, winWindowController, loseWindowController, timeController);
            gameController.Init();
            
            gameStateMachine.Enter<StartState, GameController>(gameController);
        }

        private async UniTask CreateGameUI(IGameUIFactory gameUIFactory, TimeController timeController, IInputService inputService)
        {
            
        }
        
        public void Exit()
        {
            
        }
    }
}