using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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
    public Rigidbody _rb;
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
        yield return new WaitForSeconds(.3f);
        _rb.isKinematic = true;
         hand.handCollider.enabled = true;
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
      //  _rb.isKinematic = false;
    }
    public void UnGrab()
    {

        StartCoroutine(ReturnBottle());
    }

}
