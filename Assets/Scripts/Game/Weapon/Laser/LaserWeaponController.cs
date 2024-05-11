using Infrastructure.AssetManagement;
using Interfaces;
using UniRx;

namespace Game.Weapon.Laser
{
    public class LaserWeaponController : IInit, IDispose
    {
        private readonly LaserWeaponView view;
        private readonly IAssetProvider assetProvider;
        private  LaserProjectilePool pool;
        private  LaserProjectileFactory factory;

        private CompositeDisposable disposes = new CompositeDisposable();

        public LaserWeaponController(LaserWeaponView view, IAssetProvider assetProvider)
        {
            this.view = view;
            this.assetProvider = assetProvider;
        }

        public async void Init()
        {
            factory = new LaserProjectileFactory(assetProvider);
            await factory.WarmUp();
            pool = new LaserProjectilePool(factory);
            pool.Init();
        }

        public void Dispose()
        {
            
        }

        public void Shoot()
        {
            ShotPoint[] shotPoints = view.GetNextShootPoints(1);//TODO: LaserWeaponData

            foreach (var shotPoint in shotPoints)
            {
                LaserProjectileView projectile = pool.Pool.Get();
                var projectileTransform = projectile.transform;
                projectileTransform.position = shotPoint.GetCenterPosition();
                projectileTransform.rotation = shotPoint.GetRotation();
                projectile.Fire(shotPoint.GetDirection());
                projectile.OnLifetimeEnd += OnProjectileLifeTimeEnd;
            }
        }

        private void OnProjectileLifeTimeEnd(LaserProjectileView projectile)
        {
            projectile.OnLifetimeEnd -= OnProjectileLifeTimeEnd;
            pool.Pool.Release(projectile);

        }
    }
}