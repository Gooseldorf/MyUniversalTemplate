using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagement;
using UnityEngine;

namespace Infrastructure.Factories
{
    public abstract class FactoryBase
    {
        protected IAssetProvider assetProvider;
        private GameObject prefabObject;
        private GameObject container;
        
        public FactoryBase(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
            container = new GameObject(GetType().Name + "Container");
        }

        public abstract UniTask WarmUpIfNeeded();

        protected async UniTask<GameObject> CachePrefab(string address) => await assetProvider.LoadAddressable<GameObject>(address);

        protected GameObject CreateGameObject(GameObject prefab) => Object.Instantiate(prefab, container.transform);
    }
}