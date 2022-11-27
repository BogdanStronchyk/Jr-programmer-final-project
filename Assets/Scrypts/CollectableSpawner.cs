using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    public GameObject HealingCollectable;
    public GameObject Ammo;

    private int waves = 7;
    private bool ReadyToSpawn = true;

    private void Awake()
    {
        ObjectPooler.current.pooledAmount = waves;
    }

    private Vector3 GetRandomPosition()
    {
        float posX = Random.Range(-45f, 45f);
        float posZ = Random.Range(-45f, 45f);
        return new(posX, 1f,posZ);
    }

    void SpawnCollectables(int wave)
    {
        for (int i = 0; i < wave; i++)
        {
            GameObject enemyObj = ObjectPooler.current.GetPooledObject(1);
            enemyObj.transform.position = GetRandomPosition();
            enemyObj.SetActive(true);
        }
    }

    void Update()
    {
        if (ReadyToSpawn)
        {
            StartCoroutine(Timer(Random.Range(15, 30)));
            ReadyToSpawn = false;
        }

    }

    IEnumerator Timer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SpawnCollectables(Random.Range(1, 3));
        ReadyToSpawn = true;
    }
}
