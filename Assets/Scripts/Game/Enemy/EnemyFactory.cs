using Cysharp.Threading.Tasks;
using Data;
using Game.Weapon;
using Infrastructure.AssetManagement;
using Infrastructure.Factories;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyFactory : GameObjectFactoryBase, IEnemyFactory
    {
        public EnemyData EnemyData { private get; set; }
        private GameObject enemyPrefab;
        
        public EnemyFactory(IAssetProvider assetProvider) : base(assetProvider)
        {
            this.assetProvider = assetProvider;
        }

        public override async UniTask WarmUpIfNeeded()
        {
            if(enemyPrefab == null)
                enemyPrefab =  await CachePrefab(EnemyData.Address);
        }

        public EnemyView CreateEnemy()
        {
            GameObject enemyObject = CreateGameObject(enemyPrefab);
            enemyObject.TryGetComponent(out EnemyView enemyView);
            return enemyView;
        }
    }
}