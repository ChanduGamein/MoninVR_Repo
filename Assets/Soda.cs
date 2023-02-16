using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soda : SprinkleWater
{
    public bool isCapRemoved;
    public GameObject cap;
    // Start is called before the first frame update
    bool _calledSound;
    public void RemoveCap()
    {
        cap.gameObject.SetActive(false);
        isCapRemoved = true;
        if(!_calledSound)
        {
            _calledSound = true;
            AudioManagerMain.instance.PlaySFX("Cap");

        }
    }
    public override void Grab()
    {
        base.Grab();
        if(!isCapRemoved)
        {
            SceneController.instance.opener.PointAtItem();
        }
    }
    protected override void Update()
    {
        if(isCapRemoved)
        base.Update();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "end")
        {
            UnGrab();
        }
        if (SceneController.instance.isFridgeOpen)
        {
            if(!grabed)
            if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
            {
                hand = other.GetComponent<HandHolder>();
                UIManager.instance.ActivateGrab(hand.smallBottlePosition, hand, this.transform, "SmallBottle");
               // UIManager.instance.canGrab = true;
            }
        }
    }


}
