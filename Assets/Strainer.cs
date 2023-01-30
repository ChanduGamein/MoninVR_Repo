using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strainer : Holder
{

    private void OnTriggerEnter(Collider other)
    {
        if (!grabed)
            if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
            {
                hand = other.GetComponent<HandHolder>();
                UIManager.instance.ActivateGrab(hand.strainerPosition, hand, this.transform, "Opener");
                //   UIManager.instance.grabButton.SetActive(true);
                UIManager.instance.canGrab = true;
            }
        if (other.tag == "end")
        {
            UnGrab();
        }
        if (other.gameObject.tag == "Shaker")
        {
            other.GetComponent<Shaker>().AddStrainer();
            UnGrab();
            GetComponent<BoxCollider>().enabled = false;
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
            //   UIManager.instance.grabButton.SetActive(false);
          //  UIManager.instance.canGrab = false;

        }
    }
}
