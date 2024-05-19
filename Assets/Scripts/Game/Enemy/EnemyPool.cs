using System;
using System.Collections.Generic;
using Infrastructure;
using Infrastructure.Factories;
using Infrastructure.Pools;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyPool : ComponentPoolBase<EnemyView>
    {
        private readonly HashSet<EnemyView> activeEnemies;
        public EnemyPool(CachedGameObjectFactoryBase factory, int poolSize) : base(factory, poolSize)
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
            obj.transform.SetParent(Container.transform);
            obj.gameObject.SetActive(false);
            return obj;
        }

        protected override void Get(EnemyView obj)
        {
            base.Get(obj);
            if(!activeEnemies.Contains(obj))
                activeEnemies.Add(obj);
        }

        protected override void Release(EnemyView obj)
        {
            /*if(activeEnemies.Contains(obj))
                activeEnemies.Remove(obj);*/
            base.Release(obj);
        }
    }
}