using Infrastructure.AssetManagement;
using UnityEngine;
using Zenject;

namespace Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider assets;

        [Inject]
        public GameFactory(IAssetProvider assetProvider)
        {
            assets = assetProvider;
        }

        public GameObject CreatePlayer(GameObject at) => assets.Instantiate(AssetPaths.PlayerPath, at.transform.position);
        public void InstantiateHUD() => assets.Instantiate(AssetPaths.HUDPath);
    }
}