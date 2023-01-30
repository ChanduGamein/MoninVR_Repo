using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleCap : MonoBehaviour
{
    [SerializeField] SmallBottle smallBottle;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Opener")
        {
            smallBottle.isCapRemoved = true;
            gameObject.SetActive(false);
        }
    }
}
