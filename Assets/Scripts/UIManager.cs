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
    public void ActivateGrab(Transform _handTransform, HandHolder _handHolder, Transform _itemToGrab)
    {
        handTransform = _handTransform;
        itemToGrab = _itemToGrab;
        handHolder = _handHolder;
    }
    public void ResetGrabbedItems()
    {

    }
    //public void ActivatePump(Pump _pump)
    //{
    //    pumpButton.SetActive(true);
    //    pump = _pump;
    //}

    //public void OnClickPump()
    //{
    //    pump.PlayPumpAnimation();
    //}
    bool called = false;
    public void OnClickGrab(bool isLeft)
    {
        if (handHolder != null)
        {
            if (handHolder.handType == HandType.right && !isLeft)
            {
                if(!handHolder.hasGarnish)
                if (canGrab)
                {
                    Grab();
                }
            }
            if (handHolder.handType == HandType.left && isLeft)
            {
                if (!handHolder.hasGarnish)

                    if (canGrab)
                {
                    Grab();
                }
            }
        }
    }
    public void Grab()
    {
        if (itemToGrab.TryGetComponent(out Shaker _shaker) && !called)
        {
            called = true;
            SetTutorialText("Add Base MONIN Cloudy Lemonade Concentrate");
        }
        Debug.Log("clicked");
        itemToGrab.parent = handTransform;
        itemToGrab.localPosition = Vector3.zero;
        itemToGrab.GetComponent<Holder>().hand = handHolder;
        itemToGrab.GetComponent<Holder>().grabed = true;
        //    handHolder.handCollider.enabled = false;
        handHolder.grabbing = true;
        handHolder.currentHolder = itemToGrab.GetComponent<Holder>();
        // grabButton.SetActive(false);
    }
}