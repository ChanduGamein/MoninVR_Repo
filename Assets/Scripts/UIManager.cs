using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
  //  public GameObject grabButton;
    public GameObject pumpButton;
    public GameObject PickUpIceButton;
    public GameObject shakeButton;
    public GameObject pourButton;
    public GameObject pourSparklinButton;
    public static UIManager instance;
    public Transform handTransform;
    public Transform itemToGrab;
    Pump pump;
    public HandHolder handHolder;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ActivateGrab(Transform _handTransform,HandHolder _handHolder,Transform _itemToGrab)
    {
        handTransform = _handTransform;
        itemToGrab = _itemToGrab;
        handHolder = _handHolder;
    }
    public void ActivatePump(Pump _pump)
    {
        pumpButton.SetActive(true);
        pump = _pump;
    }
    
    public void OnClickPump()
    {
        pump.PlayPumpAnimation();
    }
    public void OnClickGrab()
    {
        itemToGrab.parent = handTransform;
        itemToGrab.localPosition = Vector3.zero;
        itemToGrab.GetComponent<Holder>().hand = handHolder;
        itemToGrab.GetComponent<Holder>().grabed=true ;
        handHolder.handCollider.enabled = false;
        handHolder.currentHolder = itemToGrab.GetComponent<Holder>();
    }

}
