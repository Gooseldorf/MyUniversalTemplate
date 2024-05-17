using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagement;
using Infrastructure.Factories;
using UnityEngine;

namespace Game.Weapon.Laser
{
    public class LaserProjectileFactory : CachedGameObjectFactoryBase
    {
        private GameObject projectilePrefab;

        public LaserProjectileFactory(IAssetProvider assetProvider) : base(assetProvider)
        { }
        
        public override async UniTask WarmUpIfNeeded()
        {
            if(projectilePrefab == null)
                projectilePrefab = await CachePrefab("LaserProjectile");
        }

        public override void Clear()
        {
            projectilePrefab = null;
        }

        public LaserProjectileView CreateLaserProjectile()
        {
            GameObject projectile = CreateGameObject(projectilePrefab);
            projectile.TryGetComponent(out LaserProjectileView projectileView);
            return projectileView;
        }
    }
}