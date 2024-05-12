using System;
using System.Collections.Generic;
using Infrastructure;
using Infrastructure.Factories;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyPool : PoolBase<EnemyView>
    {
        private readonly HashSet<EnemyView> activeEnemies;
        public EnemyPool(FactoryBase factory, int poolSize) : base(factory, poolSize)
        {
            activeEnemies = new(poolSize);
        }

        public void ReleaseEveryone()
        {
            foreach (var enemy in activeEnemies)
            {
                try
                {
                    Pool.Release(enemy);
                }
                catch (InvalidOperationException exception)
                {
                    Debug.Log( $"CATCHED : {exception.Message}");
                }
            }
            activeEnemies.Clear();
        }

        protected override EnemyView Create()
        {
            EnemyView obj = ((EnemyFactory)Factory).CreateEnemy();
            obj.gameObject.SetActive(false);
            return obj;
        }

        protected override void Get(EnemyView obj)
        {
            base.Get(obj);
            activeEnemies.Add(obj);
        }
    }
}