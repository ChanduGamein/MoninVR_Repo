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
    public int numOfSteps;
}

public class SceneController : MonoBehaviour
{
    public List<Recipe> userSelectedRecipe = new List<Recipe>();
    public static SceneController instance;
    public Recipe currentRecipe;
    public TextMeshProUGUI shakerCountTXT;
    public int currentAddedAmount = 0;
    public HandHolder handHolderLeft, handHolderRigh;
    [SerializeField]int recipeIndex;
    public int recipeStepIndex = 0;
    public FillLiquidUI fillLiquidUI;
    public FillLiquidUI fillLiquidStatic;
    public GlassDrink glassDrink;

    private void Awake()
    {
        instance = this;
        currentRecipe = userSelectedRecipe[1];
    }
    private void Start()
    {
        UIManager.instance.SetStepsUI(currentRecipe.numOfSteps);
        UIManager.instance.SetTutorialText("Pick Up Shaker");
    }
    public bool setLiquidAmount;
    public void SetLiquidCanvasParent(Transform _parent)
    {
        fillLiquidUI.transform.parent = _parent;
        fillLiquidUI.transform.localRotation = Quaternion.identity;
        fillLiquidUI.transform.localPosition = Vector3.zero;
    }
    public void SetShakerLiquidAmount(string drinkName, float fullAmount, float addedAmount)
    {
        if (!setLiquidAmount)
        {
            fillLiquidUI.gameObject.SetActive(true);
            fillLiquidUI.SetAmount(drinkName, fullAmount);
            fillLiquidStatic.gameObject.SetActive(true);
            fillLiquidStatic.SetAmount(drinkName, fullAmount);
            setLiquidAmount = true;
        }
        fillLiquidUI.InCreaseAmount(addedAmount);
        fillLiquidStatic.InCreaseAmount(addedAmount);
    }
    public void ResetShakerLiquidUI()
    {
        setLiquidAmount = false;
        fillLiquidUI.gameObject.SetActive(false);
        fillLiquidUI.ResetValues();
        fillLiquidStatic.gameObject.SetActive(false);
        fillLiquidStatic.ResetValues();
    }
    //public void SetGlassLiquidAmount(string drinkName, int fullAmount, int addedAmount)
    //{
    //    if (!setLiquidAmount)
    //    {
    //        fillLiquidGlass.gameObject.SetActive(true);
    //        fillLiquidGlass.SetAmount(drinkName, fullAmount);
    //        setLiquidAmount = true;
    //    }
    //    fillLiquidGlass.InCreaseAmount(addedAmount);
    //}
    //public void ResetGlassLiquidUI()
    //{
    //    setLiquidAmount = false;
    //    fillLiquidGlass.gameObject.SetActive(false);
    //    fillLiquidGlass.ResetValues();
    //}
    public void ChooseRecipe(int id)
    {
        recipeIndex = id;
    }
    public void InvokeCurrentStep()
    {
        if (!(recipeStepIndex > userSelectedRecipe[recipeIndex].RecipeItems.Count - 1))
        {
            userSelectedRecipe[recipeIndex].RecipeItems[recipeStepIndex].myEvent.Invoke();
            userSelectedRecipe[recipeIndex].RecipeItems[recipeStepIndex].NeXtStep();
            UIManager.instance.SetCurrentStepCompleted();
        }
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

