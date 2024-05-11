using UnityEngine;

namespace Game.VFX.Explosion
{
    public class ExplosionController
    {
        private readonly ExplosionPool explosionPool;

        public ExplosionController(ExplosionPool explosionPool)
        {
            this.explosionPool = explosionPool;
        }

        public void Explode(Vector3 position)
        {
            ExplosionView explosion = explosionPool.Pool.Get();
            explosion.transform.position = position + new Vector3(0,0,-3);
        }
    }
}