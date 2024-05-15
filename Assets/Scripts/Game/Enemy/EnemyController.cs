using System;
using Data;
using Game.Weapon.Laser;
using Interfaces;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyController : IUpdate, IDispose
    {
        private readonly EnemyView enemyView;
        private readonly EnemyData enemyData;
        private readonly LaserWeaponController laserWeaponController;
        
        private int health = 3;
        private float shootDelay = 0;
        private float shootTimer = 0;
        private bool isCanShoot;

        public CompositeDisposable disposes = new CompositeDisposable();
        public event Action<EnemyController, EnemyView> Dead;
        
        public EnemyController(EnemyView enemyView, EnemyData enemyData, LaserWeaponController laserWeaponController)
        {
            this.enemyView = enemyView;
            this.enemyData = enemyData;
            this.laserWeaponController = laserWeaponController;
            SetUpEnemy();
        }

        private void SetUpEnemy()
        {
            health = enemyData.Health;
            shootDelay = enemyData.ShootDelay;
        }

        public void Init()
        {
            SubscribeToViewCollisionStream();
            enemyView.HitComponent.OnHit += OnHit;
            isCanShoot = true;
        }

        public void Update()
        {
            if (isCanShoot)
            {
                shootTimer += Time.deltaTime;
                if (shootTimer >= shootDelay)
                {
                    enemyView.EnemyShoot.Shoot(laserWeaponController);
                    shootTimer = 0;
                }
            }
        }

        public void Dispose()
        {
            disposes.Dispose();
            isCanShoot = false;
            enemyView.HitComponent.OnHit -= OnHit;
        }

        private void SubscribeToViewCollisionStream()
        {
            enemyView.EnemyCollider.OnCollisionEnterAsObservable()
                .Where(collision =>
                {
                    Transform parent;
                    return (parent = collision.transform.parent) != null &&
                           parent.TryGetComponent(out LaserProjectileView projectile) &&
                           projectile.IsPlayerProjectile;
                })
                .Subscribe(_ => OnHit(true))
                .AddTo(disposes);
        }

        private void OnHit(bool isPlayerProjectile)
        {
            if(!isPlayerProjectile) return;
            health--;
            if(health <= 0)
                Die();
        }

        private void Die()
        {
            isCanShoot = false;
            Dead?.Invoke(this, enemyView);
            Dispose();
        }
    }
}