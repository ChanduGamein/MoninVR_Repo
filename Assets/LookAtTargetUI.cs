using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTargetUI : MonoBehaviour
{
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void PointAtTarget(Transform _targtet)
    {
        target = _targtet;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        transform.LookAt(transform.position - dirToTarget, Vector3.up);
        transform.position = new Vector3(target.position.x, target.position.y + .25f, target.position.z);
    }

}
