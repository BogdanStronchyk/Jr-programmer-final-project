using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler current;

    public GameObject pooledObject1;
    public GameObject pooledObject2;
    public GameObject pooledObject3;
    public int pooledAmount = 0;
    public int pooledAmount1 = 0;
    public bool willGrow = false;

    public List<GameObject> pooledObjects1;
    public List<GameObject> pooledObjects2;
    // Start is called before the first frame update
    void Awake()
    {
        current = this;
    }

    private void Start()
    {
        pooledObjects1 = new List<GameObject>();
        for(int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject1);
            obj.SetActive(false);
            pooledObjects1.Add(obj);
        }

        if (pooledObject2 != null && pooledObject3 != null)
        {
            pooledObjects2 = new List<GameObject>();
            for (int i = 0; i < pooledAmount1; i++)
            {
                int num = Random.Range(0, 2);
                GameObject obj1 = null;

                switch (num)
                {
                    case 0:
                        obj1 = (GameObject)Instantiate(pooledObject2);
                        break;

                    case 1:
                        obj1 = (GameObject)Instantiate(pooledObject3);
                        break;
                }

                
                obj1.SetActive(false);
                pooledObjects2.Add(obj1);
            }
        }
    }

    /// <summary>
    /// 0 for enemies, 1 for pickups
    /// </summary>
    /// <param name="i"> 
    /// </param>
    /// <returns></returns>
    public GameObject GetPooledObject(int i)
    {
        List<GameObject> pool = null;
        GameObject pooledObject = null;
        switch (i)
        {
            case 0:
                pool = pooledObjects1;
                pooledObject = pooledObject1;
                break;

            case 1:
                pool = pooledObjects2;
                int num = Random.Range(0, 2);

                switch (num)
                {
                    case 0:
                        pooledObject = pooledObject2;
                        break;

                    case 1:
                        pooledObject = pooledObject3;
                        break;
                }
                break;
        }


        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        //if (willGrow)
        //{
        //    GameObject obj = (GameObject)Instantiate(pooledObject);
        //    obj.SetActive(false);
        //    pool.Add(obj);
        //    return obj;
        //}

        return null;
    }

}
