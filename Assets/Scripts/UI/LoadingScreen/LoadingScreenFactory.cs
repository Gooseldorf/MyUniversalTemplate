using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagement;
using UI;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface ILoadingScreenFactory
    {
        UniTask WarmUpIfNeeded();
        LoadingScreenController CreateLoadingScreen();
        void Clear();
    }
    
    public class LoadingScreenFactory : GameObjectFactoryBase, ILoadingScreenFactory
    {
        private GameObject loadingScreenViewPrefab;
        
        public LoadingScreenFactory(IAssetProvider assetProvider) : base(assetProvider)
        { }

        public override async UniTask WarmUpIfNeeded()
        {
            loadingScreenViewPrefab = await CachePrefab("LoadingScreen");
        }

        public LoadingScreenController CreateLoadingScreen()
        {
            GameObject loadingScreenObject = CreateGameObject(loadingScreenViewPrefab);
            loadingScreenObject.TryGetComponent(out LoadingScreenView loadingScreenView);
            LoadingScreenController loadingScreenController = new(loadingScreenView);
            return loadingScreenController;
        }

        public override void Clear() => loadingScreenViewPrefab = null;
    }
}