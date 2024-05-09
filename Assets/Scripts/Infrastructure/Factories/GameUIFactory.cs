using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagement;
using UI.Game.HUD;
using UI.Game.LoseWindow;
using UI.Game.PauseWindow;
using UI.Game.WinWindow;
using UnityEngine;

namespace Infrastructure.Factories
{
    public class GameUIFactory : IGameUIFactory
    {
        private readonly IAssetProvider assetProvider;
        
        public GameUIFactory(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }

        public async UniTask<Canvas> CreateMainCanvas()
        {
            GameObject canvas =  await assetProvider.InstantiateAddressable("MainCanvas");
            canvas.TryGetComponent(out Canvas canvasComponent);
            return canvasComponent;
        }

        public async UniTask<HUDView> CreateHUD(Canvas mainCanvas)
        {
            GameObject hud = await assetProvider.InstantiateAddressable("HUD");
            hud.TryGetComponent(out HUDView hudView);
            return hudView;
        }
        
        public async UniTask<PauseWindowView> CreatePauseWindow(Canvas mainCanvas)
        {
            GameObject pauseWindow = await assetProvider.InstantiateAddressable("PauseWindow");
            pauseWindow.transform.SetParent(mainCanvas.transform, false);
            pauseWindow.TryGetComponent(out PauseWindowView pauseWindowView);
            pauseWindowView.Hide();
            return pauseWindowView;
        }
        
        public async UniTask<WinWindowView> CreateWinWindow(Canvas mainCanvas)
        {
            GameObject winWindow = await assetProvider.InstantiateAddressable("WinWindow");
            winWindow.transform.SetParent(mainCanvas.transform, false);
            winWindow.TryGetComponent(out WinWindowView winWindowView);
            winWindowView.Hide();
            return winWindowView;
        }
        
        public async UniTask<LoseWindowView> CreateLoseWindow(Canvas mainCanvas)
        {
            GameObject loseWindow = await assetProvider.InstantiateAddressable("LoseWindow");
            loseWindow.transform.SetParent(mainCanvas.transform, false);
            loseWindow.TryGetComponent(out LoseWindowView loseWindowView);
            loseWindowView.Hide();
            return loseWindowView;
        }
    }
}