using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obscurable : MonoBehaviour
{
    public GameObject fog;
    public void Clear()
    {
        if(fog) {
            Destroy(fog);
            fog = null;
        }
    }
}
