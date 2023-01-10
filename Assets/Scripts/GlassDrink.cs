using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using LiquidVolumeFX;

public class GlassDrink : Holder
{
    public TextMeshProUGUI amountTxt;
    [SerializeField] Transform shakerPourPosition;
    [SerializeField]Transform shaker;
    bool poured;
    public List<Transform> garnishPositions = new List<Transform>();
    [SerializeField] LiquidVolume liquidVolume;
    public void IncreaseLiquid(float value)
    {
        liquidVolume.gameObject.SetActive(true);
        liquidVolume.level += value;
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
                UIManager.instance.ActivateGrab(hand.glassPosition, hand, this.transform);
                UIManager.instance.canGrab = true;
            }
        if (other.gameObject.tag == "Shaker")
        {
          //  PourIntoGlass();
           // UIManager.instance.pourButton.SetActive(true);
          //  shaker = other.transform;
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
