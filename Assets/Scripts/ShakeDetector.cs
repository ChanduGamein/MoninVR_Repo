using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeDetector : MonoBehaviour
{
    public bool Detectshake,detected;
    public float inity;
    Shaker shaker;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Detectshake)
        {
            Detectshake = false;
            inity = this.transform.localPosition.y;

        }
        if (Mathf.Abs(inity - this.transform.localPosition.y) > 0.06f && inity != 0&&shaker.grabed)
        {
            detected = true;
            AudioManagerMain.instance.PlaySFX("shakerSound");
            shaker.ShakerAnimation();
            inity = 0;
        }


    }
    public void SetShaker(Shaker _shaker)
    {
        shaker = _shaker;
    }
}
