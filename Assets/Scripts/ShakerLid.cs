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
        UIManager.instance.shakeButton.SetActive(false);

        transform.parent = shaker.transform;
        transform.DOMove(shakerClosePosition.position,1);
        transform.DORotate(shakerClosePosition.rotation.eulerAngles, 1).OnComplete(() =>
        shaker.transform.DOShakePosition(3, 1).OnComplete(() => FinishShake()));
    }
    public void FinishShake()
    {

        transform.parent = transform.parent.parent;
        shaker.GetComponent<Rigidbody>().isKinematic = false;

        UnGrab();
        SceneController.instance.InvokeCurrentStep();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "end")
        {
            UnGrab();
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
            //hand = other.GetComponent<HandHolder>();

            //UIManager.instance.ActivateGrab(hand.shakerPositon, hand,this.transform);
        }
        if (other.gameObject.tag == "Shaker")
        {
            Shake();
            // UIManager.instance.shakeButton.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
           // UIManager.instance.grabButton.SetActive(false);
        }
    }

}
