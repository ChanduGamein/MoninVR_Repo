using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarnishItem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] LayerMask layer;
    RaycastHit hit;
    Rigidbody _rb;
    public HandHolder hand;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, -transform.up, Color.green);
        if (Physics.Raycast(transform.position, -transform.up, out hit, 20, layer))
        {
            Debug.Log("hiiiit   " + hit.transform.gameObject.name);
            Debug.Log("hit");
            transform.parent = hit.transform;
            hand.handCollider.enabled = true;
            hand.hasGarnish = false;
            gameObject.SetActive(false);
            SceneController.instance.glassDrink.garnish.SetActive(true);
        }
    }
    public void LeaveHAnd()
    {
       // _rb.isKinematic = false;

    }
}
