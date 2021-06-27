using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public static EnemySpawner Instance;
    public static List<EnemyV2> enemies;
    public float minSpawnInterval;
    public float maxSpawnInterval;
    public float minDistanceFromAllEnemiesAtSpawn;
    public Vector2 screenBounds;
    float timer;
    public void Start()
    {
        Instance = this;
        enemies = new List<EnemyV2>();
    }
    public void Update()
    {
        if (timer <= 0)
        {
            var challenge = Mathf.Clamp((minSpawnInterval / 2f) * (Time.timeSinceLevelLoad / 120), 0, minSpawnInterval * 0.25f);
            var pos = GetSpawnPosition(20);
            if (pos != null) {
                var e = Instantiate(enemy, pos.Value, Quaternion.identity);
                enemies.Add(e.GetComponent<EnemyV2>());
                timer = Random.Range(minSpawnInterval - challenge, Mathf.Clamp(maxSpawnInterval - (challenge * 4), minSpawnInterval, maxSpawnInterval));
            }
        }
        timer -= Time.deltaTime;
    }
    public Vector2? GetSpawnPosition(int times)
    {
        if (times <= 0)
            return null;

        var pos = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), Random.Range(-screenBounds.y, screenBounds.y));
        
        if (((Vector2)PlayerMovement.Instance.gameObject.transform.position - pos).magnitude < minDistanceFromAllEnemiesAtSpawn)
            return GetSpawnPosition(times - 1);

        for (int i = 0; i < enemies.Count; i++)
            if (((Vector2)enemies[i].transform.position - pos).magnitude < minDistanceFromAllEnemiesAtSpawn)
                return GetSpawnPosition(times - 1);

        return pos;
    }
}
