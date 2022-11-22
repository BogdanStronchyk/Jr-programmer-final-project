using System.Text;
using UnityEngine;
using UnityEngine.Pool;

// This component returns the object to the pool when the OnDisable event is received.
[RequireComponent(typeof(GameObject))]
public class ReturnToPool : MonoBehaviour
{
    public GameObject obj;
    public IObjectPool<GameObject> pool;

    void Start()
    {
        obj = GetComponent<GameObject>();
    }

    private void OnDisable()
    {
        // Return to the pool
        pool.Release(obj);
    }

}

// This example spans a random number of ParticleSystems using a pool so that old systems can be reused.
public class PoolExample : MonoBehaviour
{
    public static PoolExample Instance;
    public GameObject ObjectToPool;
    public enum PoolType
    {
        Stack,
        LinkedList
    }

    public PoolType poolType;

    // Collection checks will throw errors if we try to release an item that is already in the pool.
    public bool collectionChecks = true;
    public int maxPoolSize;

    private void Awake()
    {
        Instance = this;
    }

    IObjectPool<GameObject> m_Pool;

    public IObjectPool<GameObject> Pool
    {
        get
        {
            if (m_Pool == null)
            {
                if (poolType == PoolType.Stack)
                    m_Pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, 10, maxPoolSize);
                else
                    m_Pool = new LinkedPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, maxPoolSize);
            }
            return m_Pool;
        }
    }

    GameObject CreatePooledItem()
    {
        var go = ObjectToPool;

        // This is used to return ParticleSystems to the pool when they have stopped.
        var returnToPool = go.AddComponent<ReturnToPool>();
        returnToPool.pool = Pool;

        return go;
    }

    // Called when an item is returned to the pool using Release
    void OnReturnedToPool(GameObject system)
    {
        system.gameObject.SetActive(false);
    }

    // Called when an item is taken from the pool using Get
    void OnTakeFromPool(GameObject system)
    {
        system.gameObject.SetActive(true);
    }

    // If the pool capacity is reached then any items returned will be destroyed.
    // We can control what the destroy behavior does, here we destroy the GameObject.
    void OnDestroyPoolObject(GameObject system)
    {
        Destroy(system.gameObject);
    }

}