using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TIKKIMug : HolderGlass
{
    bool poured;



    //public override void UnGrab()
    //{
    //    base.UnGrab();

    //}
    public override void IncreaseLiquid(float value)
    {
        base.IncreaseLiquid(value);
        liquidVolume.GetComponent<MeshRenderer>().enabled = true;
    }

    bool called;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "end")
        {
            UnGrab();
        }
        if(!grabed)
        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
            hand = other.GetComponent<HandHolder>();
            //  UIManager.instance.grabButton.SetActive(true);
            UIManager.instance.ActivateGrab(hand.tikkiPosition, hand, this.transform, "Tikki");
            UIManager.instance.canGrab = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
            UIManager.instance.canGrab = false;

            //  UIManager.instance.grabButton.SetActive(false);
        }

    }
}
