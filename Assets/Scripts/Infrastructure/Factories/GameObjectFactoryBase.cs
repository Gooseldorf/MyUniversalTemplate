using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagement;
using UnityEngine;

namespace Infrastructure.Factories
{
    public abstract class GameObjectFactoryBase
    {
        protected IAssetProvider assetProvider;
        
        protected GameObjectFactoryBase(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }

        protected UniTask<GameObject> InstantiateAddressableAsync(string address) => assetProvider.InstantiateAddressable(address);
    }
}