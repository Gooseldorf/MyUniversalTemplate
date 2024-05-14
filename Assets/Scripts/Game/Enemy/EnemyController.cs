using System;
using Game.Weapon.Laser;
using Interfaces;
using UniRx;
using UniRx.Triggers;

namespace Game.Enemy
{
    public class EnemyController : IDispose
    {
        private readonly EnemyView enemyView;
        private LaserProjectileView cachedProjectile;
        private int health = 3;

        public CompositeDisposable disposes = new CompositeDisposable();
        public event Action<EnemyController, EnemyView> Dead;
        
        public EnemyController(EnemyView enemyView)
        {
            this.enemyView = enemyView;
        }

        public void Init()
        {
            enemyView.EnemyCollider.OnCollisionEnterAsObservable()
                .Where(collision => 
                    collision.transform.parent != null &&
                    collision.transform.parent.TryGetComponent(out cachedProjectile) && 
                    cachedProjectile.IsPlayerProjectile)
                .Subscribe(_ => OnHit())
                .AddTo(disposes);
        }
        

        private void OnHit()
        {
            health--;
            if(health <= 0)
                Dead?.Invoke(this, enemyView);
        }

        public void Dispose()
        {
            disposes.Dispose();
        }
    }
}