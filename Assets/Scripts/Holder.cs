using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using LiquidVolumeFX;
using UnityEngine.XR.Interaction.Toolkit;

public class Holder : MonoBehaviour
{
    [HideInInspector]
    public bool grabed;
    Vector3 originalPosition;
    Quaternion originalRotation;
    public items itemType;
    //[SerializeField] Colider parentColider;
    [SerializeField]Transform originalParent;
    
    public HandHolder hand;
    // Start is called before the first frame update
    public LiquidVolume liquidVolume;
    [HideInInspector]
    public bool picked;
    [HideInInspector]
    public bool callTutoral;
    public bool haveLiquid;
    public ParticleSystem liquidParticle;
    public Transform poringRight, poringLeft;
    public virtual void IncreaseLiquid(float value)
    {
        liquidVolume.level += value;
        if(hand!=null &&grabed)
        hand.GetComponent<XRController>().SendHapticImpulse(.5f, .5f);
    }
    public void DecreaseLiquid(float value)
    {
        if (liquidVolume.level > 0)
            liquidVolume.level -= value;
    }
    //public void IncreaseLiquidScale(float addedAmount)
    //{
    //    liquid.transform.localScale =new Vector3 (1, liquid.transform.localScale.y+ addedAmount, 1);
            
    //}
    public virtual void Grab()
    {
    }
    void Start()
    {
       // _rb = GetComponent<Rigidbody>();
        originalPosition = transform.localPosition;
        originalRotation = transform.rotation;
        if(transform.parent!=null)
        originalParent = transform.parent.transform;
        else
        {
            originalParent = null;
        }
    }
    IEnumerator ReturnBottle()
    {
        yield return new WaitForSeconds(0);
      //  _rb.isKinematic = true;
         hand.handCollider.enabled = true;
        hand.grabbing = false;
        //  hand.transform.rotation= new Quaternion(0,0,0,0);
        if (originalParent != null)
        {
            transform.parent = originalParent;
        }
        else
        {
            transform.parent = null;
        }
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        grabed = false;
        UIManager.instance.canGrab = false;
        hand.handCollider.enabled = true;

        //  _rb.isKinematic = false;
    }
    public virtual void UnGrab()
    {
      //  _rb.isKinematic = true;
        hand.handCollider.enabled = true;
        hand.grabbing = false;
        //  hand.transform.rotation= new Quaternion(0,0,0,0);
        if (originalParent != null)
        {
            transform.parent = originalParent;
        }
        else
        {
            transform.parent = null;
        }
        transform.localPosition = originalPosition;
        transform.rotation = originalRotation;
        grabed = false;
        UIManager.instance.canGrab = false;
        hand.handCollider.enabled = true;
       // hand = null;

        //   StartCoroutine(ReturnBottle());
    }

}
