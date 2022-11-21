using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy;
    private int waves = 7;
    private int waveCounter = 0;
    [SerializeField] List<GameObject> enemies = new List<GameObject>();

    private Vector3 GetRandomPosition()
    {
        Vector3 playerPosition = FindObjectOfType<Player>().transform.position;
        float posX = Random.Range(-9f, 9f);
        float posZ = Random.Range(-9f, 9f);
        return new(playerPosition.x + posX, 1f, playerPosition.z + posZ);
    }

    void SpawnEnemies(int wave)
    {
        int i;
        for (i = 0; i <= wave; i++)
        {
            Instantiate(enemy, GetRandomPosition(), enemy.transform.rotation);
            enemies.Add(enemy);
        }
        
    }

    void Update()
    {

        if (enemies.Count == 0 && waveCounter < waves)
        {
            SpawnEnemies(waveCounter);
            waveCounter += 1;
        }
        
        if (waveCounter <= waves)
        {
            var enemyObject = FindObjectOfType<EnemyBehaviour>();
            if (enemyObject == null)
            {
                enemies.Clear();
            }
        }
    }
}
