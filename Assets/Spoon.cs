using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spoon : Holder
{
    [SerializeField] LongGlass longGlass;
    // Start is called before the first frame update
    public override void Grab()
    {
        base.Grab();
        if (!picked)
        {
            SceneController.instance.InvokeCurrentStep();
            picked = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!grabed)
            if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
            {
                hand = other.GetComponent<HandHolder>();
                UIManager.instance.ActivateGrab(hand.shakerPositon, hand, this.transform, "Shaker");
                //   UIManager.instance.grabButton.SetActive(true);
                UIManager.instance.canGrab = true;
            }
        if (other.tag == "end")
        {
            UnGrab();
        }
        if(other.gameObject.tag=="LongGlass")
        {
            UnGrab();
            longGlass.Stir();
            gameObject.SetActive(false);
        }
    }

}
