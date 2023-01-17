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
    public Transform smallBottlePosition;
    public Collider handCollider;
    public HandType handType;
    public bool grabbing;
    public Animator animator;
    public Tweezers tweezers;
    public bool hasGrarnish;
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

        if (other.gameObject.tag=="Garnish")
        {
            Debug.Log("garnishhhhh");
            hasGrarnish = true;
            tweezers.gameObject.SetActive(true);
            tweezers.grabed = true;
            tweezers.SpawnGarnish(other.GetComponent<Garnish>().garnish);
        }
    }
}
