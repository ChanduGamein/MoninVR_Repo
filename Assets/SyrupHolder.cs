using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyrupHolder : Holder
{
    [SerializeField] Transform pourPoint;
    [SerializeField] Jigger jigger;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] float liquidLevel;
    RaycastHit hit;
    bool checkPouring = true;
    //[SerializeField] FillLiquidUI liquidUI;
    public int liquidMLPerPump;
    public int liquidMLFullAmount;
    public string itemName;
    [SerializeField] float shakerLevel;
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

    }
    [SerializeField]bool calledSound;

    private void Update()
    {
        if(grabed&&checkPouring)
        {
            if (Physics.Raycast(pourPoint.position, Vector3.down, out hit, 20, targetLayer))
            {

                if (jigger.liquidVolume.level < liquidLevel)
                {
                    if (!calledSound)
                    {
                        AudioManagerMain.instance.PlaySFX("PouringSmall");
                        calledSound = true;
                    }
                    jigger.IncreaseLiquid( .1f * Time.deltaTime*3);

                    SceneController.instance.fillLiquidStatic.SetAmount(itemName, liquidMLFullAmount);
                    // SceneController.instance.SetShakerLiquidAmount(itemName, liquidMLFullAmount, .1f);

                //    liquidUI.SetFillAmount(jigger.liquidVolume.level, liquidLevel, liquidMLFullAmount);
                    SceneController.instance.fillLiquidStatic.SetFillAmount(jigger.liquidVolume.level, liquidLevel, liquidMLFullAmount);

                    SceneController.instance.fillLiquidStatic.gameObject.SetActive(true);
                    liquidParticle.gameObject.SetActive(true);
                }
                else
                {
                    checkPouring = false;
                  //  jigger.shakerLevel = shakerLevel;
                    jigger.haveLiquid = true;
                 //   SceneController.instance.fillLiquidUI.gameObject.SetActive(false);
                    SceneController.instance.fillLiquidStatic.gameObject.SetActive(false);
                    SceneController.instance.InvokeCurrentStep();
                    liquidParticle.gameObject.SetActive(false);
                    AudioManagerMain.instance.StopSound("PouringSmall");
                    calledSound = false;

                }
            }
            else
            {
                liquidParticle.gameObject.SetActive(false);
                AudioManagerMain.instance.StopSound("PouringSmall");
                calledSound = false;
            }

        }
        else
        {
            liquidParticle.gameObject.SetActive(false);

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
