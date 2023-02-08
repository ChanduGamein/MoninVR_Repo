using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public enum items { IceCubes, LemonFlavor,LemonBase, Water,shakerLid,sparklingWater,garnich,pourIntoGlass,Shaker,LongGlass,LemonBase2,Jigger }

 [System.Serializable]
 public class Item
 {
    public items itemType;
    public int numberOfItemsRequired=1;
    public bool itemCollected;
    public UnityEvent myEvent;
    public Sprite itemSprite;
    public string itemName;
    public string actionName;
    public string quantity;
    public string instruction;
    public bool isQuantity;

    
    public void NeXtStep()
    {
        SceneController.instance.recipeStepIndex += 1;
     //   UIManager.instance.SetTutorialText(myText);
        UIManager.instance.SetStepNumber("Step#" + (SceneController.instance.recipeStepIndex+1).ToString());
        UIManager.instance.SetTutorial(itemSprite, itemName, actionName, quantity, instruction,isQuantity);

    }

}

[System.Serializable]
public class Recipe
{
    public List<Item> RecipeItems = new List<Item>();
    public int numOfSteps;
    public List<GameObject> itemsToUseinRecipe = new List<GameObject>();
    public Holder firstItem;

    public Sprite itemSprite;
    public string itemName;
    public string actionName;
    public string quantity;
    public string instruction;
    public bool isQuantity;

}

public class SceneController : MonoBehaviour
{
    public List<Recipe> userSelectedRecipe = new List<Recipe>();
    public static SceneController instance;
    public Recipe currentRecipe;
    public int currentAddedAmount = 0;
    public HandHolder handHolderLeft, handHolderRigh;
    public int recipeIndex;
    public int recipeStepIndex = 0;
    public FillLiquidUI fillLiquidUI;
    public FillLiquidUI fillLiquidStatic;
    public GlassDrink glassDrink;
    public bool isFridgeOpen;
    public bool firstShot = true;
    [SerializeField] ParticleSystem confetti;
    private void Awake()
    {
        instance = this;
      //  currentRecipe = userSelectedRecipe[1];
    }
    private void Start()
    {

    }
    public bool setLiquidAmount;
    public void SetLiquidCanvasParent(Transform _parent)
    {
        fillLiquidUI.transform.parent = _parent;
        fillLiquidUI.transform.localRotation = Quaternion.identity;
        fillLiquidUI.transform.localPosition = Vector3.zero;
        fillLiquidUI.transform.localScale = Vector3.one;
    }
    public void SetShakerLiquidAmount(string drinkName, float fullAmount, float addedAmount)
    {
        if (!setLiquidAmount)
        {
          //  fillLiquidUI.gameObject.SetActive(true);
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
        currentRecipe = userSelectedRecipe[id];
        for (int i = 0; i < userSelectedRecipe[id].itemsToUseinRecipe.Count; i++)
        {
            userSelectedRecipe[id].itemsToUseinRecipe[i].SetActive(true);
        }
      //  UIManager.instance.SetStepsUI(currentRecipe.numOfSteps);
        userSelectedRecipe[id].firstItem.callTutoral = true;
        //  InvokeCurrentStep();
        //  UIManager.instance.SetTutorialText("Pick Up Shaker");
        //   UIManager.instance.SetTutorialText(userSelectedRecipe[id].myText);
        UIManager.instance.SetTutorial(userSelectedRecipe[id].itemSprite, userSelectedRecipe[id].itemName, userSelectedRecipe[id].actionName,
            userSelectedRecipe[id].quantity, userSelectedRecipe[id].instruction, userSelectedRecipe[id].isQuantity);

    }
    public void InvokeCurrentStep()
    {
        if (!(recipeStepIndex > userSelectedRecipe[recipeIndex].RecipeItems.Count - 1))
        {
            userSelectedRecipe[recipeIndex].RecipeItems[recipeStepIndex].myEvent.Invoke();
            userSelectedRecipe[recipeIndex].RecipeItems[recipeStepIndex].NeXtStep();
          //  UIManager.instance.SetCurrentStepCompleted();
        }
        int lastIndex = recipeStepIndex + 1;
        if((lastIndex)> userSelectedRecipe[recipeIndex].RecipeItems.Count)
        {
            UIManager.instance.DisplayWellDone();
            AudioManagerMain.instance.PlaySFX("WellDone");
            confetti.gameObject.SetActive(true);
            confetti.Play();
            Invoke("Reload",7);
        }
    }
    public void Reload()
    {

        SceneManager.LoadSceneAsync(0);
    }
    public void OnClickUnGrabLeft()
    {
        if (handHolderLeft.currentHolder != null)
        {
            if (handHolderLeft.currentHolder.TryGetComponent<Shaker>(out Shaker _shaker))
            {
                if (!(_shaker.shaking))
                {
                    handHolderLeft.currentHolder.UnGrab();
                    handHolderLeft.currentHolder = null;
                    UIManager.instance.itemToGrabLeft = null;
                }
            }
            else
            {
                handHolderLeft.currentHolder.UnGrab();
                handHolderLeft.currentHolder = null;
                UIManager.instance.itemToGrabLeft = null;

            }
        }

    } 
    public void OnClickUnGrabRight()
    {
        if (handHolderRigh.currentHolder != null)
        {
            if (handHolderRigh.currentHolder.TryGetComponent<Shaker>(out Shaker _shaker))
            {
              if(!(_shaker.shaking))
                {
                    handHolderRigh.currentHolder.UnGrab();
                    handHolderRigh.currentHolder = null;
                    UIManager.instance.itemToGrabRight = null;
                }
            }
            else
            {
                handHolderRigh.currentHolder.UnGrab();
                handHolderRigh.currentHolder = null;
                UIManager.instance.itemToGrabRight = null;
            }

        }
    }

}

