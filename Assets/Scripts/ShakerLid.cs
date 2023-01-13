using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ShakerLid : Holder
{
    [SerializeField] Transform shakerClosePosition;
    [SerializeField] Transform shaker;
    public void Shake()
    {
        _rb.isKinematic = true;
        shaker.GetComponent<Rigidbody>().isKinematic = true;
        // UIManager.instance.shakeButton.SetActive(false);
        UnGrab();

        shaker.GetComponent<Shaker>().Shake();
        AudioManagerMain.instance.PlaySFX("shakerSound");
        GetComponent<BoxCollider>().enabled = false;
        gameObject.SetActive(false);

      //  transform.parent = shaker.transform;
       // transform.DOMove(shakerClosePosition.position,1);
       // transform.DORotate(shakerClosePosition.rotation.eulerAngles, 1).OnComplete(() =>
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
