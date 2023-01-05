using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class IceScoop : Holder
{
    [SerializeField] List<Rigidbody> _rbs = new List<Rigidbody>();
    [SerializeField] Transform placeICeTarget;
    
    bool pickedIce;

    public void ActivatePhysicsOnCubes()
    {
        //transform.GetChild(0).parent = null;
        foreach (Rigidbody item in _rbs)
        {
            item.gameObject.SetActive(false);
        }
        SceneController.instance.InvokeCurrentStep();


    }
    public void PickUpIce()
    {
        Debug.Log("pickupIce");
      //  transform.DORotate(new Vector3(21.701f, 0,0), 1).OnComplete(()=>
      //  transform.DORotate(new Vector3(0, 0, 0), 1));
     //   UIManager.instance.PickUpIceButton.SetActive(false);

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            //child is your child transform
        }
        pickedIce = true;
      //  AudioManagerMain.instance.PlaySFX("iceBucketScoop");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider");
        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
            hand = other.gameObject.GetComponent<HandHolder>();
          //  UIManager.instance.grabButton.SetActive(true);
            UIManager.instance.ActivateGrab(hand.scoopPositon, hand, this.transform);
            UIManager.instance.canGrab = true;

        }

        if (other.gameObject.tag == "Shaker")
        {
            if (pickedIce == true)
            {
                //    transform.DOMove(placeICeTarget.position, .5f);
                //  transform.DORotate(placeICeTarget.rotation.eulerAngles, .5f).OnComplete(()=>
                ActivatePhysicsOnCubes();

            }
        }
        if (other.gameObject.tag == "IceBox")
        {
            // UIManager.instance.PickUpIceButton.SetActive(true);
            PickUpIce();
        }
        if(other.tag=="end")
        {
            UnGrab();
        }
    }
    private void OnCollisionEnter(Collision other)
    {

    }
    private void OnCollisionExit(Collision other)
    {
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
            UIManager.instance.canGrab = true;

        }
    }
}
