using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class HeatManager : MonoBehaviour
{
    public static void damageHeat()
    {
        foreach(GameObject player in ResourceManager.Instance.players)
        {
            if(ResourceManager.Instance.getTileObject(new Vector2(player.transform.position.x,player.transform.position.y)).GetComponent<Flammable>().heat>50)
            {
                player.GetComponent<Damageable>().damage((int)Mathf.Lerp(0f,50f,ResourceManager.Instance.getTileObject(new Vector2(player.transform.position.x,player.transform.position.y)).GetComponent<Flammable>().heat/100));
                try
                {
                    player.GetComponent<ActionPoints>().exhaust((int)Mathf.Lerp(0f,12f,ResourceManager.Instance.getTileObject(new Vector2(player.transform.position.x,player.transform.position.y)).GetComponent<Flammable>().heat/100));
                } catch (System.Exception e)
                {
                    Debug.Log(e);
                    Debug.Log("Player already died to fire.");
                }
            }
        }
    }


    public static void propagateHeat()
    {
        List<Vector2> directions = new List<Vector2>(4) {Vector2.up,Vector2.down,Vector2.left,Vector2.right};
        for(int i = 0;i<ResourceManager.Instance.tileObjects.GetLength(0);i++)
        {
            for(int j = 0;j<ResourceManager.Instance.tileObjects.GetLength(1);j++)  
            {
                Tile obj = ResourceManager.Instance.getTile(new Vector2(j,i));
                if(!obj.isSolid || obj.type=="door" || obj.type=="barrel")
                {
                    Flammable flam = ResourceManager.Instance.getTileObject(new Vector2(j,i)).GetComponent<Flammable>();
                    flam.setHeat(directions.Where(x=>ResourceManager.Instance.getTile(new Vector2(j,i)+x).type!="wall").Select(x=>ResourceManager.Instance.getTileObject(new Vector2(j,i)+x).GetComponent<Flammable>().heat).Average());
                    if(flam.heat>32 && Random.Range(1,4)==1)
                    {
                        flam.setHeat(100);
                    }
                }

                if(obj.type=="barrel" && ResourceManager.Instance.getTileObject(new Vector2(j,i)).GetComponent<Flammable>().heat>=20
                && ResourceManager.Instance.getTileObject(new Vector2(j,i)).name!="used_barrel")
                {
                    Debug.Log(10);
                    foreach(Vector2 vec in Player.ComputeTilesInRange(4,new Vector2(j,i)))
                    {
                        Flammable flam = ResourceManager.Instance.getTileObject(vec).GetComponent<Flammable>();
                        flam.setHeat(100);
                    }
                    ResourceManager.Instance.PlaySFX("firegrows");
                    CameraPan cameraPan = Camera.main.GetComponent<CameraPan>();
                    cameraPan.Shake();
                    ResourceManager.Instance.getTileObject(new Vector2(j,i)).name="used_barrel";
                }
            }
        }
            
    }
}
