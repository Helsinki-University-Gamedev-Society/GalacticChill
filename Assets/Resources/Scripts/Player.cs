using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;


public class Player
{



    public static void DealDeath(GameObject player)
    {
        if(player.GetComponent<Damageable>().health<=0)
        {
            player.transform.position=Constants.shadowRealm;
            Texture2D tex = ResourceManager.Instance.ImportTexture("Assets/Resources/Tile/Sprites/dead_sprite.png");    

            Sprite sprite = Sprite.Create(tex,
                    new Rect(0.0f, 0.0f, tex.width, tex.height),
                    new Vector2(0.5f, 0.5f));
            
            if (player.name.Contains("Player"))
            {
                player.GetComponent<BetterAnimator>().dead = true;
            }

            player.GetComponent<SpriteRenderer>().sprite=sprite;
            
            // I have to do this again, since I need it before and after changing the sprite.
            if (player.name.Contains("Player"))
            {
                GameObject.Find("Character Manager").GetComponent<CharacterManager>().DisplayCharacterSprites();
                GameObject.Find("Character Manager").GetComponent<CharacterManager>().HideLoot(player);
            }
            
            ResourceManager.Instance.players=ResourceManager.Instance.players.Where(val => val != player).ToArray();
            CheckIfLevelComplete();
        }
    }


    public static bool CheckClear(Vector3 pos1, Vector3 pos2)
    {
        RaycastHit hit;
        if (!Physics.Raycast(pos1, (pos2-pos1)/((pos2-pos1).magnitude), out hit, (pos2-pos1).magnitude))
        {
            return true;
            // Debug.Log($"{pos1.x} {pos1.y} {pos2.x} {pos2.y} {hit.collider.name}");
            // // Debug.DrawRay(pos1, (pos2-pos1)*range, Color.green, 10f);
            // if (!hit.collider.name.Contains("solid"))
            // {
            //     Debug.Log($"Did Hit {hit.distance}");
            //     return true;
            // }
        }
        return false;
    }




    private static int[,] TileMapToObstructionGrid()
    {
        Tile[,] tilemap = ResourceManager.Instance.tileArray;
        int[,] grid = new int[tilemap.GetLength(0),tilemap.GetLength(1)];
        for (int y = 0; y < tilemap.GetLength(0); y++) 
            {
                for (int x = 0; x < tilemap.GetLength(1); x++)
                {
                    if(tilemap[y,x].isSolid && tilemap[y,x].type!="door")
                    {
                        grid[y,x]=1;
                    }
                }
            }
        return grid;
    }
    

    public static Vector2[] ComputeTilesInRange(int range, Vector2 startPos)
    {
        // BFS time!
        List<Vector2> directions = new List<Vector2>(4) {Vector2.up,Vector2.down,Vector2.left,Vector2.right};
        Tile[,] tilemap = ResourceManager.Instance.tileArray;
        int[,] grid = TileMapToObstructionGrid();
        List<Vector2> tilesInRange = new List<Vector2>();
        Queue<Vector2> queue = new Queue<Vector2>();
        
        queue.Enqueue(startPos);
        tilesInRange.Add(startPos);

        while(queue.Count>0 && range > 0){
            int tileAtDepth = queue.Count;
            while (tileAtDepth-- > 0)
            {
                Vector2 curPos = queue.Dequeue();
                foreach (Vector2 direction in directions)
                {
                    Vector2 newPos = curPos + direction;
                    
                    if (newPos.x>=0 && newPos.x<tilemap.GetLength(1) && newPos.y>=0 && 
                        newPos.y<tilemap.GetLength(0) && grid[(int)(newPos.y),(int)(newPos.x)]==0 && 
                        !tilesInRange.Contains(newPos))
                    {
                        queue.Enqueue(newPos);
                        tilesInRange.Add(newPos);
                    }
                }
            }
            range--;
        }
        return tilesInRange.ToArray();
    }
    
    public static void CheckIfLevelComplete()
    {
        // check if all players are either extracted or dead, and complete level if so
        if (ResourceManager.Instance.players.Length > 0)
        {
            return;
        }
        
        // check if the ship runs out of AstroFreeze at this point
        GameManager gameManager = GameObject.Find("Time Traveler").GetComponent<GameManager>();
        int count = -2;
        if (gameManager.playerLoot0)
        {
            count++;
        }
        if (gameManager.playerLoot1)
        {
            count++;
        }
        if (gameManager.playerLoot2)
        {
            count++;
        }
        gameManager.astroFreeze += count;
        if (gameManager.astroFreeze > 10)
        {
            gameManager.astroFreeze = 10;
        }
        if (gameManager.astroFreeze < 0)
        {
            Debug.Log("The crew ran out of AstroFreeze!");
            CameraPan cameraPan = Camera.main.GetComponent<CameraPan>();
            ResourceManager.Instance.extractedPlayers = null;
            cameraPan.StartCoroutine(cameraPan.FadeToBlack(3));
            return;
        }
        
        // if we get to this point, we know all the players are KIA or extracted
        Debug.Log("Level over!");
        Debug.Log("Extracted players: ");
        
        
        if (ResourceManager.Instance.extractedPlayers?.Length > 0)
        {
            foreach (GameObject player in ResourceManager.Instance.extractedPlayers)
            {
                if (!player)
                {
                    continue;
                }
                Debug.Log(player.tag);
                GameObject.Find("Character Manager").GetComponent<CharacterManager>().UpdateExtracted(player);
            }
            CameraPan cameraPan = Camera.main.GetComponent<CameraPan>();
            ResourceManager.Instance.extractedPlayers = null;
            cameraPan.StartCoroutine(cameraPan.FadeToBlack(2));
            return;
        } else
        {
            Debug.Log("All players died!");
            CameraPan cameraPan = Camera.main.GetComponent<CameraPan>();
            ResourceManager.Instance.extractedPlayers = null;
            cameraPan.StartCoroutine(cameraPan.FadeToBlack(3));
            return;
        }

    }
    
    public static GameObject[] FindTilesInSight(GameObject player)
    {
        GameObject[,] tileObjects = ResourceManager.Instance.tileObjects;
        List<GameObject> tilesInSight = new List<GameObject>();

        for (int y = 0; y < tileObjects.GetLength(0); y++) 
            {
                for (int x = 0; x < tileObjects.GetLength(1); x++)
                {
                    if (CheckClear(player.transform.position, new Vector3(x,y,player.transform.position.z)) && !tilesInSight.Contains(tileObjects[y,x]))
                    {
                        tilesInSight.Add(tileObjects[y,x]);
                    }
                }
            }
            
        return tilesInSight.ToArray();
    }

    public static void ClearFog()
    {
        foreach(GameObject player in ResourceManager.Instance.players)
        {
            foreach(GameObject tile in FindTilesInSight(player))
            {
                tile.GetComponent<Obscurable>().Clear();
            }
        }
    }
}
