using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagement;
using UI.Game.HUD;
using UI.Game.LoseWindow;
using UI.Game.PauseWindow;
using UI.Game.WinWindow;
using UnityEngine;

namespace UI.Game
{
    public class GameUIFactory : IGameUIFactory
    {
        private readonly IAssetProvider assetProvider;
        
        public GameUIFactory(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }

        public async UniTask<Canvas> CreateWindowsCanvas()
        {
            GameObject canvas =  await assetProvider.InstantiateAddressable("WindowsCanvas");
            canvas.TryGetComponent(out Canvas canvasComponent);
            return canvasComponent;
        }

        public async UniTask<HUDView> CreateHUD()
        {
            GameObject hud = await assetProvider.InstantiateAddressable("HUD");
            hud.TryGetComponent(out HUDView hudView);
            return hudView;
        }
        
        public async UniTask<PauseWindowView> CreatePauseWindow(Canvas windowsCanvas)
        {
            GameObject pauseWindow = await assetProvider.InstantiateAddressable("PauseWindow");
            pauseWindow.transform.SetParent(windowsCanvas.transform, false);
            pauseWindow.TryGetComponent(out PauseWindowView pauseWindowView);
            pauseWindowView.Hide();
            return pauseWindowView;
        }
        
        public async UniTask<WinWindowView> CreateWinWindow(Canvas windowsCanvas)
        {
            GameObject winWindow = await assetProvider.InstantiateAddressable("WinWindow");
            winWindow.transform.SetParent(windowsCanvas.transform, false);
            winWindow.TryGetComponent(out WinWindowView winWindowView);
            winWindowView.Hide();
            return winWindowView;
        }
        
        public async UniTask<LoseWindowView> CreateLoseWindow(Canvas windowsCanvas)
        {
            GameObject loseWindow = await assetProvider.InstantiateAddressable("LoseWindow");
            loseWindow.transform.SetParent(windowsCanvas.transform, false);
            loseWindow.TryGetComponent(out LoseWindowView loseWindowView);
            loseWindowView.Hide();
            return loseWindowView;
        }
    }
}