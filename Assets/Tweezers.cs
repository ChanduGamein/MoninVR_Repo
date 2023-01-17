using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweezers : Holder
{
    public bool hasGarnish;
    public Transform garnishPosition;

    public void SpawnGarnish(GarnishItem grnish)
    {
        GarnishItem garnishItem = Instantiate(grnish, garnishPosition);
        garnishItem.tweezers = this;
        
       // hasGarnish = true;

    }
    private void OnTriggerEnter(Collider other)
    {

        //if (!hasGarnish )
        //    if (other.tag == "Garnish")
        //    {

        //    }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
            UIManager.instance.canGrab = false;

        }
    }
}
