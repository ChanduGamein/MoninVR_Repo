using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweezers : Holder
{
    public bool hasGarnish;
    public Transform garnishPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (!grabed)
            if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
            {
                hand = other.GetComponent<HandHolder>();
                UIManager.instance.ActivateGrab(hand.scoopPositon, hand, this.transform,"IceScoop");
                UIManager.instance.canGrab = true;
            }
        if (other.tag == "end")
        {
            UnGrab();
        }
        if (!hasGarnish )
            if (other.tag == "Garnish")
            {
                GarnishItem garnishItem = Instantiate(other.GetComponent<Garnish>().garnish, garnishPosition);
                garnishItem.tweezers = this;
                hasGarnish = true;

            }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
            UIManager.instance.canGrab = false;

        }
    }
}
