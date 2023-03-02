using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTargetUI : MonoBehaviour
{
    public static LookAtTargetUI instance;

    public Transform target;
    [SerializeField] GameObject arrow;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    public void DeactivateArrow()
    {
       // arrow.gameObject.SetActive(false);
    }
    public void PointAtTarget(Transform _targtet)
    {
       // arrow.SetActive(true);
       // target = _targtet;
       // Vector3 dirToTarget = (target.position - transform.position).normalized;
       //// transform.LookAt(transform.position - dirToTarget, Vector3.up);
       // transform.position = new Vector3(target.position.x, target.position.y + .2f, target.position.z);
    }

}
 