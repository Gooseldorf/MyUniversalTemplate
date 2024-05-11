using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner
{
    public Canvas canvas;
    public Vector2 GetSpawnPoint(Vector2 objectSize)
    {
        RectTransform rectTransform = canvas.GetComponent<RectTransform>();

        float x = rectTransform.rect.width / 2 - objectSize.x;
        float y = rectTransform.rect.height / 2 + objectSize.y;
        
        Vector3 spawnPointLocal= new Vector3(Random.Range(-x,x), y ,0);
        
        Vector3 spawnPointWorld = rectTransform.TransformPoint(spawnPointLocal);

        Debug.Log(spawnPointWorld);

        return spawnPointWorld;
    }
}
