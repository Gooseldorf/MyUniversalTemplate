using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagement;
using Infrastructure.Factories;
using UnityEngine;

namespace UI.LoadingScreen
{
    public interface ILoadingScreenFactory
    {
        UniTask<LoadingScreenController> CreateLoadingScreenAsync();
    }
    
    public class LoadingScreenFactory : GameObjectFactoryBase, ILoadingScreenFactory
    {
        public LoadingScreenFactory(IAssetProvider assetProvider) : base(assetProvider) { }

        public async UniTask<LoadingScreenController> CreateLoadingScreenAsync()
        {
            GameObject loadingScreenObject = await InstantiateAddressableAsync("LoadingScreen");
            loadingScreenObject.TryGetComponent(out LoadingScreenView loadingScreenView);
            LoadingScreenController loadingScreenController = new(loadingScreenView);
            return loadingScreenController;
        }
    }
}