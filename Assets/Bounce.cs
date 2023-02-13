using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{   
    private Vector3 scale;
    void Start()
    {
        scale = this.gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.localScale=(1+Mathf.Sin(Time.time)*0.2f)*scale;
    }
}
