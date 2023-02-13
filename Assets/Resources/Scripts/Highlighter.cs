using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviour
{
    public Vector2[] tilesToHighlight;
    public List<GameObject> highlightedTiles = new List<GameObject>();

    public void Highlight()
    {
        if(tilesToHighlight==null) return;
        foreach (Vector2 tileCoor in tilesToHighlight)
        {
            GameObject sprite = SpriteFactory.CreateSprite("active_PLACEHOLDER",SpriteFactory.SpriteMaterial.Normal);
            sprite.transform.position = new Vector3(tileCoor.x,tileCoor.y,-0.59f);
            sprite.AddComponent<BoxCollider2D>();
            sprite.name=$"Highlight {tileCoor.x} {tileCoor.y}";
            highlightedTiles.Add(sprite);
        }
    }

    public void Clear()
    {
        if(highlightedTiles==null) return;
        foreach (GameObject tile in highlightedTiles)
        {
            Destroy(tile);
        }
        highlightedTiles.Clear();
    }



}
