﻿using Infrastructure.Factories;
using Infrastructure.Pools;
using Interfaces;
using UniRx;

namespace Game.VFX.Explosion
{
    public class ExplosionPool : ComponentPoolBase<ExplosionView>, IDispose
    {
        private CompositeDisposable disposes = new CompositeDisposable();

        public ExplosionPool(CachedGameObjectFactoryBase factory, int poolSize) : base(factory, poolSize)
        { }
        
        public void Dispose()
        {
            disposes.Dispose();
        }

        protected override void Get(ExplosionView obj)
        {
            base.Get(obj);
            obj.Play();
        }

        protected override void Release(ExplosionView obj)
        {
            obj.ResetParticles();
            base.Release(obj);
        }

        protected override ExplosionView Create()
        {
            ExplosionView explosion = ((ExplosionFactory)Factory).CreateExplosion();
            explosion.Init();
            explosion.CompleteStream.Subscribe(OnComplete).AddTo(disposes);
            explosion.gameObject.SetActive(false);
            return explosion;
        }

        private void OnComplete(ExplosionView explosionView) => Pool.Release(explosionView);
    }
}