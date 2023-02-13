using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class AttackHighlighter : MonoBehaviour
{
    public Vector2[] tilesToHighlight;
    public List<GameObject> highlightedTiles = new List<GameObject>();

    public void Highlight()
    {
        if(tilesToHighlight==null) return;
        foreach (Vector2 tileCoor in tilesToHighlight)
        {
            if(ResourceManager.Instance.enemies.Select(
                x=>(x.transform.position.x==tileCoor.x && x.transform.position.y==tileCoor.y) && 
                Player.CheckClear(x.transform.position,this.gameObject.transform.position))
                .Aggregate(false, (sum,cur)=>sum || cur))
            {
                GameObject sprite = SpriteFactory.CreateSprite("activeEnemy_PLACEHOLDER",SpriteFactory.SpriteMaterial.Normal);
                sprite.transform.position = new Vector3(tileCoor.x,tileCoor.y,-0.61f);
                sprite.AddComponent<BoxCollider2D>();
                sprite.name=$"Attack {tileCoor.x} {tileCoor.y}";
                highlightedTiles.Add(sprite);
            }
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
