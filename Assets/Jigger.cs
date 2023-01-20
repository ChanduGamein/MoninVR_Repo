using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jigger : Holder
{
    [SerializeField] Transform rayCastPoint;
    public float shakerLevel;
    [SerializeField] Shaker shaker;
    [SerializeField] LayerMask targetLayer;
    public bool haveLiquid;
    RaycastHit hit;
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
            }
        if (other.tag == "end")
        {
            UnGrab();
        }

    }
    private void Update()
    {
        if (grabed&&haveLiquid)
        {
            if (Physics.Raycast(rayCastPoint.position, Vector3.down, out hit, 20, targetLayer))
            {
                if (flowRenderer != null)
                    flowRenderer.enabled = true;
                if (liquidVolume.level > 0)
                {
                    shaker.liquidVolume.level += .03f * Time.deltaTime;
                    liquidVolume.level -= .1f * Time.deltaTime;
                    flowRenderer.enabled = true;
                }
                else
                {
                    haveLiquid = false;
                    SceneController.instance.InvokeCurrentStep();
                    flowRenderer.enabled = false;

                }
            }
            else
            {
                flowRenderer.enabled = false;

            }

        }
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
