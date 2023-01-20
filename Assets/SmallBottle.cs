using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBottle : Holder
{
    [SerializeField] GameObject cap;
    public bool isCapRemoved;
    [SerializeField] Transform pourPoint;
    [SerializeField] Jigger jigger;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] float liquidLevel;
    [SerializeField] float shakerLevel;
    RaycastHit hit;
    bool checkPouring = true;
    [SerializeField] FillLiquidUI liquidUI;
    public int liquidMLPerPump;
    public int liquidMLFullAmount;
    public string itemName;
    private void Update()
    {
        if (grabed && checkPouring&& isCapRemoved)
        {
            if (Physics.Raycast(pourPoint.position, Vector3.down, out hit, 20, targetLayer))
            {
                if (flowRenderer != null)
                    flowRenderer.enabled = true;
                if (jigger.liquidVolume.level < liquidLevel)
                {
                    jigger.liquidVolume.level += .1f * Time.deltaTime;
                    liquidUI.gameObject.SetActive(true);
                    liquidUI.SetAmount(itemName, liquidMLFullAmount);
                    SceneController.instance.fillLiquidStatic.SetAmount(itemName, liquidMLFullAmount);
                    // SceneController.instance.SetShakerLiquidAmount(itemName, liquidMLFullAmount, .1f);

                    liquidUI.SetFillAmount(jigger.liquidVolume.level, liquidLevel, liquidMLFullAmount);
                    SceneController.instance.fillLiquidStatic.SetFillAmount(jigger.liquidVolume.level, liquidLevel, liquidMLFullAmount);

                    SceneController.instance.fillLiquidStatic.gameObject.SetActive(true);
                }
                else
                {
                    checkPouring = false;
                    if(flowRenderer!=null)
                    flowRenderer.enabled = false;
                    jigger.shakerLevel = shakerLevel;
                    jigger.haveLiquid = true;
                    SceneController.instance.fillLiquidUI.gameObject.SetActive(false);
                    SceneController.instance.fillLiquidStatic.gameObject.SetActive(false);
                    SceneController.instance.InvokeCurrentStep();
                }
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(SceneController.instance.isFridgeOpen)
        if (!grabed)
            if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
            {
                hand = other.GetComponent<HandHolder>();
                UIManager.instance.ActivateGrab(hand.smallBottlePosition, hand, this.transform, "SmallBottle");
                //   UIManager.instance.grabButton.SetActive(true);
                UIManager.instance.canGrab = true;
            }
        if (other.tag == "end")
        {
            UnGrab();
        }


    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
            //   UIManager.instance.grabButton.SetActive(false);
           // UIManager.instance.canGrab = false;

        }
    }
}
