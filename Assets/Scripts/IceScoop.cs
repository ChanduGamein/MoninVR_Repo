using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class IceScoop : Holder
{
    [SerializeField] List<Rigidbody> _rbs = new List<Rigidbody>();
    [SerializeField] Transform placeICeTarget;
    [SerializeField] Shaker shaker;
    [SerializeField] PooledObjects iceCube;
    bool pickedIce;
    bool called;
    public void ActivatePhysicsOnCubes()
    {
        //transform.GetChild(0).parent = null;
        AudioManagerMain.instance.PlaySFX("IceIntoGlass");
        //foreach (Rigidbody item in _rbs)
        //{
        //    item.gameObject.SetActive(false);
        //}
        if (!called)
        {
            SceneController.instance.InvokeCurrentStep();
            called = true;
        }
        pickedIce = false;
        if(shaker.iceCubes.Count>=3)
        for (int i = 0; i < 3; i++)
        {
            shaker.iceCubes[0].SetActive(true);
            shaker.iceCubes.RemoveAt(0);
        }
    }
    public void PickUpIce()
    {
        Debug.Log("pickupIce");
        for (int i = 0; i < 4; i++)
        {
            if(iceCube.GetPooledObject(placeICeTarget.transform.position))
            {

            }
        }
        AudioManagerMain.instance.PlaySFX("iceBucketScoop");
        //foreach (Rigidbody item in _rbs)
        //{
        //    item.gameObject.SetActive(true);
        //}
        pickedIce = true;
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
            UIManager.instance.canGrab = false;

        }
    }
}
