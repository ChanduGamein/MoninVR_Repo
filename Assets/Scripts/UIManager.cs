using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] GameObject activePrepareBtn, inActivePrepareBtn;
    [SerializeField] Image inGameRecipeImage;
    [SerializeField] Text inGameRecipeName;
    [SerializeField] Text tutorialTxt;
    [SerializeField] Image tutorialPickUpImage;
    [SerializeField] Text tutorialPickUpTxt;
    [SerializeField] RecipeStepUI recipeStepUI;
    [SerializeField] Transform stepsParent;
    public GameObject tutorialStepsPanel;
    public int drinkId;

    #region testing
    public GameObject grabButton;
    public GameObject pumpButton;
    public GameObject PickUpIceButton;
    public GameObject shakeButton;
    public GameObject pourButton;
    public GameObject pourSparklinButton;
    public Transform handTransform;
    public Transform itemToGrab;
    Pump pump;
    public HandHolder handHolder;
    public bool canGrab;
    #endregion
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;

    }
    public void OnClickStartPrepare()
    {
        SceneController.instance.ChooseRecipe(drinkId);
    }
    #region SideSteps
    List<RecipeStepUI> currentRecipeSteps = new List<RecipeStepUI>();
    int currentStepIndex=0;
    public void SetStepsUI(int numOfSteps)
    {
        for (int i = 0; i < numOfSteps; i++)
        {
            RecipeStepUI stepUI = Instantiate(recipeStepUI, stepsParent);
            stepUI.stepText.text = "Step#" + (i+1).ToString();
            currentRecipeSteps.Add(stepUI);
        }
        SetCurrentRecipce();
    }
    public void SetCurrentRecipce()
    {
        currentRecipeSteps[currentStepIndex].SetCurrentStep();
      // index+=1;
    }
    public void SetCurrentStepCompleted()
    {
        currentRecipeSteps[currentStepIndex].SetStepCompleted();
        if (currentStepIndex < currentRecipeSteps.Count-1)
        {
            currentStepIndex += 1;
            SetCurrentRecipce();
        }

    }
    #endregion
    #region Turorial
    public void SetTutorialText(string instruction)
    {
        tutorialStepsPanel.SetActive(true);
        tutorialTxt.text = instruction;
    }
    public void SetTutorialPickUpItem(Sprite itemSprite,string instruction)
    {
        tutorialPickUpImage.sprite = itemSprite;
        tutorialPickUpTxt.text = instruction;
    }
    public void SetInGameRecipeImage(Sprite recipeSprite,string recipeName)
    {
        inGameRecipeImage.sprite = recipeSprite;
        inGameRecipeName.text = recipeName;
    }
    #endregion
    public void ActivatePrepareBtn()
    {
        activePrepareBtn.SetActive(true);
        inActivePrepareBtn.SetActive(false);
    }
    public void DeActivatePrepareBtn()
    {
        activePrepareBtn.SetActive(false);
        inActivePrepareBtn.SetActive(true);
    }
    string triggerName;
    public void ActivateGrab(Transform _handTransform, HandHolder _handHolder, Transform _itemToGrab,string _triggerName)
    {
        handTransform = _handTransform;
        itemToGrab = _itemToGrab;
        handHolder = _handHolder;
        triggerName = _triggerName;
    }
    public void ResetGrabbedItems()
    {

    }

    bool called = false;
    public void OnClickGrab(bool isLeft)
    {
        if (handHolder != null)
        {
            if (handHolder.handType == HandType.right && !isLeft)
            {
                if(!handHolder.hasGrarnish)
                if (canGrab)
                {
                    Grab();
                }
            }
            if (handHolder.handType == HandType.left && isLeft)
            {
                if (!handHolder.hasGrarnish)
                if (canGrab)
                {
                    Grab();
                }
            }
        }
    }
    public void Grab()
    {
        itemToGrab.parent = handTransform;
        itemToGrab.localPosition = Vector3.zero;
        itemToGrab.localRotation = Quaternion.identity;
        itemToGrab.GetComponent<Holder>().hand = handHolder;
        itemToGrab.GetComponent<Holder>().grabed = true;
        itemToGrab.GetComponent<Holder>().Grab ();
        //    handHolder.handCollider.enabled = false;
        handHolder.grabbing = true;
        handHolder.SetAnimatorTigger(triggerName);
        handHolder.currentHolder = itemToGrab.GetComponent<Holder>();
    }
}