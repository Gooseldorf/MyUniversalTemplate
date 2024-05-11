using Controllers;
using Cysharp.Threading.Tasks;
using Data;
using Game.Enemy;
using Game.Environment;
using Game.Player;
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
            EnvironmentView environmentView = levelFactory.CreateEnvironment(levelData);
            
            //CreatePlayer
            IInputService inputService = gameInstaller.Resolve<IInputService>();
            IPlayerFactory playerFactory = gameInstaller.Resolve<IPlayerFactory>();
            PlayerView playerView = await playerFactory.CreatePlayer(levelData);

            IWeaponFactory weaponFactory = gameInstaller.Resolve<IWeaponFactory>();
            LaserWeaponView laserWeaponView = await weaponFactory.CreateLaserWeapon();
            laserWeaponView.transform.SetParent(playerView.transform);
            laserWeaponView.transform.localPosition = Vector3.zero;
            LaserWeaponController laserWeaponController = new LaserWeaponController(laserWeaponView, assetProvider, true);
            laserWeaponController.Init();
            
            PlayerController playerController = new PlayerController(playerView, laserWeaponController, inputService);
            playerController.Init();

            IEnemyFactory enemyFactory = gameInstaller.Resolve<IEnemyFactory>();
            //TODO: Create enemy pool
            //TODO: Create GameController(enemyPool)
            
            //CreateGameUI
            IGameUIFactory gameUIFactory = gameInstaller.Resolve<IGameUIFactory>();
            await CreateGameUI(gameUIFactory, timeController, inputService);
            
            gameStateMachine.Enter<StartState>();
        }

        private async UniTask CreateGameUI(IGameUIFactory gameUIFactory, TimeController timeController, IInputService inputService)
        {
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
        }
        
        public void Exit()
        {
            
        }
    }
}