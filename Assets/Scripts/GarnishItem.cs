using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarnishItem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] items itemType;
    public Tweezers tweezers;


    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position, -transform.up, Color.green);
        //if (Physics.Raycast(transform.position, -transform.up, out hit, 20, layer))
        //{
        //    Debug.Log("hiiiit   " + hit.transform.gameObject.name);
        //    Debug.Log("hit");
        //    transform.parent = hit.transform;
        //    hand.handCollider.enabled = true;
        //    hand.hasGarnish = false;
        //    gameObject.SetActive(false);
        //    SceneController.instance.glassDrink.garnish.SetActive(true);
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Cup"|| other.gameObject.tag == "LongGlass")
        {
            HolderGlass glass = other.GetComponent<HolderGlass>();
            int rnd = Random.Range(0, glass.garnishPositions.Count);
            glass.SetGarnishTransform(this.transform);
            tweezers.hasGarnish = false;
            tweezers.hand.hasGrarnish = false;
            tweezers.gameObject.SetActive(false);
            tweezers._garnish = null;

                if (SceneController.instance.currentRecipe.RecipeItems[SceneController.instance.recipeStepIndex].itemType == itemType)
                {
                    Debug.Log("nammme "+SceneController.instance.currentRecipe.RecipeItems[SceneController.instance.recipeStepIndex].itemType+"    "+ itemType);
                    SceneController.instance.currentRecipe.RecipeItems[SceneController.instance.recipeStepIndex].numberOfItemsRequired -= 1;
                    if (SceneController.instance.currentRecipe.RecipeItems[SceneController.instance.recipeStepIndex].numberOfItemsRequired <= 0)
                    {
                        SceneController.instance.InvokeCurrentStep();
                        Debug.Log("invoke garnish");
                    }
                }
            }
        
    }

}
