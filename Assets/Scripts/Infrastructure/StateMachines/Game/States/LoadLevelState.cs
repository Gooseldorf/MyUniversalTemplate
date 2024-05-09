using Controllers;
using Cysharp.Threading.Tasks;
using Game.Player;
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
            
            //TODO: LoadFromLevelData
            await CreateEnvironment(gameInstaller.Resolve<ILevelFactory>());

            IInputService inputService = gameInstaller.Resolve<IInputService>();
            await CreatePlayer(gameInstaller.Resolve<IPlayerFactory>(), inputService);

            IGameUIFactory gameUIFactory = gameInstaller.Resolve<IGameUIFactory>();
            await CreateGameUI(gameUIFactory, timeController, inputService);
            
            gameStateMachine.Enter<StartState>();
        }

        private async UniTask CreatePlayer(IPlayerFactory playerFactory, IInputService inputService)
        {
            PlayerView playerView = await playerFactory.CreatePlayer();
            playerView.transform.position = GameObject.Find("InitialPoint").transform.position;
            PlayerController playerController = new PlayerController(inputService);
            playerView.Init(inputService);
        }

        private async UniTask CreateEnvironment(ILevelFactory levelFactory)
        {
            GameObject environment = await levelFactory.CreateEnvironment();
        }

        private async UniTask CreateGameUI(IGameUIFactory gameUIFactory, TimeController timeController, IInputService inputService)
        {
            Canvas mainCanvas = await gameUIFactory.CreateMainCanvas();
            
            HUDView hudView = await gameUIFactory.CreateHUD(mainCanvas);
            hudView.transform.SetParent(mainCanvas.transform, false);
            HUDController hudController = new HUDController(hudView);
            
            PauseWindowView pauseWindowView = await gameUIFactory.CreatePauseWindow(mainCanvas);
            PauseWindowController pauseWindowController = new PauseWindowController(gameStateMachine, timeController, inputService, pauseWindowView);
            pauseWindowController.Init();
            
            WinWindowView winWindowView = await gameUIFactory.CreateWinWindow(mainCanvas);
            WinWindowController winWindowController = new WinWindowController(gameStateMachine, winWindowView);
            winWindowController.Init();
            
            LoseWindowView loseWindowView = await gameUIFactory.CreateLoseWindow(mainCanvas);
            LoseWindowController loseWindowController = new LoseWindowController(gameStateMachine, loseWindowView);
            loseWindowController.Init();
        }
        
        public void Exit()
        {
            
        }
    }
}