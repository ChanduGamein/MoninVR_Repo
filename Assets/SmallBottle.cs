using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBottle : Holder
{
    public bool isCapRemoved;
    [SerializeField] Transform pourPoint;
    [SerializeField] Holder holder;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] float liquidLevel;
    [SerializeField] float shakerLevel;
    RaycastHit hit;
    bool checkPouring = true;
    [SerializeField] FillLiquidUI liquidUI;
    public int liquidMLPerPump;
    public int liquidMLFullAmount;
    public string itemName;
    public override void Grab()
    {
        base.Grab();
        //if(!isCapRemoved)
        //UIManager.instance.pointerTutorial.SetActive(true);
    }
    protected  void Update()
    {
        if (grabed && checkPouring&& isCapRemoved)
        {
            if (Physics.Raycast(pourPoint.position, Vector3.down, out hit, 20, targetLayer))
            {

                if (holder.liquidVolume.level < liquidLevel)
                {
                    liquidParticle.gameObject.SetActive(true);
                    holder.liquidVolume.level += .2f * Time.deltaTime;
                    if(liquidUI!=null)
                    {
                        liquidUI.gameObject.SetActive(true);
                        liquidUI.SetAmount(itemName, liquidMLFullAmount);
                        SceneController.instance.fillLiquidStatic.SetAmount(itemName, liquidMLFullAmount);
                        // SceneController.instance.SetShakerLiquidAmount(itemName, liquidMLFullAmount, .1f);

                        liquidUI.SetFillAmount(holder.liquidVolume.level, liquidLevel, liquidMLFullAmount);
                        SceneController.instance.fillLiquidStatic.SetFillAmount(holder.liquidVolume.level, liquidLevel, liquidMLFullAmount);
                        SceneController.instance.fillLiquidStatic.gameObject.SetActive(true);
                    }

                }
                else
                {
                    checkPouring = false;
                    liquidParticle.gameObject.SetActive(false);

                    holder.haveLiquid = true;
                    SceneController.instance.fillLiquidUI.gameObject.SetActive(false);
                    SceneController.instance.fillLiquidStatic.gameObject.SetActive(false);
                    SceneController.instance.InvokeCurrentStep();
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
