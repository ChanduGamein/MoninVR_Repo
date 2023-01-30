using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EspressoCup : Holder
{
    [SerializeField] Transform spellPoint;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] float value;
    [SerializeField] Shaker shaker;
    RaycastHit hit;
    bool called;
    private void Update()
    {
        if (grabed)
        {
            if (Physics.Raycast(spellPoint.position, Vector3.down, out hit, 20, targetLayer))
            {

                if (liquidVolume.level > 0)
                {
                    shaker.liquidVolume.level += value * Time.deltaTime;
                    liquidVolume.level -= .1f * Time.deltaTime;
                    liquidParticle.gameObject.SetActive(true);
                }
                else
                {
                    if (!called)
                    {
                        called = true;
                        SceneController.instance.InvokeCurrentStep();
                        liquidVolume.GetComponent<MeshRenderer>().enabled = false;
                        liquidParticle.gameObject.SetActive(false);

                    }

                }
            }
            else
            {
                liquidParticle.gameObject.SetActive(false);

            }

        }
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
            UIManager.instance.ActivateGrab(hand.cupPosition, hand, this.transform, "Opener");
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
