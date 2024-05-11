using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagement;
using Infrastructure.Factories;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyFactory : FactoryBase, IEnemyFactory
    {
        private GameObject enemyPrefab;
        
        public EnemyFactory(IAssetProvider assetProvider) : base(assetProvider)
        {
            this.assetProvider = assetProvider;
        }

        public override async UniTask WarmUp()
        {
            enemyPrefab =  await CachePrefab("Enemy");
        }

        public EnemyView CreateEnemy()
        {
            GameObject enemyObject = CreateGameObject(enemyPrefab);
            enemyObject.TryGetComponent(out EnemyView enemyView);
            //TODO: SetupEnemy
            enemyObject.transform.position = new Vector3(0, 80, 0);
            return enemyView;
        }
    }
}