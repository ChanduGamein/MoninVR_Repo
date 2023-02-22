using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField]
    Vector3[] vectorArrays;   
    public bool spawn;
   // public Poolingmanager PoolingmanagerObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        Poolingmanager.instance.SetObjectHolder(this.gameObject);
        Poolingmanager.instance.InstantiateIce();
    }
    // Update is called once per frame
    public void Spawn()
    {
        spawn = false;
        for (int i = 0; i < Poolingmanager.instance.prefablist.Count; i++)
        {
            Poolingmanager.instance.prefablist[i].SetActive(false);
            Poolingmanager.instance.prefablist[i].GetComponent<Rigidbody>().useGravity = false;
            Poolingmanager.instance.prefablist[i].transform.SetParent(this.transform);
            Poolingmanager.instance.prefablist[i].transform.localPosition = vectorArrays[i];
            Poolingmanager.instance.prefablist[i].transform.localRotation = Quaternion.identity;
            //  Poolingmanager.instance.prefablist[i].transform.localScale = Vector3.one;
        }
        StartCoroutine(DelayActivation());
    }
    //void Update()
    //{
    //    if (spawn)
    //    {

    //    }
    //}
    IEnumerator DelayActivation()
    {
        yield return new WaitForSeconds(.6f);
        for (int i = 0; i < Poolingmanager.instance.prefablist.Count; i++)
        {
            Poolingmanager.instance.prefablist[i].SetActive(true);
            Poolingmanager.instance.prefablist[i].GetComponent<Rigidbody>().useGravity = true;

        }
    }
}
