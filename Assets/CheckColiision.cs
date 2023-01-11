using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckColiision : MonoBehaviour
{

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Scoop")
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Scoop")
        {
            gameObject.SetActive(false);
        }
    }
}
