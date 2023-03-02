using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poolingmanager : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    public int maxAmount;
    public List<GameObject> prefablist;
    public GameObject ObjHoder;
    public static Poolingmanager instance;
    private void Awake()
    {
        instance = this;
    }
    public void SetObjectHolder(GameObject scoop)
    {
        ObjHoder = scoop;
    }
    public void InstantiateIce()
    {
        prefablist = new List<GameObject>();
        for (int i = 0; i < maxAmount; i++)
        {
            GameObject B = Instantiate(prefab, ObjHoder.transform);
            prefablist.Add(B);
            B.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
