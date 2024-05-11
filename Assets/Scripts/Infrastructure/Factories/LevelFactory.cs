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
        public LevelFactory(IAssetProvider assetProvider) : base(assetProvider)
        {
            this.assetProvider = assetProvider;
        }

        public override async UniTask WarmUp()
        {
            environmentPrefab = await CachePrefab("Environment");
        }

        public EnvironmentView CreateEnvironment()
        {
            GameObject environmentObject = CreateGameObject(environmentPrefab);
            environmentObject.TryGetComponent(out EnvironmentView environmentView);
            return environmentView;
        }
    } 

    public interface ILevelFactory
    {
        EnvironmentView CreateEnvironment();

        UniTask WarmUp();
    }
}