using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObjects : MonoBehaviour
{
    [SerializeField] GameObject iceCubePrefab;
    List<GameObject> _pool_IceCubes = new List<GameObject>();
    // Start is called before the first frame update
    int counter = 0;
    private GameObject SpawnedObjectsFromPool(GameObject objPrefab,List<GameObject> pool,Vector3 location)
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                pool[i].transform.position = location;
                pool[i].gameObject.SetActive(true);
                return pool[i];
                
            }
        }
        if (counter <= 4)
        {
            GameObject obj = Instantiate(objPrefab, location, Quaternion.identity);
            pool.Add(obj);
            counter++;
            return obj;
        }
        return null;
    }
    public GameObject GetPooledObject(Vector3 location)
    {
        return SpawnedObjectsFromPool(iceCubePrefab, _pool_IceCubes, location);
    }
}
