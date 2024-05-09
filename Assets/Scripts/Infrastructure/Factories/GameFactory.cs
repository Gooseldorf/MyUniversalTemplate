using Cysharp.Threading.Tasks;
using Game.Player;
using Infrastructure.AssetManagement;
using UI.Game;
using UI.Game.HUD;
using UI.Game.LoseWindow;
using UI.Game.PauseWindow;
using UI.Game.WinWindow;
using UnityEngine;

namespace Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider assetProvider;
        
        public GameFactory(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }

        public async UniTask<PlayerView> CreatePlayer()
        {
            GameObject player =  await assetProvider.InstantiateAddressable("Player");
            player.TryGetComponent(out PlayerView playerView);
            return playerView;
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