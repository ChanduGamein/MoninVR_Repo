using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonChecker : MonoBehaviour
{
    public bool OnObject;
  

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Scoop"))
        {
            Debug.Log(collision.gameObject.name);
            OnObject = true;
        }
    }
  
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Scoop"))
        {
            OnObject = false;
            this.gameObject.transform.parent = null;
        }
       
    }
}
