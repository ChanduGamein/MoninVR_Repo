using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using LiquidVolumeFX;
public class GlassDrink : Holder
{
    public TextMeshProUGUI amountTxt;
    [SerializeField]Transform shaker;
    bool poured;
    public List<Transform> garnishPositions = new List<Transform>();
    public LiquidVolume liquidVolume;
    int counter = 0;
    [SerializeField] Transform drinkServingPosition;

    public void SetGarnishTransform(Transform garnish)
    {
        if (counter < garnishPositions.Count)
        {
            garnish.parent = transform;
            garnish.position = garnishPositions[counter].position;
            garnish.rotation = garnishPositions[counter].rotation;
            garnish.localScale = garnishPositions[counter].localScale;
            counter++;
        }
    }
    //public override void UnGrab()
    //{
    //    base.UnGrab();

    //}
    public void IncreaseLiquid(float value)
    {
        Debug.Log("increase");
        liquidVolume.GetComponent<MeshRenderer>().enabled=true;
        liquidVolume.level+=value;

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
