using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandHolder : MonoBehaviour
{
    // Start is called before the first frame update
    public Holder currentHolder;
    public Transform scoopPositon;
    public Transform shakerPositon;
    public Transform garnishPosition;
    public Collider handCollider;
    public bool hasGarnish;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!hasGarnish)
        if(other.tag=="Garnish")
        {
         GarnishItem garnishItem=  Instantiate(other.GetComponent<Garnish>().garnish,garnishPosition);
                garnishItem.hand = this;
                handCollider.enabled = false;
            hasGarnish = true;
        }
    }
}
