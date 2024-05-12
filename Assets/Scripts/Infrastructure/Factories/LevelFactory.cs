using Cysharp.Threading.Tasks;
using Data;
using Game.Environment;
using Infrastructure.AssetManagement;
using UnityEngine;

namespace Infrastructure.Factories
{
    public class LevelFactory : FactoryBase, ILevelFactory
    {
        private GameObject environmentPrefab;
        private GameObject cityPrefab;
        public LevelFactory(IAssetProvider assetProvider) : base(assetProvider)
        {
            this.assetProvider = assetProvider;
        }

        public override async UniTask WarmUpIfNeeded()
        {
            if(environmentPrefab == null)
                environmentPrefab = await CachePrefab("Environment");
            if(cityPrefab == null)
                cityPrefab = await CachePrefab("City");
        }

        public EnvironmentView CreateEnvironment()
        {
            GameObject environmentObject = CreateGameObject(environmentPrefab);
            environmentObject.TryGetComponent(out EnvironmentView environmentView);
            return environmentView;
        }

        public CityView CreateCity()
        {
            GameObject cityObject = CreateGameObject(cityPrefab);
            cityObject.TryGetComponent(out CityView cityView);
            return cityView;
        }
    } 

    public interface ILevelFactory
    {
        EnvironmentView CreateEnvironment();
        CityView CreateCity();

        UniTask WarmUpIfNeeded();
    }
}