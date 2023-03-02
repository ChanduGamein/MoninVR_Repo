using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Jigger : Holder
{
    [SerializeField] Transform rayCastPoint;
    [SerializeField] Shaker shaker;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] float value;
    RaycastHit hit;
    public void Haptics()
    {
        if (hand != null && grabed)
            hand.GetComponent<XRController>().SendHapticImpulse(.5f, .5f);
    }
    public void SetPickUp()
    {
        callTutoral = true;

    }
    public override void Grab()
    {
        base.Grab();
        if(callTutoral)
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
                UIManager.instance.ActivateGrab(hand.jiggerPosition, hand, this.transform, "Jigger");
                //   UIManager.instance.grabButton.SetActive(true);
                UIManager.instance.canGrab = true;
                if (hand.handType == HandType.right)
                {
                    liquidParticle.transform.localPosition = poringRight.localPosition;
                }
                else
                {
                    liquidParticle.transform.localPosition = poringLeft.localPosition;

                }
            }
        if (other.tag == "end")
        {
            UnGrab();
        }

    }
    bool calledSound;
    private void Update()
    {
        if (grabed&&haveLiquid)
        {
            if (Physics.Raycast(rayCastPoint.position, Vector3.down, out hit, 20, targetLayer))
            {

                if (liquidVolume.level > 0)
                {
                    if (!calledSound)
                    {
                        calledSound = true;

                        AudioManagerMain.instance.PlaySFX("PouringSmall");
                    }
                    shaker.IncreaseLiquid(value*Time.deltaTime);
                    liquidVolume.level -= .6f * Time.deltaTime;
                    liquidParticle.gameObject.SetActive(true);
                }
                else
                {
                    haveLiquid = false;
                    SceneController.instance.InvokeCurrentStep();
                    liquidParticle.gameObject.SetActive(false);
                    AudioManagerMain.instance.StopSound("PouringSmall");
                    calledSound = false;
                    shaker.DeactivateOutline();
                }
            }
            else
            {
                liquidParticle.gameObject.SetActive(false);
                AudioManagerMain.instance.StopSound("PouringSmall");
                calledSound = false;
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
            //   UIManager.instance.grabButton.SetActive(false);
            UIManager.instance.canGrab = false;
         //   UIManager.instance.ExitTrigger();


        }
    }
}
