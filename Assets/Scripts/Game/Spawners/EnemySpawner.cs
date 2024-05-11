using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Spawners
{
    public class EnemySpawner
    {
        private readonly Bounds spawnArea;

        public EnemySpawner(Bounds spawnArea)
        {
            this.spawnArea = spawnArea;
        }
    
        public Vector2 GetSpawnPoint(Vector2 objectSize)
        {
            float x = spawnArea.size.x / 2 - objectSize.x;
            float y = spawnArea.size.y / 2 + objectSize.y;
        
            Vector3 spawnPoint = new Vector3(Random.Range(-x, x), y ,0);

            return spawnPoint;
        }
    }
}
