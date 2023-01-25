using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NickAndNorah : GlassDrink
{

    public override void IncreaseLiquid(float value)
    {
        base.IncreaseLiquid(value);
        liquidVolume.GetComponent<MeshRenderer>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "end")
        {
            UnGrab();
        }

        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
            hand = other.GetComponent<HandHolder>();
            UIManager.instance.ActivateGrab(hand.coupePosition, hand, this.transform, "Spoon");
            UIManager.instance.canGrab = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
            UIManager.instance.canGrab = false;

            // UIManager.instance.grabButton.SetActive(false);
        }
    }
}
