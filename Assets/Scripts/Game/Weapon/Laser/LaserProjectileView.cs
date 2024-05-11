using System;
using UnityEngine;

namespace Game.Weapon.Laser
{
    public class LaserProjectileView : MonoBehaviour
    {
        private const float lifeTime = 2;
        private float lifeTimer = 0;
        private const float speed = 80;

        private bool shouldFly = false;
        private Vector3 direction = Vector3.zero;

        public bool IsPlayerProjectile;

        public event Action<LaserProjectileView> OnLifetimeEnd;
        
        public void Fire(Vector3 direction, bool isPlayerProjectile)
        {
            lifeTimer = 0;
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
            lifeTimer += Time.deltaTime;
            if (lifeTimer >= lifeTime)
            {
                OnLifetimeEnd?.Invoke(this);
                shouldFly = false;
            }
        }
    }
}