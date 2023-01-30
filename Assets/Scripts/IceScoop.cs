using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.XR.Interaction.Toolkit;

public class IceScoop : Holder
{
    [SerializeField] Transform placeICeTarget;
    [SerializeField] Transform placeICeTargetSmall;
    [SerializeField] Shaker shaker;
    [SerializeField] LongGlass longGlass;
    [SerializeField] PooledObjects iceCube;
    bool pickedIce;
    bool called;

    public void SetShaker(Shaker _shaker)
    {
        shaker = _shaker;
    }
    public void ActivatePhysicsOnCubesShaker()
    {
        //transform.GetChild(0).parent = null;
        AudioManagerMain.instance.PlaySFX("IceIntoGlass");

        placeICeTarget.gameObject.SetActive(false);

        //if (!called)
        //{
        //    SceneController.instance.InvokeCurrentStep();
        //    called = true;
        //}
        pickedIce = false;
        //if(shaker.iceCubes.Count>=4)
        //for (int i = 0; i < 4; i++)
        //{
        //        shaker.hand.GetComponent<XRController>().SendHapticImpulse(.5f,.5f);
        //    shaker.iceCubes[0].SetActive(true);
        //    shaker.iceCubes.RemoveAt(0);
        //}
    }
    public void ActivatePhysicsOnCubesLongGlass()
    {
        //transform.GetChild(0).parent = null;
        AudioManagerMain.instance.PlaySFX("IceIntoGlass");

      //  placeICeTarget.gameObject.SetActive(false);
        //placeICeTargetSmall.gameObject.SetActive(false);

        //if (!called)
        //{
        //    SceneController.instance.InvokeCurrentStep();
        //    called = true;
        //}
        //pickedIce = false;
        //if(longGlass.iceCubes.Count>=3)
        //for (int i = 0; i < 3; i++)
        //{
        // //   longGlass.hand.GetComponent<XRController>().SendHapticImpulse(.5f,.5f);
        //    longGlass.iceCubes[0].SetActive(true);
        //    longGlass.iceCubes.RemoveAt(0);
        //}
    }
    public override void UnGrab()
    {
        base.UnGrab();
        placeICeTarget.gameObject.SetActive(false);
        pickedIce = false;


    }
    public void PickUpIce()
    {
      //  placeICeTarget.gameObject.SetActive(true);
        hand.GetComponent<XRController>().SendHapticImpulse(.5f, .5f);

        GetComponent<SpawnerManager>().Spawn();
        AudioManagerMain.instance.PlaySFX("iceBucketScoop");

        pickedIce = true;
    }
    public void PickUpIceSmall()
    {
        placeICeTargetSmall.gameObject.SetActive(true);
        hand.GetComponent<XRController>().SendHapticImpulse(.5f, .5f);


        AudioManagerMain.instance.PlaySFX("iceBucketScoop");

        pickedIce = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider");
        if(!grabed)
        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
            hand = other.gameObject.GetComponent<HandHolder>();
          //  UIManager.instance.grabButton.SetActive(true);
            UIManager.instance.ActivateGrab(hand.scoopPositon, hand, this.transform, "IceScoop");
            UIManager.instance.canGrab = true;

        }

        if (other.gameObject.tag == "Shaker")
        {
            if (pickedIce == true)
            {

                ActivatePhysicsOnCubesShaker ();

            }
        }
        if (other.gameObject.tag == "LongGlass")
        {
            if (pickedIce == true)
            {

                ActivatePhysicsOnCubesLongGlass ();

            }
        }
        if (other.gameObject.tag == "IceBox")
        {
            // UIManager.instance.PickUpIceButton.SetActive(true);
            PickUpIce();
        }
        //if (other.gameObject.tag == "IceBoxSmall")
        //{
        //    // UIManager.instance.PickUpIceButton.SetActive(true);
        //    PickUpIceSmall();
        //}
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
