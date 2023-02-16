using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using LiquidVolumeFX;
using UnityEngine.XR.Interaction.Toolkit;

public class Holder : MonoBehaviour
{
   
    public bool grabed;
    Vector3 originalPosition;
    Quaternion originalRotation;
    public items itemType;
    //[SerializeField] Colider parentColider;
    [SerializeField]Transform originalParent;
    
    public HandHolder hand;
    // Start is called before the first frame update
    public LiquidVolume liquidVolume;
    
    public bool picked;
    [HideInInspector]
    public bool callTutoral;
    public bool haveLiquid;
    public ParticleSystem liquidParticle;
    public Transform poringRight, poringLeft;
    [SerializeField] BoxCollider boxCollider;
    public bool isPointer;
    protected Outline outline;
    [SerializeField] Color color;
    public void PointAtItem()
    {
        if(outline==null)
        {
            CreateOutline();
        }
        isPointer = true;
        outline.enabled = true;
    }
    public void CreateOutline()
    {
        outline = gameObject.AddComponent<Outline>();
        outline.enabled = false;
        outline.OutlineWidth = 6;
        if (ColorUtility.TryParseHtmlString("#FFCD0D", out color))
            outline.OutlineColor = color;
    }
    //public void ActivateItem()
    //{
    //   // boxCollider.enabled = true;
    //    LookAtTargetUI.instance.PointAtTarget(this.transform);
    //}
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

        DeactivateOutline();
        //  LookAtTargetUI.instance.DeactivateArrow();
    }
    public void DeactivateOutline()
    {
        if (isPointer)
        {
            isPointer = false;
            outline.enabled = false;

        }
    }
    private void Awake()
    {
        CreateOutline();
    }
    protected virtual void Start()
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
