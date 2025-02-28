using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectPool : MonoBehaviour
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
        PoolDictionary?.Clear();
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
        {
            Debug.LogError($"[ObjectPool] {tag} 태그를 찾을 수 없습니다.");

            return null;
        }

        GameObject obj = PoolDictionary[tag].Dequeue();

        if (obj == null)
        {
            Debug.LogError($"[ObjectPool] {tag} 오브젝트가 Destroy되었습니다!");
            return null;
        }

        PoolDictionary[tag].Enqueue(obj);
        obj.transform.position = position;
        obj.SetActive(true);
        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}
