using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tile
{
    public string color;
    public string type;
    public string sprite;
    public bool isSolid;
    public int heat;
}

[System.Serializable]
public class TileDataContainer
{
  public Tile[] tiles;
}