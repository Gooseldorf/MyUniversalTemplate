using System;
using Game.Projectiles;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Game.Weapon.Laser
{
    public class LaserProjectileView : ProjectileViewBase
    {
        [SerializeField] private Collider projectileCollider;
        
        private const float speed = 70;

        private bool shouldFly = false;
        private Vector3 direction = Vector3.zero;

        public bool IsPlayerProjectile;
        public IObservable<(Collider collider, LaserProjectileView view)> CollisionStream;
        private IObservable<Collider> collisionStreamInternal;

        private CompositeDisposable disposes = new CompositeDisposable();
        public void Init()
        {
            CollisionStream = projectileCollider.OnTriggerEnterAsObservable().Select(collider =>(collision: collider, this));
            collisionStreamInternal = projectileCollider.OnTriggerEnterAsObservable().Select(_ => _);
            collisionStreamInternal.Subscribe(OnCollision).AddTo(disposes);
        }
        
        public void Fire(Vector3 direction, bool isPlayerProjectile)
        {
            this.direction = direction;
            shouldFly = true;
            IsPlayerProjectile = isPlayerProjectile;
        }

        private void Update()
        {
            if (shouldFly)
                Fly();
        }

        private void OnCollision(Collider collider)
        {
            if (collider.transform.TryGetComponent(out HitComponent hitComponent))
            {
                hitComponent.Hit(IsPlayerProjectile);
                //disposes.Dispose();
            }
        }

        private void Fly()
        {
            transform.position += direction.normalized * speed * Time.deltaTime;
        }
    }
}