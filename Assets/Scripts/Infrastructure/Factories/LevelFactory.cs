using Cysharp.Threading.Tasks;
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
        
        public async UniTask<GameObject> CreateEnvironment()
        {
            GameObject environment = await assetProvider.InstantiateAddressable("Environment");
            return environment;
        }
    }

    public interface ILevelFactory
    {
        UniTask<GameObject> CreateEnvironment();
    }
}