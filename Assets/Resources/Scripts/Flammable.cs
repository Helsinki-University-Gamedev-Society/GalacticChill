using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flammable : MonoBehaviour
{
    public float heat;
    public GameObject fire;
    public void setHeat(float heat)
    {
        this.heat = heat;
        fire.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,Mathf.Lerp(0f,1f,heat/100));
    }
}
