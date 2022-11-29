using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;
    public int waveCounter = 0;
    public int waves = 7;
    public bool gameWon = false;

    private bool spawnWave = true;

    private void Awake()
    {
        Instance = this;
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

        // counting active enemies
        int i = 0;
        foreach (GameObject obj in ObjectPooler.current.pooledObjects1)
        {
            if (obj.activeInHierarchy)
            {
                i++;
            }
        }

        // if we killed everyone and it's a last wave - we won!
        if (i == 0 && waveCounter == waves)
        {
            gameWon = true;
        }

        // if we killed everyone and it's not a last wave - spawn another wave!
        else if (i == 0 && waveCounter < waves)
        {
            spawnWave = true;
        }

        // spawn no one if there's at least one active enemy
        else if (i > 0)
        {
            spawnWave = false;
        }
    }
}
