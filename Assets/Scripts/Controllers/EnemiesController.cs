﻿using System;
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
        private readonly EnemySpawnArea spawnArea;
        private readonly ExplosionController explosionController;

        private List<EnemyController> spawnedEnemies = new ();
        
        public EnemiesController(EnemyPool enemyPool, EnemySpawnArea spawnArea, ExplosionController explosionController)
        {
            this.enemyPool = enemyPool;
            this.spawnArea = spawnArea;
            this.explosionController = explosionController;
        }

        public void Reset()
        {
            enemyPool.ReleaseEveryone();
            foreach (var enemy in spawnedEnemies)
            {
                enemy.Dispose();
            }
        }

        public void SpawnEnemy()
        {
            EnemyView enemyView = enemyPool.Pool.Get();
            enemyView.transform.position = spawnArea.GetSpawnPoint(enemyView.Size);
            EnemyController enemyController = new EnemyController(enemyView);
            enemyController.Init();
            enemyController.Dead += KillEnemy;
            spawnedEnemies.Add(enemyController);
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