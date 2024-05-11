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
        
        private const float speed = 80;

        private bool shouldFly = false;
        private Vector3 direction = Vector3.zero;

        public bool IsPlayerProjectile;
        public IObservable<(Collision collision, LaserProjectileView view)> CollisionStream;

        public void Init()
        {
            CollisionStream = projectileCollider.OnCollisionEnterAsObservable().Select(collision =>(collision, this));
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

        private void Fly()
        {
            transform.position += direction.normalized * speed * Time.deltaTime;
        }
    }
}