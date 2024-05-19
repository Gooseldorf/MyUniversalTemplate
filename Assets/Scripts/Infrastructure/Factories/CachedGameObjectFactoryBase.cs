using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagement;
using UnityEngine;

namespace Infrastructure.Factories
{
    public abstract class CachedGameObjectFactoryBase
    {
        protected IAssetProvider assetProvider;
        
        protected CachedGameObjectFactoryBase(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }

        public abstract UniTask WarmUpIfNeeded();

        public abstract void Clear();

        protected async UniTask<GameObject> CachePrefab(string address) => await assetProvider.LoadAddressable<GameObject>(address);

        public GameObject CreateGameObject(GameObject prefab) => Object.Instantiate(prefab);
    }
}