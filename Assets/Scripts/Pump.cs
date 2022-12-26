using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Pump : MonoBehaviour
{
    public int liquidMLPerPump;
    public items itemType;
    public bool handOnItem;
    public bool shakerInPlace;
    [SerializeField] Transform bottlePump;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayPumpAnimation()
    {
        StartCoroutine(PumpAnimation());
    }
    IEnumerator PumpAnimation()
    {
          bottlePump.DOLocalMove(Vector3.zero,.5f).OnComplete (() => bottlePump.DOLocalMove(new Vector3(0, 5.12f, 0), .5f));
        //currentAddedAmount += liquidMLPerPump;
        //SceneController.instance.shakerCountTXT.text = currentAddedAmount.ToString();
        SceneController.instance.AddTextAmount(liquidMLPerPump);
        for (int i = 0; i < SceneController.instance.currentRecipe.RecipeItems.Count; i++)
        {

            if (SceneController.instance.currentRecipe.RecipeItems[i].itemType == itemType)
            {
                SceneController.instance.currentRecipe.RecipeItems[i].numberOfItemsRequired -= 1;
                if (SceneController.instance.currentRecipe.RecipeItems[i].numberOfItemsRequired <= 0)
                {
                    GetComponent<Collider>().enabled = false;
                    SceneController.instance.currentAddedAmount = 0;
                    UIManager.instance.pumpButton.SetActive(false);
                    SceneController.instance.InvokeCurrentStep();
                    //   Invoke(nameof(ReturnObjectToOriginalTransform),.5f);
                }
            }
        }
        yield return new WaitForSeconds(.5f);

        SceneController.instance.shakerCountTXT.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
            handOnItem = true;
        }
        if (other.gameObject.tag=="Shaker")
        {
            shakerInPlace = true;
        }
        if(handOnItem&&shakerInPlace)
        {
            PlayPumpAnimation();
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
            UIManager.instance.pumpButton.SetActive(false);
           // UIManager.instance.ActivatePump(this);
        }
    }

}
