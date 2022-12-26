using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public enum items { IceCubes, LemonFlavor,LemonBase, Water,shakerLid,sparklingWater,garnich,pourIntoGlass }

 [System.Serializable]
 public class Item
 {
    public items itemType;
    public int numberOfItemsRequired=1;
    public bool itemCollected;
    public UnityEvent myEvent;
    public void NeXtStep()
    {
        SceneController.instance.recipeStepIndex += 1;
    }

}

[System.Serializable]
public class Recipe
{
    public List<Item> RecipeItems = new List<Item>();
}

public class SceneController : MonoBehaviour
{
    public List <Recipe> userSelectedRecipe =new List<Recipe>();
    public static SceneController instance;
    public Recipe currentRecipe;
    public TextMeshProUGUI shakerCountTXT;
    public int currentAddedAmount = 0;
    public HandHolder handHolderLeft, handHolderRigh;
    public int recipeStepIndex = 0;
    private void Awake()
    {
        instance = this;
        currentRecipe = userSelectedRecipe[0];
    }
    public void InvokeCurrentStep()
    {
        userSelectedRecipe[0].RecipeItems[recipeStepIndex].myEvent.Invoke();
        userSelectedRecipe[0].RecipeItems[recipeStepIndex].NeXtStep();
    }
    public void OnClickUnGrabLeft()
    {
        if (handHolderLeft.currentHolder != null)
            handHolderLeft.currentHolder.UnGrab();
    } 
    public void OnClickUnGrabRight()
    {
        if (handHolderRigh.currentHolder != null)
        handHolderRigh.currentHolder.UnGrab();
    }
    public void AddTextAmount(int value)
    {
        shakerCountTXT.gameObject.SetActive(true);
        currentAddedAmount += value;
        shakerCountTXT.text = currentAddedAmount.ToString();
    }
}

