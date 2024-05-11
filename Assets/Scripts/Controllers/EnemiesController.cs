using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Enemy;
using Game.Spawners;
using Game.VFX.Explosion;
using Interfaces;
using UnityEngine;

namespace Controllers
{
    public class EnemiesController : IDispose
    {
        private readonly EnemyPool enemyPool;
        private readonly EnemySpawner spawner;
        private readonly ExplosionController explosionController;

        private List<EnemyController> spawnedEnemies = new ();
        
        public EnemiesController(EnemyPool enemyPool, EnemySpawner spawner, ExplosionController explosionController)
        {
            this.enemyPool = enemyPool;
            this.spawner = spawner;
            this.explosionController = explosionController;
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
            spawnedEnemies.Remove(controller);
            controller.Dispose();
            enemyPool.Pool.Release(view);
            explosionController.Explode(view.transform.position);
        }

        public void Dispose()
        {
            foreach (EnemyController enemy in spawnedEnemies)
                enemy.Dispose();
            
            spawnedEnemies.Clear();
        }
    }
}