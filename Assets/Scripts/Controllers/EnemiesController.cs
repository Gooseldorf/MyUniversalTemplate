using System;
using Game.Enemy;
using Game.Spawners;
using Interfaces;
using UnityEngine;

namespace Controllers
{
    public class EnemiesController : IDispose
    {
        private readonly EnemyPool enemyPool;
        private readonly EnemySpawner spawner;
        public event Action AllDead;
        
        public EnemiesController(EnemyPool enemyPool, EnemySpawner spawner)
        {
            this.enemyPool = enemyPool;
            this.spawner = spawner;
        }

        public void SpawnEnemy()
        {
            EnemyView enemyView = enemyPool.Pool.Get();
            enemyView.transform.position = spawner.GetSpawnPoint(enemyView.Size);
            EnemyController enemyController = new EnemyController(enemyView);
            enemyController.Init();
            enemyController.Dead += KillEnemy;
        }

        private void KillEnemy(EnemyController controller, EnemyView view)
        {
            controller.Dead -= KillEnemy;
            controller.Dispose();
            enemyPool.Pool.Release(view);
        }

        public void Dispose()
        {
            
        }
    }
}