using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.XR.Interaction.Toolkit;

public class Pump : MonoBehaviour
{
    public int liquidMLPerPump;
    public int liquidMLFullAmount;
    public items itemType;
    public bool handOnItem;
    public bool shakerInPlace;
    public string itemName;
    [SerializeField] Transform bottlePump;
    [SerializeField] ParticleSystem liquidParticle;
    [SerializeField] Holder itemToFill;
    [SerializeField] float amountToAdd;
    [SerializeField] Transform spellPoint;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] float pumpPressedPositionY;
    [SerializeField] float pumpOriginalPosition;
    [SerializeField] Collider objectCollider;
    // Update is called once per frame
    private void Start()
    {
        
    }
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
        if(!called)
        objectCollider.enabled = true;
    }
    bool called = false;
    IEnumerator PumpAnimation()
    {
        objectCollider.enabled = false;

          bottlePump.DOLocalMoveY(pumpPressedPositionY, .6f).OnComplete (() => bottlePump.DOLocalMoveY(pumpOriginalPosition, .6f).OnComplete(()=>ActivateCollider()));
        //currentAddedAmount += liquidMLPerPump;
        //SceneController.instance.shakerCountTXT.text = currentAddedAmount.ToString();
        //    SceneController.instance.AddTextAmount(liquidMLPerPump);
         liquidParticle.Play();
        itemToFill.IncreaseLiquid(amountToAdd);
        for (int i = 0; i < SceneController.instance.currentRecipe.RecipeItems.Count; i++)
        {

            if (SceneController.instance.currentRecipe.RecipeItems[i].itemType == itemType)
            {
                SceneController.instance.currentRecipe.RecipeItems[i].numberOfItemsRequired -= 1;
                if (SceneController.instance.currentRecipe.RecipeItems[i].numberOfItemsRequired <= 0)
                {
                  //  GetComponent<Collider>().enabled = false;
                    SceneController.instance.currentAddedAmount = 0;
                    SceneController.instance.InvokeCurrentStep();
                    called = true;
                    //  UIManager.instance.pumpButton.SetActive(false);
                    yield return new WaitForSeconds(.7f);

                    SceneController.instance.ResetShakerLiquidUI();

                    //   Invoke(nameof(ReturnObjectToOriginalTransform),.5f);
                }
            }
        }
        //yield return new WaitForSeconds(1.4f);
        //objectCollider.enabled = true;

        //  yield return new WaitForSeconds(.5f);

        //   SceneController.instance.shakerCountTXT.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
            handOnItem = true;
        }
        //if (other.gameObject.tag=="Shaker")
        //{
        //    shakerInPlace = true;
        //}
        if(handOnItem&&shakerInPlace)
        {
            PlayPumpAnimation();
            SceneController.instance.SetShakerLiquidAmount(itemName, liquidMLFullAmount, liquidMLPerPump);
          //  other.GetComponent<XRController>().SendHapticImpulse(.5f, .5f);
            // UIManager.instance.ActivatePump(this);
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
        if (!handOnItem || !shakerInPlace)
        {
        //    UIManager.instance.pumpButton.SetActive(false);
           // UIManager.instance.ActivatePump(this);
        }
    }

}
