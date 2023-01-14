using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HandType
{
    left,right
}
public class HandHolder : MonoBehaviour
{
    // Start is called before the first frame update
    public Holder currentHolder;
    public Transform scoopPositon;
    public Transform shakerPositon;
    public Transform glassPosition;
    public Collider handCollider;
    public HandType handType;
    public bool grabbing;
    public Animator animator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetAnimatorTigger(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }
    public void Ungrab()
    {
        currentHolder.UnGrab();
        grabbing = false;

        UIManager.instance.canGrab = false;
    }
    private void OnTriggerEnter(Collider other)
    {

    }
}
