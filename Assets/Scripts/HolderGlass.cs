using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderGlass : Holder
{
    public List<Transform> garnishPositions = new List<Transform>();
    int counter = 0;
    public virtual void SetGarnishTransform(Transform garnish)
    {
        if (counter < garnishPositions.Count)
        {
            garnish.parent = transform;
            garnish.position = garnishPositions[counter].position;
            garnish.rotation = garnishPositions[counter].rotation;
            garnish.localScale = garnishPositions[counter].localScale;
            counter++;
        }
    }
}
