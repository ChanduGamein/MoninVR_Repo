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
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetInGameRecipeImage(Sprite recipeSprite,string recipeName)
    {
        inGameRecipeImage.sprite = recipeSprite;
        inGameRecipeName.text = recipeName;
    }

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
    //public void ActivateGrab(Transform _handTransform,HandHolder _handHolder,Transform _itemToGrab)
    //{
    //    handTransform = _handTransform;
    //    itemToGrab = _itemToGrab;
    //    handHolder = _handHolder;
    //}
    //public void ActivatePump(Pump _pump)
    //{
    //    pumpButton.SetActive(true);
    //    pump = _pump;
    //}
    
    //public void OnClickPump()
    //{
    //    pump.PlayPumpAnimation();
    //}
    //public void OnClickGrab()
    //{
    //    itemToGrab.parent = handTransform;
    //    itemToGrab.localPosition = Vector3.zero;
    //    itemToGrab.GetComponent<Holder>().hand = handHolder;
    //    itemToGrab.GetComponent<Holder>().grabed=true ;
    //    handHolder.handCollider.enabled = false;
    //    handHolder.currentHolder = itemToGrab.GetComponent<Holder>();
    //}

}
