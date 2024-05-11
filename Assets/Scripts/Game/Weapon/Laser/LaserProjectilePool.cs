using Game.Player;
using Game.Projectiles;
using Infrastructure;
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
        }
        
        public void Dispose()
        {
            disposes.Dispose();
        }

        protected override LaserProjectileView Create()
        {
            LaserProjectileView projectile = ((LaserProjectileFactory)Factory).CreateLaserProjectile();
            projectile.Init();
            projectile.CollisionStream.Subscribe(OnCollision).AddTo(disposes);
            projectile.gameObject.SetActive(false);
            return projectile;
        }

        private void OnCollision((Collision collision, LaserProjectileView view) data)
        {
            if (data.collision.transform.parent != null)
            {
                if((data.view.IsPlayerProjectile && data.collision.transform.parent.TryGetComponent(out PlayerView player)) || 
                   !data.view.IsPlayerProjectile && data.collision.transform.parent.TryGetComponent(out EnemyView enemy))
                    return;
            }

            Release(data.view);
        }
    }
}