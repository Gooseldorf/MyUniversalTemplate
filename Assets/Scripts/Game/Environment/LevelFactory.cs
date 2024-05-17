using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagement;
using Infrastructure.Factories;
using UnityEngine;

namespace Game.Environment
{
    public class LevelFactory : CachedGameObjectFactoryBase, ILevelFactory
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

        public override void Clear()
        {
            environmentPrefab = null;
            cityPrefab = null;
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