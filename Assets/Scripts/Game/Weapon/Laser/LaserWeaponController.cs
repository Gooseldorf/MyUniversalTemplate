using Infrastructure.AssetManagement;
using Infrastructure.Services.Input;
using Interfaces;
using UniRx;
using UnityEngine;

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
            await factory.Init();
            pool = new LaserProjectilePool(factory);
            pool.Init();
        }

        public void Dispose()
        {
            
        }

        public void Shoot()
        {
            Transform[] shotPoints = view.GetNextShootPoints(1);//TODO: LaserWeaponData

            foreach (var shotPoint in shotPoints)
            {
                LaserProjectileView projectile = pool.Pool.Get();
                var projectileTransform = projectile.transform;
                projectileTransform.position = shotPoint.position;
                projectileTransform.rotation = shotPoint.rotation;
                projectile.Fire(view.transform.up);
                projectile.OnLifetimeEnd += OnProjectileLideTimeEnd;
            }
        }

        private void OnProjectileLideTimeEnd(LaserProjectileView projectile)
        {
            projectile.OnLifetimeEnd -= OnProjectileLideTimeEnd;
            pool.Pool.Release(projectile);

        }
    }
}