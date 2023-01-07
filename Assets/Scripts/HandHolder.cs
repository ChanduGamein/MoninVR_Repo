using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HandType
{
    left,right
}
public class HandHolder : MonoBehaviour
{
    // Start is called before the first frame update
    public Holder currentHolder;
    public Transform scoopPositon;
    public Transform shakerPositon;
    public Transform garnishPosition;
    public Transform glassPosition;
    public Collider handCollider;
    public bool hasGarnish;
    public HandType handType;
    public bool grabbing;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Ungrab()
    {
        currentHolder.UnGrab();
        grabbing = false;

        UIManager.instance.canGrab = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!hasGarnish&&!grabbing)
        if(other.tag=="Garnish")
        {
         GarnishItem garnishItem=  Instantiate(other.GetComponent<Garnish>().garnish,garnishPosition);
                garnishItem.hand = this;
                handCollider.enabled = false;
            hasGarnish = true;
            
        }
    }
}
