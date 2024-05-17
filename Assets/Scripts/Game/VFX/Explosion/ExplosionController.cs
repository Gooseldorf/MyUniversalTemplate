using Audio;
using Enums;
using Interfaces;
using UnityEngine;

namespace Game.VFX.Explosion
{
    public class ExplosionController : IDispose
    {
        private readonly ExplosionPool explosionPool;
        private readonly IAudioManager audioManager;

        public ExplosionController(ExplosionPool explosionPool, IAudioManager audioManager)
        {
            this.explosionPool = explosionPool;
            this.audioManager = audioManager;
        }

        public void Explode(Vector3 position)
        {
            ExplosionView explosion = explosionPool.Pool.Get();
            explosion.transform.position = position + new Vector3(0,0,-10);
            audioManager.PlayGame2DSound(SoundKeys.Explosion);
        }

        public void Dispose()
        {
            explosionPool.Dispose();
        }
    }
}