using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagement;
using UI.Game;
using UI.Menu;
using UnityEngine;
using Zenject;

namespace Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider assetProvider;

        [Inject]
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

        public async UniTask<HUDView> CreateHUD()
        {
            GameObject hud = await assetProvider.InstantiateAddressable("HUD");
            hud.TryGetComponent(out HUDView hudView);
            return hudView;
        }
        
        public async UniTask<PauseWindowView> CreatePauseWindow()
        {
            GameObject pauseWindow = await assetProvider.InstantiateAddressable("PauseWindow");
            pauseWindow.TryGetComponent(out PauseWindowView pauseWindowView);
            return pauseWindowView;
        }
        
        public async UniTask<GameObject> CreateEnvironment()
        {
            GameObject environment = await assetProvider.InstantiateAddressable("Environment");
            return environment;
        }
    }
}