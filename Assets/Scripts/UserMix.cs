using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserMix : MonoBehaviour
{
    public List<items> userMixOfItems = new List<items>();

    public void AddItemToMix(items item)
    {
        userMixOfItems.Add(item);
    }
}
