using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public static void interact()
    {
        // interact sfx
        ResourceManager.Instance.PlaySFX("omenu3");
        
        GameObject player = ResourceManager.Instance.selectedPlayer;
        List<Vector2> directions = new List<Vector2>(4) {Vector2.up,Vector2.down,Vector2.left,Vector2.right};
        foreach(Vector2 dir in directions)
        {
            //    Debug.Log(ResourceManager.Instance.getTileObject(new Vector2(player.transform.position.x,player.transform.position.y)+dir).name);
            try
            {
                if(ResourceManager.Instance.getTileObject(new Vector2(player.transform.position.x,player.transform.position.y)+dir).name.Contains("loot"))
            {
                //change player sprite? add inventory script
                GameObject characterManager = GameObject.Find("Character Manager");
                CharacterManager characterManagerScript = characterManager.GetComponent<CharacterManager>();
                characterManagerScript.ShowLoot(player);
                
                // Debug.Log("loot get");
                ResourceManager.Instance.getTileObject(new Vector2(player.transform.position.x,player.transform.position.y)+dir).name="used";
            }

                if(ResourceManager.Instance.getTileObject(new Vector2(player.transform.position.x,player.transform.position.y)+dir).name.Contains("Spawn"))
                {
                    //check for loot. then exit
                }
            } catch (Exception e)
            {
                ResourceManager.Instance.PlaySFX("error");
                Debug.Log(e);
                break;
            }
            
        }
    }
}
