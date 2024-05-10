using Interfaces;
using UnityEngine;
using UnityEngine.Pool;

namespace Game.Weapon.Laser
{
    public class LaserProjectilePool : IInit
    {
        private readonly LaserProjectileFactory factory;
        private int poolSize;
        
        public ObjectPool<LaserProjectileView> Pool { get; private set; }

        public LaserProjectilePool(LaserProjectileFactory factory)
        {
            this.factory = factory;
        }
        public async void Init()
        {
            await factory.Init();
            Pool = new ObjectPool<LaserProjectileView>(Create, Get, Release, Destroy, true, poolSize);
        }

        private LaserProjectileView Create()
        {
            LaserProjectileView obj = factory.CreateLaserProjectile();
            obj.gameObject.SetActive(false);
            return obj;
        }

        private void Get(LaserProjectileView obj) => obj.gameObject.SetActive(true);

        private void Release(LaserProjectileView obj) => obj.gameObject.SetActive(false);

        private void Destroy(LaserProjectileView obj) => Object.Destroy(obj);
    }
}