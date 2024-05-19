using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagement;
using Infrastructure.Factories;
using UnityEngine;

namespace Game.Environment
{
    public interface ILevelFactory
    {
        UniTask<EnvironmentView> CreateEnvironment();
        UniTask<CityView> CreateCity();
    }
    
    public class LevelFactory : GameObjectFactoryBase, ILevelFactory
    {
        public LevelFactory(IAssetProvider assetProvider) : base(assetProvider)
        {
            this.assetProvider = assetProvider;
        }

        public async UniTask<EnvironmentView> CreateEnvironment()
        {
            GameObject environmentObject = await InstantiateAddressableAsync("Environment");
            environmentObject.TryGetComponent(out EnvironmentView environmentView);
            return environmentView;
        }

        public async UniTask<CityView> CreateCity()
        {
            GameObject cityObject = await InstantiateAddressableAsync("City");
            cityObject.TryGetComponent(out CityView cityView);
            return cityView;
        }
    } 
}