using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Departer : MonoBehaviour
{
    public GameObject[] departedPlayers;
    
    void Start()
    {
        departedPlayers = new GameObject[Constants.maxPlayers];
    }
    
    public void Depart()
    {
        // interact sfx
        ResourceManager.Instance.PlaySFX("omenu1");
        GameObject player = ResourceManager.Instance.selectedPlayer;
        List<Vector2> directions = new List<Vector2>(4) {Vector2.up,Vector2.down,Vector2.left,Vector2.right};
        
        foreach(Vector2 dir in directions)
        {
            try
            {
                if(ResourceManager.Instance.getTileObject(new Vector2(player.transform.position.x,player.transform.position.y)+dir).name.Contains("spawn"))
                {
                    player.transform.position = Constants.shadowRealm;
                    player.GetComponent<Highlighter>().Clear();
                    ResourceManager.Instance.selectedPlayer = null;
                    ResourceManager.Instance.players=ResourceManager.Instance.players.Where(val => val != player).ToArray();
                    
                    if (player.tag=="Player0")
                    {
                        departedPlayers[0] = player;
                        if (GameObject.Find("Character Manager").GetComponent<CharacterManager>().GetHasLoot(player))
                        {
                            GameObject.Find("Time Traveler").GetComponent<GameManager>().playerLoot0 = true;
                        }
                    } else if (player.tag=="Player1")
                    {
                        departedPlayers[1] = player;
                        if (GameObject.Find("Character Manager").GetComponent<CharacterManager>().GetHasLoot(player))
                        {
                            GameObject.Find("Time Traveler").GetComponent<GameManager>().playerLoot1 = true;
                        }
                    } else if (player.tag=="Player2")
                    {
                        departedPlayers[2] = player;
                        if (GameObject.Find("Character Manager").GetComponent<CharacterManager>().GetHasLoot(player))
                        {
                            GameObject.Find("Time Traveler").GetComponent<GameManager>().playerLoot2 = true;
                        }
                    }
                                        
                    ResourceManager.Instance.extractedPlayers = departedPlayers;
                    
                    break;
                }
            } catch (Exception e)
            {
                ResourceManager.Instance.PlaySFX("error");
                Debug.Log(e);
                break;
            }
        }
        Player.CheckIfLevelComplete();
    }
}
