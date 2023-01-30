using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    public Transform itemToGrabRight;
    public Transform itemToGrabLeft;
    Pump pump;
    public HandHolder handHolder;
    public GameObject pointerTutorial;
    public bool canGrab;
    #endregion
    public void Reload()
    {

        SceneManager.LoadScene(0);
    }
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
        if(handHolder.handType==HandType.left)
        {
            itemToGrabLeft = itemToGrab;
        }
        else
        {
            itemToGrabRight = itemToGrab;
        }
    }
    public void ResetGrabbedItems()
    {

    }

    bool called = false;
    public void OnClickGrab(bool isLeft)
    {
        if (handHolder != null)
        {
            if (handHolder.handType == HandType.right && !isLeft &&!handHolder.triggerGarnish)
            {
                if(!handHolder.hasGrarnish)
                //if (canGrab)
                //{
                if(itemToGrabRight!=null)
                    Grab(itemToGrabRight);
              //  }
            }
            if (handHolder.handType == HandType.left && isLeft &&!handHolder.triggerGarnish)
            {
                if (!handHolder.hasGrarnish)
                    //if (canGrab)
                    //{
                    if (itemToGrabLeft != null)

                        Grab(itemToGrabLeft);
               // }
            }
        }
    }
    public void Grab(Transform _itemToGrab)
    {
        _itemToGrab.parent = handTransform;
        _itemToGrab.localPosition = Vector3.zero;
        _itemToGrab.localRotation = Quaternion.identity;
        _itemToGrab.GetComponent<Holder>().hand = handHolder;
        _itemToGrab.GetComponent<Holder>().grabed = true;
        _itemToGrab.GetComponent<Holder>().Grab ();
        //    handHolder.handCollider.enabled = false;
        handHolder.grabbing = true;
        handHolder.SetAnimatorTigger(triggerName);
        handHolder.currentHolder = _itemToGrab.GetComponent<Holder>();
    }
}