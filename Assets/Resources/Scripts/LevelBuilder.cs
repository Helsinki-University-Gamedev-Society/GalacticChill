using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    private ResourceManager resources;
    
    public GameObject characterManager;
    private CharacterManager characterManagerScript;
    
    void Start()
    {
        GameObject timeTraveler = GameObject.Find("Time Traveler");
        GameManager gameManager = timeTraveler.GetComponent<GameManager>();
        gameManager.FillNames();
        gameManager.playerLoot0 = false;
        gameManager.playerLoot1 = false;
        gameManager.playerLoot2 = false;
        gameManager.playerLived0 = false;
        gameManager.playerLived1 = false;
        gameManager.playerLived2 = false;
        
        resources = ResourceManager.Instance;
        
        int randomInt = UnityEngine.Random.Range(0,2);
        if (randomInt==0)
        {
            resources.LoadMap(Constants.jamlvl);
        } else
        {
            resources.LoadMap(Constants.saucer);
        }
        
        BuildLevel(resources.tileArray);
        
        characterManagerScript = characterManager.GetComponent<CharacterManager>();
        characterManagerScript.DisplayCharacterSprites();
        characterManagerScript.GiveNames();
        
        CameraPan cameraPan = Camera.main.GetComponent<CameraPan>();
        cameraPan.StartCoroutine(cameraPan.FadeToClear());
    }

    private void BuildLevel(Tile[,] tileArray)
    {
        int playerCount = 0;
        int enemyCount = 0;
        GameObject[,] tileObjects = new GameObject[tileArray.GetLength(0),tileArray.GetLength(1)];
        GameObject[] players = new GameObject[Constants.maxPlayers];
        List<GameObject> enemies = new List<GameObject>();
        for (int y = 0; y < tileArray.GetLength(0); y++) 
        {
            for (int x = 0; x < tileArray.GetLength(1); x++)
            {
                if(tileArray[y,x].type=="space")
                {
                    continue;
                }
                
                Material mat = new Material(Shader.Find("psx/vertexlit"));
                if (tileArray[y,x].type=="door")
                {
                    mat = new Material(Shader.Find("Transparent/Diffuse"));
                }
                mat.SetTexture("_MainTex", resources.ImportTexture("Assets/Resources/Tile/Sprites/"+tileArray[y,x].sprite+".png"));

                var tile = GameObject.CreatePrimitive(PrimitiveType.Cube);
                tile.name = string.Format("{0} {1}",x,y);
                tile.transform.position = new Vector3(x,y,0);
                tile.GetComponent<MeshRenderer>().material = mat;
                
                tile.AddComponent<Flammable>();
                GameObject fire = SpriteFactory.CreateSprite("fire01_PLACEHOLDER",SpriteFactory.SpriteMaterial.Normal);
                fire.transform.position = new Vector3(x,y,-0.7f);
                tile.GetComponent<Flammable>().fire = fire;
                tile.GetComponent<Flammable>().setHeat(tileArray[y,x].heat);

                tile.AddComponent<Obscurable>();
                
                if (tileArray[y,x].type!="wall" && tileArray[y,x].type!="door" && tileArray[y,x].type!="barrel")
                {
                    GameObject fog = SpriteFactory.CreateSprite("fog",SpriteFactory.SpriteMaterial.Normal);
                    fog.transform.position = new Vector3(x,y,-0.8f);
                    fog.transform.localScale += new Vector3(0.05f,0.05f,0f);
                    tile.GetComponent<Obscurable>().fog = fog;
                }
                
                // tile.AddComponent<Obscurable>();
                // GameObject fog = SpriteFactory.CreateSprite("fog",SpriteFactory.SpriteMaterial.Normal);
                // fog.transform.position = new Vector3(x,y,-0.7f);
                // fog.
                // tile.GetComponent<Flammable>().fire = fire;
                // tile.GetComponent<Flammable>().setHeat(tileArray[y,x].heat);

                if(tileArray[y,x].isSolid){
                    tile.GetComponent<Flammable>().fire.transform.position = new Vector3(x,y,-1.7f);
                    tile.transform.position += new Vector3(0,0,-1);
                    tile.name = string.Format("solid {0} {1}",x,y);
                    tile.AddComponent<BoxCollider>();
                }

                if(tileArray[y,x].type=="door")
                {
                    tile.name = string.Format("door {0} {1}",x,y);
                }

                if(tileArray[y,x].type=="boiler")
                {
                    tile.name = string.Format("boiler {0} {1}",x,y);
                }

                if(tileArray[y,x].type=="chest")
                {
                    tile.name = string.Format("loot {0} {1}",x,y);
                    tile.transform.position += new Vector3(0f,0f,1f);
                }
                
                if(tileArray[y,x].type=="spawn")
                {
                    tile.name = string.Format("spawn {0} {1}",x,y);
                }

                tileObjects[y,x] = tile;

                if((tileArray[y,x].type=="spawn") && (playerCount < Constants.maxPlayers)){
                    
                    GameObject player = SpriteFactory.CreateSprite($"player0{playerCount+1}_sprite1",SpriteFactory.SpriteMaterial.Diffuse);
                    
                    player.transform.position = new Vector3(x,y,-0.6f);

                    player.name = $"Player{playerCount}";
                    
/*                     try {
                        ResourceRequest request = Resources.LoadAsync($"Tile/Sprites/player0{playerCount+1}_animationclip");
                        AnimationClip animationClip = request.asset as AnimationClip;
                        Animation animation = player.AddComponent<Animation>();
                        animation.AddClip(animationClip,$"animation{playerCount}");
                        animation.Play($"animation{playerCount}");
                    } catch(Exception e){
                        Debug.Log(e);
                        Debug.Log("No animation clip.");
                    } */
                    

                    player.AddComponent<BoxCollider2D>();

                    player.AddComponent<Damageable>();

                    player.AddComponent<Highlighter>();
                    player.AddComponent<AttackHighlighter>();
                    
                    player.AddComponent<ActionPoints>();
                    
                    player.AddComponent<BetterAnimator>();
                    
                    player.tag = $"Player{playerCount}";
                                        
                    player.GetComponent<ActionPoints>().setActionPoints(24);
                    player.GetComponent<Damageable>().setHealth(24);

                    players[playerCount] = player;
                    
                    playerCount++;
                }

                if((tileArray[y,x].type=="enemy")){
                    GameObject enemy = SpriteFactory.CreateSprite($"enemy_PLACEHOLDER",SpriteFactory.SpriteMaterial.Diffuse);
                    
                    enemy.name = $"Enemy{enemyCount}";

                    enemy.transform.position = new Vector3(x,y,-0.6f);

                    enemy.AddComponent<BoxCollider2D>();

                    enemy.AddComponent<Damageable>();

                    enemies.Add(enemy);

                    enemyCount++;
                }

                


            }
        }

        resources.players = players;
        resources.enemies = enemies;
        resources.tileObjects = tileObjects;
        Player.ClearFog();
        
        ResourceManager.Instance.UpdateTurnText(true);
    }



}