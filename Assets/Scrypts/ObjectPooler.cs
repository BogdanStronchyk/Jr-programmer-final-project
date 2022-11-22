using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler current;
    public GameObject pooledObject;
    public int pooledAmount = 0;
    public bool willGrow = true;

    public List<GameObject> pooledObjects;
    // Start is called before the first frame update
    void Awake()
    {
        current = this;
    }

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        for(int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        foreach (GameObject obj in pooledObjects)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            return obj;
        }

        return null;
    }

    public void DeactivatePool()
    {
        foreach (GameObject obj in pooledObjects)
        {
            obj.SetActive(false);
        }
    }
}
