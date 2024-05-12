﻿using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagement;
using Infrastructure.Factories;
using UnityEngine;

namespace Game.VFX.Explosion
{
    public class ExplosionFactory : FactoryBase
    {
        private GameObject explosionPrefab;
        
        public ExplosionFactory(IAssetProvider assetProvider) : base(assetProvider)
        {
        }

        public override async UniTask WarmUpIfNeeded()
        {
            if(explosionPrefab == null)
                explosionPrefab = await CachePrefab("Explosion");
        }
        
        public ExplosionView CreateExplosion()
        {
            GameObject explosion = CreateGameObject(explosionPrefab);
            explosion.TryGetComponent(out ExplosionView explosionView);
            return explosionView;
        }
    }
}