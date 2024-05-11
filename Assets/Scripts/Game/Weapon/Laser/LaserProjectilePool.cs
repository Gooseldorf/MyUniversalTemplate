using Game.Projectiles;
using Infrastructure;
using Interfaces;
using UniRx;
using UnityEngine;

namespace Game.Weapon.Laser
{
    public class LaserProjectilePool : PoolBase<LaserProjectileView>
    {
        private readonly ProjectileReleaser releaser;
        private CompositeDisposable disposes = new CompositeDisposable();

        public LaserProjectilePool(LaserProjectileFactory factory, ProjectileReleaser releaser, int poolSize) : base(factory, poolSize)
        {
            this.releaser = releaser;
            releaser.ProjectileStream.Subscribe(OnReleaserTriggered).AddTo(disposes);
        }

        public void OnReleaserTriggered(ProjectileViewBase projectile)
        {
            if(projectile is LaserProjectileView laserProjectile)
                Release(laserProjectile);
            Debug.Log("ProjectileReleased");
        }

        protected override LaserProjectileView Create()
        {
            LaserProjectileView obj = ((LaserProjectileFactory)Factory).CreateLaserProjectile();
            obj.gameObject.SetActive(false);
            return obj;
        }
    }
}