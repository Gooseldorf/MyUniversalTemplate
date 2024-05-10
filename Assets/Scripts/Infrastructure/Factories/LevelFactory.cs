using Cysharp.Threading.Tasks;
using Data;
using Infrastructure.AssetManagement;
using UnityEngine;

namespace Infrastructure.Factories
{
    public class LevelFactory : ILevelFactory
    {
        private readonly IAssetProvider assetProvider;

        public LevelFactory(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }
        
        public async UniTask<GameObject> CreateEnvironment(LevelData levelData)
        {
            GameObject environment = await assetProvider.InstantiateAddressable(levelData.EnvironmentAddress);
            return environment;
        }
    }

    public interface ILevelFactory
    {
        UniTask<GameObject> CreateEnvironment(LevelData levelData);
    }
}