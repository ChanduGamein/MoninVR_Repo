using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.XR.Interaction.Toolkit;

public class Pump : Holder
{
    public int liquidMLPerPump;
    public int liquidMLFullAmount;
    //public items itemType;
    public bool handOnItem;
    public bool shakerInPlace;
    public string itemName;
    [SerializeField] Transform bottlePump;
   // [SerializeField] ParticleSystem liquidParticle;
    [SerializeField] Holder itemToFill;
    [SerializeField] float amountToAdd;
    [SerializeField] Transform spellPoint;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] float pumpPressedPositionY;
    [SerializeField] float pumpOriginalPosition;
    [SerializeField] Collider objectCollider;
    // Update is called once per frame

    void Update()
    {
        Debug.DrawRay(spellPoint.position, Vector3.down,Color.green);
        if(Physics.Raycast(spellPoint.position,Vector3.down,10,targetLayer))
        {
              shakerInPlace = true;

        }
        else
        {
            shakerInPlace = false;

        }
    }
    public void PlayPumpAnimation()
    {
        StartCoroutine(PumpAnimation());
    }
    public void ActivateCollider()
    {
        if (!called)
        {
            objectCollider.enabled = true;
            addPump = true;

        }
    }
    bool called = false;
    IEnumerator PumpAnimation()
    {
        if(isPointer)
        {
            outline.enabled = false;
        }
        objectCollider.enabled = false;
        AudioManagerMain.instance.PlaySFX("Pump");
          bottlePump.DOLocalMoveY(pumpPressedPositionY, .6f).OnComplete (() => bottlePump.DOLocalMoveY(pumpOriginalPosition, .6f).OnComplete(()=>ActivateCollider()));

         liquidParticle.Play();
        itemToFill.IncreaseLiquid(amountToAdd);
        for (int i = 0; i < SceneController.instance.currentRecipe.RecipeItems.Count; i++)
        {

            if (SceneController.instance.currentRecipe.RecipeItems[i].itemType == itemType)
            {
                SceneController.instance.currentRecipe.RecipeItems[i].numberOfItemsRequired -= 1;
                if (SceneController.instance.currentRecipe.RecipeItems[i].numberOfItemsRequired <= 0)
                {
                    SceneController.instance.currentAddedAmount = 0;
                    SceneController.instance.InvokeCurrentStep();
                    called = true;
                    yield return new WaitForSeconds(.7f);
                    SceneController.instance.ResetShakerLiquidUI();
                }
            }
        }

    }
    bool addPump=true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
            if(!(other.GetComponent<HandHolder>().grabbing))
            handOnItem = true;
        }

        if(handOnItem&&shakerInPlace)
        {
            if (addPump)
            {
                handOnItem = false;
                addPump = false;
                PlayPumpAnimation();
                SceneController.instance.SetShakerLiquidAmount(itemName, liquidMLFullAmount, liquidMLPerPump);
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
            handOnItem = false;
        }
        if (other.gameObject.tag == "Shaker")
        {
            shakerInPlace = false;
        }
    }

}
