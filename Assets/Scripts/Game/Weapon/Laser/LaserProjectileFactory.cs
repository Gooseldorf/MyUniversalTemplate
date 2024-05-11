using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagement;
using Infrastructure.Factories;
using UnityEngine;

namespace Game.Weapon.Laser
{
    public class LaserProjectileFactory : FactoryBase
    {
        private GameObject projectilePrefab;

        public LaserProjectileFactory(IAssetProvider assetProvider) : base(assetProvider)
        { }
        
        public override async UniTask WarmUp()
        {
            projectilePrefab = await CachePrefab("LaserProjectile");
        }

        public LaserProjectileView CreateLaserProjectile()
        {
            GameObject projectile = CreateGameObject(projectilePrefab);
            projectile.TryGetComponent(out LaserProjectileView projectileView);
            return projectileView;
        }
    }
}