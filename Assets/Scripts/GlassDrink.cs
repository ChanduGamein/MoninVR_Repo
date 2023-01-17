using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using LiquidVolumeFX;
public class GlassDrink : HolderGlass
{
    public TextMeshProUGUI amountTxt;
    [SerializeField]Transform shaker;
    bool poured;

    [SerializeField] Transform drinkServingPosition;


    //public override void UnGrab()
    //{
    //    base.UnGrab();

    //}
    public override void IncreaseLiquid(float value)
    {
        base.IncreaseLiquid(value);
        liquidVolume.GetComponent<MeshRenderer>().enabled=true;
      //  liquidVolume.level+=value;

    }

    bool called;
    public void IncreseLiquidGradually(float maxAddedAmount)
    {
        if(liquid.localScale.y<maxAddedAmount)
        IncreaseLiquidScale(.01f);
        else
        {
            shaker.GetComponent<Shaker>().PourToGlass = false;
            if (!called)
            {
                SceneController.instance.InvokeCurrentStep();
                called = true;
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
              //  UIManager.instance.grabButton.SetActive(true);
                UIManager.instance.ActivateGrab(hand.glassPosition, hand, this.transform, "MasonJar");
                UIManager.instance.canGrab = true;
            }

    }
    private void OnCollisionEnter(Collision other)
    {

    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
            UIManager.instance.canGrab = false;

            //  UIManager.instance.grabButton.SetActive(false);
        }

    }
}
