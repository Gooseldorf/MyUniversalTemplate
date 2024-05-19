using System;
using System.Collections.Generic;
using Game.Player;
using Game.Projectiles;
using Infrastructure;
using Infrastructure.Pools;
using UniRx;
using UnityEngine;

namespace Game.Weapon.Laser
{
    public class LaserProjectilePool : ComponentPoolBase<LaserProjectileView>
    {
        private readonly ProjectileReleaser releaser;
        private List<LaserProjectileView> spawnedProjectiles = new();
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
            projectile.transform.SetParent(Container.transform);
            projectile.Init();
            projectile.CollisionStream.Subscribe(OnCollision).AddTo(disposes);
            projectile.gameObject.SetActive(false);
            return projectile;
        }

        protected override void Get(LaserProjectileView obj)
        {
            base.Get(obj);
            if(!spawnedProjectiles.Contains(obj))
                spawnedProjectiles.Add(obj);
        }

        protected override void Release(LaserProjectileView obj)
        {
            /*if(spawnedProjectiles.Contains(obj))
                spawnedProjectiles.Remove(obj);*/
            base.Release(obj);
        }

        private void OnCollision((Collider collider, LaserProjectileView view) data)
        {
            if (data.collider.transform.parent != null)
            {
                if((data.view.IsPlayerProjectile && data.collider.transform.parent.TryGetComponent(out PlayerView player)) || 
                   !data.view.IsPlayerProjectile && data.collider.transform.parent.TryGetComponent(out EnemyView enemy))
                    return;
            }

            Release(data.view);
        }

        public void ReleaseEveryone()
        {
            foreach (var enemy in spawnedProjectiles)
            {
                try
                {
                    Pool.Release(enemy);
                }
                catch (InvalidOperationException exception)
                {
                    Debug.Log( $"CATCHED : {exception.Message}");
                }
            }
            spawnedProjectiles.Clear();
        }
    }
}