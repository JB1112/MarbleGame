using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class ObjectPool : Singleton<ObjectPool>
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> Pools;
    public Dictionary<string, Queue<GameObject>> PoolDictionary;

    private void Awake()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        if (PoolDictionary != null)
        {
            foreach (var pool in PoolDictionary.Values)
            {
                while (pool.Count > 0)
                {
                    GameObject obj = pool.Dequeue();
                    if (obj != null)
                    {
                        Destroy(obj);
                    }
                }
            }
        }

        PoolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (var pool in Pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            PoolDictionary.Add(pool.tag, objectPool);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializePool();
    }

    public GameObject SpawnFromPool(string tag, Vector3 position)
    {
        if (!PoolDictionary.ContainsKey(tag))
            return null;

        GameObject obj = null;

        // 사용 가능한 오브젝트 찾기
        for (int i = 0; i < PoolDictionary[tag].Count; i++)
        {
            obj = PoolDictionary[tag].Dequeue();// 오브젝트 꺼내기

            if (!obj.activeInHierarchy) // 비활성화된 오브젝트 찾으면 사용
            {
                obj.transform.position = position;
                obj.SetActive(true);
                PoolDictionary[tag].Enqueue(obj); // 다시 Queue에 추가
                return obj;
            }

            PoolDictionary[tag].Enqueue(obj); // 다시 Queue에 추가 (사용 중이라면 다시 뒤로 보냄)
        }

        return null;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}
