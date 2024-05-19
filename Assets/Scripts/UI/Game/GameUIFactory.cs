using Controllers;
using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagement;
using Infrastructure.Factories;
using Infrastructure.Services.Input;
using Infrastructure.StateMachines.Game;
using UI.Game.HUD;
using UI.Game.LoseWindow;
using UI.Game.PauseWindow;
using UI.Game.WinWindow;
using UnityEngine;

namespace UI.Game
{
    public class GameUIFactory : GameObjectFactoryBase, IGameUIFactory
    {
        public GameUIFactory(IAssetProvider assetProvider) : base(assetProvider) { }

        public async UniTask<Canvas> CreateWindowsCanvasAsync()
        {
            GameObject canvas =  await assetProvider.InstantiateAddressableAsync("WindowsCanvas");
            canvas.TryGetComponent(out Canvas canvasComponent);
            return canvasComponent;
        }

        public async UniTask<HUDController> CreateHUDAsync(HUDData HUDInitialData)
        {
            GameObject hud = await assetProvider.InstantiateAddressableAsync("HUD");
            hud.TryGetComponent(out HUDView hudView);
            HUDController HUDController = new HUDController(hudView, HUDInitialData);
            return HUDController;
        }
        
        public async UniTask<PauseWindowController> CreatePauseWindowAsync(Canvas windowsCanvas, GameStateMachine gameStateMachine, ITimeController timeController, IInputService inputService)
        {
            GameObject pauseWindow = await assetProvider.InstantiateAddressableAsync("PauseWindow");
            pauseWindow.transform.SetParent(windowsCanvas.transform, false);
            
            pauseWindow.TryGetComponent(out PauseWindowView pauseWindowView);
            pauseWindowView.Hide();

            PauseWindowController controller = new(pauseWindowView, gameStateMachine, timeController, inputService);
            return controller;
        }
        
        public async UniTask<WinWindowController> CreateWinWindowAsync(Canvas windowsCanvas, GameStateMachine gameStateMachine)
        {
            GameObject winWindow = await assetProvider.InstantiateAddressableAsync("WinWindow");
            winWindow.transform.SetParent(windowsCanvas.transform, false);
            
            winWindow.TryGetComponent(out WinWindowView winWindowView);
            winWindowView.Hide();

            WinWindowController controller = new(winWindowView, gameStateMachine);
            return controller;
        }
        
        public async UniTask<LoseWindowController> CreateLoseWindowAsync(Canvas windowsCanvas, GameStateMachine gameStateMachine)
        {
            GameObject loseWindow = await assetProvider.InstantiateAddressableAsync("LoseWindow");
            loseWindow.transform.SetParent(windowsCanvas.transform, false);
            
            loseWindow.TryGetComponent(out LoseWindowView loseWindowView);
            loseWindowView.Hide();
            
            LoseWindowController controller = new(loseWindowView, gameStateMachine);
            return controller;
        }
    }
}