using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : Holder
{
    private void OnTriggerEnter(Collider other)
    {
        //if (!grabed)
        //    if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        //    {
        //        hand = other.GetComponent<HandHolder>();
        //        UIManager.instance.ActivateGrab(hand.shakerPositon, hand, this.transform);
        //    }
        if (other.tag == "end")
        {
            UnGrab();
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
        }
    }


}
