using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Holder : MonoBehaviour
{
    public Transform liquid;

    public bool grabed;
    Vector3 originalPosition;
    Quaternion originalRotation;
    public items itemType;
    //[SerializeField] Colider parentColider;
    [SerializeField]Transform originalParent;
    public HandHolder hand;
    // Start is called before the first frame update
    public Rigidbody _rb;
    public void IncreaseLiquidScale(float addedAmount)
    {
        liquid.transform.localScale =new Vector3 (1, liquid.transform.localScale.y+ addedAmount, 1);
            
    }
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        originalPosition = transform.position;
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
        _rb.isKinematic = true;
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
    public void UnGrab()
    {
        _rb.isKinematic = true;
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
     //   StartCoroutine(ReturnBottle());
    }

}
