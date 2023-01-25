using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweezers : Holder
{
    public bool hasGarnish;
    public Transform garnishPosition;
    public GameObject _garnish;
    public void SpawnGarnish(GarnishItem grnish)
    {
        if(_garnish!=null)
        {
            _garnish.SetActive(false);
        }
        GarnishItem garnishItem = Instantiate(grnish, garnishPosition);
        _garnish = garnishItem.gameObject;
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
