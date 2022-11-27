using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private int waves = 7;
    private int waveCounter = 0;
    private bool spawnWave = true;

    private void Awake()
    {
        ObjectPooler.current.pooledAmount = waves;
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 playerPosition = Player.Instance.transform.position;
        float posX = Random.Range(-9f, 9f);
        float posZ = Random.Range(-9f, 9f);
        return new(playerPosition.x + posX, 1f, playerPosition.z + posZ);
    }

    void SpawnEnemies(int wave)
    {
        for (int i = 0; i < wave; i++)
        {
            GameObject enemyObj = ObjectPooler.current.GetPooledObject(0);
            enemyObj.transform.position = GetRandomPosition();
            enemyObj.SetActive(true);
        }
    }

    void Update()
    {
        if (spawnWave == true && waveCounter < waves)
        {
            waveCounter += 1;
            SpawnEnemies(waveCounter);
            spawnWave = false;
        }

        foreach (GameObject obj in ObjectPooler.current.pooledObjects1)
        {
            spawnWave = true;
            if (obj.activeInHierarchy)
            {
                spawnWave = false;
                break;
            }
        }

    }
}
