using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    public int SpawnTimeMin = 10;
    public int SpawnTimeMax = 20;

    private bool ReadyToSpawn = true;

    private void Awake()
    {
        ObjectPooler.current.pooledAmount1 = 9;
    }

    private Vector3 GetRandomPosition()
    {
        float posX = Random.Range(-45f, 45f);
        float posZ = Random.Range(-45f, 45f);
        return new(posX, 1f,posZ);
    }

    void SpawnCollectables(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = ObjectPooler.current.GetPooledObject(1);
            if (obj == null) return;

            obj.transform.position = GetRandomPosition();
            obj.SetActive(true);
        }
    }

    void Update()
    {

        if (ReadyToSpawn)
        {
            StartCoroutine(Timer(Random.Range(SpawnTimeMin, SpawnTimeMax)));
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
