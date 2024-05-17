using System;
using System.Collections.Generic;
using Audio;
using Cysharp.Threading.Tasks;
using Data;
using Game.Spawners;
using Game.VFX.Explosion;
using Game.Weapon.Laser;
using Infrastructure.AssetManagement;
using Interfaces;
using UniRx;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemiesController : IUpdate, IDispose
    {
        private readonly EnemyPool enemyPool;
        private readonly EnemySpawnArea spawnArea;
        private readonly LaserProjectilePool projectilePool;
        private readonly ExplosionController explosionController;
        private readonly IAudioManager audioManager;
        private readonly IAssetProvider assetProvider;

        private LevelData currentLevelData;
        private EnemyData currentEnemyData;
        
        private bool isCanGenerateEnemies = false;
        private float spawnDelay = 0; 
        private float spawnTimer = 0;
        private int generatedEnemyCount = 0;
        private int enemyLeftCount = 0;
        
        private readonly List<EnemyController> spawnedEnemies = new ();

        public IObservable<Unit> AllEnemiesKilledStream => allEnemiesKilledSubject;
        private readonly Subject<Unit> allEnemiesKilledSubject = new Subject<Unit>();
        
        public IObservable<Unit> EnemyKilledStream => enemyKilledSubject;
        private readonly Subject<Unit> enemyKilledSubject = new Subject<Unit>();
        
        public EnemiesController(EnemyPool enemyPool, EnemySpawnArea spawnArea, LaserProjectilePool projectilePool, ExplosionController explosionController,IAudioManager audioManager,IAssetProvider assetProvider)
        {
            this.enemyPool = enemyPool;
            this.spawnArea = spawnArea;
            this.projectilePool = projectilePool;
            this.explosionController = explosionController;
            this.audioManager = audioManager;
            this.assetProvider = assetProvider;
        }

        public void Update()
        {
            foreach (EnemyController spawnedEnemy in spawnedEnemies)
            {
                spawnedEnemy.Update();
            }
            
            if(!isCanGenerateEnemies) return;
            
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnDelay && generatedEnemyCount >= 0)
            {
                SpawnEnemy();
                spawnTimer = 0;
                generatedEnemyCount--;
                
                if (generatedEnemyCount <= 0)
                {
                    isCanGenerateEnemies = false;
                }
            }
        }

        public async UniTask SetupEnemies(LevelData levelData)
        {
            currentLevelData = levelData;
            currentEnemyData = await assetProvider.LoadAddressable<EnemyData>(levelData.EnemyDataAddress);
            spawnDelay = currentLevelData.EnemySpawnDelay;
            generatedEnemyCount = currentLevelData.NumberOfEnemies;
            enemyLeftCount = currentLevelData.NumberOfEnemies;
            isCanGenerateEnemies = true;
        }

        public void Reset()
        {
            spawnTimer = 0;
            enemyPool.ReleaseEveryone();
            projectilePool.ReleaseEveryone();
            foreach (var enemy in spawnedEnemies)
            {
                enemy.Dispose();
            }
        }

        public void Dispose()
        {
            foreach (EnemyController enemy in spawnedEnemies)
                enemy.Dispose();
            
            spawnedEnemies.Clear();
            projectilePool.Dispose();
        }

        private void SpawnEnemy()
        {
            EnemyView enemyView = enemyPool.Pool.Get();
            enemyView.transform.position = spawnArea.GetSpawnPoint(enemyView.Size);
            LaserWeaponController laserWeaponController = new LaserWeaponController(enemyView.EnemyLaserWeapon, projectilePool, audioManager, false);
            EnemyController enemyController = new EnemyController(enemyView, currentEnemyData, laserWeaponController);
            enemyController.Init();
            
            enemyController.Dead += KillEnemy;
            spawnedEnemies.Add(enemyController);
        }

        private void KillEnemy(EnemyController controller, EnemyView view)
        {
            spawnedEnemies.Remove(controller);
            controller.Dead -= KillEnemy;
            controller.Dispose();
            explosionController.Explode(view.transform.position);
            ChangeKilledEnemiesCount();
            enemyPool.Pool.Release(view);
            enemyKilledSubject.OnNext(Unit.Default);
        }

        private void ChangeKilledEnemiesCount()
        {
            enemyLeftCount--;
            if (enemyLeftCount <= 0)
            {
                allEnemiesKilledSubject.OnNext(Unit.Default);
            }
        }
    }
}