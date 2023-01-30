using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opener : Holder
{
    private void OnTriggerEnter(Collider other)
    {
        if (!grabed)
            if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
            {
                hand = other.GetComponent<HandHolder>();
                UIManager.instance.ActivateGrab(hand.bottleOpenerPosition, hand, this.transform, "Opener");
                //   UIManager.instance.grabButton.SetActive(true);
                UIManager.instance.canGrab = true;
            }
        if (other.tag == "end")
        {
            UnGrab();
        }
        if (other.tag=="Soda")
        {
            Debug.Log("sodaa");
            other.GetComponent<Soda>().RemoveCap();
        }
    }
    public override void Grab()
    {
        base.Grab();
        UIManager.instance.pointerTutorial.SetActive(false);
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
            //   UIManager.instance.grabButton.SetActive(false);
            UIManager.instance.canGrab = false;

        }
    }
}
