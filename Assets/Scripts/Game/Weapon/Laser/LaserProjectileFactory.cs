using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagement;
using UnityEngine;

namespace Game.Weapon.Laser
{
    public class LaserProjectileFactory 
    {
        private readonly IAssetProvider assetProvider;
        private LaserProjectileView projectilePrefab;
        private GameObject projectilesContainer;

        public LaserProjectileFactory(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }

        public async UniTask Init()
        {
            projectilesContainer = new GameObject("LaserProjectiles");
            projectilePrefab = await CacheLaserProjectilePrefab();
        }

        private async UniTask<LaserProjectileView> CacheLaserProjectilePrefab()
        {
            GameObject projectile =  await assetProvider.LoadAddressable<GameObject>("LaserProjectile");
            projectile.TryGetComponent(out LaserProjectileView projectileView);
            return projectileView;
        }

        public LaserProjectileView CreateLaserProjectile()
        {
            return Object.Instantiate(projectilePrefab, projectilesContainer.transform);
        }
    }
}