using Infrastructure.AssetManagement;
using UnityEngine;

namespace Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider assets;

        public GameFactory(IAssetProvider assets)
        {
            this.assets = assets;
        }

        public GameObject CreatePlayer(GameObject at) => assets.Instantiate(AssetPaths.PlayerPath, at.transform.position);
        public void InstantiateHUD() => assets.Instantiate(AssetPaths.HUDPath);
    }
}