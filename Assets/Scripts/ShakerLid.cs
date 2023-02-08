using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ShakerLid : Holder
{
    [SerializeField] Transform shakerClosePosition;
    [SerializeField] Transform shaker;
    public void SetShaker(Transform _shaker)
    {
        shaker = _shaker;
    }
    public void Shake()
    {
        if (shaker.GetComponent<Holder>().grabed)
        {
            shaker.GetComponent<Rigidbody>().isKinematic = true;
            UnGrab();
            shaker.GetComponent<Shaker>().Shake();
            GetComponent<BoxCollider>().enabled = false;
            gameObject.SetActive(false);
        }
    }
    public void FinishShake()
    {
       shaker.GetComponent<Shaker>().dummyLid.SetActive(false);
        gameObject.SetActive(true);

        AudioManagerMain.instance.PlaySFX("shakerMixerOpen");
      //  transform.parent = transform.parent.parent;
        // shaker.GetComponent<Rigidbody>().isKinematic = false;
        SceneController.instance.InvokeCurrentStep();

        UnGrab();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "end")
        {
            UnGrab();
        }
        if(!grabed)
        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
            hand = other.GetComponent<HandHolder>();
            //   UIManager.instance.grabButton.SetActive(true);
            UIManager.instance.canGrab = true;
            UIManager.instance.ActivateGrab(hand.shakerPositon, hand, this.transform,"Shaker");
        }
        if (other.gameObject.tag == "Shaker")
        {
            Shake();
            // UIManager.instance.shakeButton.SetActive(true);
        }
    }
    private void OnCollisionEnter(Collision other)
    {


    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
         //   UIManager.instance.grabButton.SetActive(false);
            UIManager.instance.canGrab = true;

        }
    }

}
