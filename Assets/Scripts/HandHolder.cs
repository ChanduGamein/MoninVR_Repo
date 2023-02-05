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
    public Transform shakerLidPosition;
    public Transform spoonPosition;
    public Transform jiggerPosition;
    public Transform waterBottlePosition;
    public Transform bottleOpenerPosition;
    public Transform strainerPosition;
    public Transform tikkiPosition;
    public Transform cupPosition;
    public Transform coupePosition;
    public Collider handCollider;
    public HandType handType;
    public bool grabbing;
    public Animator animator;
    public Tweezers tweezers;
    public bool hasGrarnish;
    public bool triggerGarnish;
    Garnish garnish;
    public bool hasTweezer;
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
    public void OnclickGrab()
    {
        if (triggerGarnish)
        {
            triggerGarnish = false;
            hasTweezer = true;
            hasGrarnish = true;
            tweezers.gameObject.SetActive(true);
            tweezers.grabed = true;
            animator.SetTrigger("Tweezer");
            tweezers.SpawnGarnish(garnish.garnish);
            grabbing = true;
        }
    }
    public void Ungrab()
    {
        if(hasTweezer)
        {
            hasTweezer = false;
            hasGrarnish = false;

            tweezers.gameObject.SetActive(false);
            tweezers.grabed = false;
        //    tweezers.SpawnGarnish(garnish.garnish);
          //  grabbing = true;
        }
        animator.SetTrigger("Idle");
        //currentHolder.UnGrab();
        
        //grabbing = false;

        //UIManager.instance.canGrab = false;
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag=="Garnish")
        {
            
            triggerGarnish = true;
            garnish = other.GetComponent<Garnish>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Garnish")
        {
            triggerGarnish = false;
        }
    }
}
