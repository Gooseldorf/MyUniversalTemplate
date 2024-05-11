#if UNITY_EDITOR

using Game.Enemy;
using Infrastructure.DI;
using NaughtyAttributes;
using UnityEngine;

public class Tester : MonoBehaviour
{
    [SerializeField] private GameInstaller installer;
    [SerializeField] private Canvas canvas;
    private EnemySpawner enemySpawner;
    private IEnemyFactory enemyFactory;
    
    [Button]
    private async void CheckTester()
    {
        Debug.Log("Tester checked");
        enemySpawner = new();
        enemySpawner.canvas = canvas;
        enemyFactory = installer.Resolve<IEnemyFactory>();
        await enemyFactory.WarmUp();
    }

    [Button]
    private void CreateEnemy()
    {
        EnemyView enemy = enemyFactory.CreateEnemy();
        Collider collider = enemy.GetComponentInChildren<Collider>();
        Vector3 size = collider.bounds.size;
        
        enemy.transform.position = enemySpawner.GetSpawnPoint(new Vector2(size.x, size.z));
    }
}

#endif
