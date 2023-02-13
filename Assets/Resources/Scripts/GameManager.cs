using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using AStarSharp;

public class GameManager : MonoBehaviour
{
    public string playerName0;
    public string playerName1;
    public string playerName2;
    
    public List<string> playerNames;
    
    public bool playerLoot0;
    public bool playerLoot1;
    public bool playerLoot2;
    
    public bool playerLived0;
    public bool playerLived1;
    public bool playerLived2;
    
    public int astroFreeze = 3;
    
    private GameObject selectedPlayer {get => ResourceManager.Instance.selectedPlayer; set  => ResourceManager.Instance.selectedPlayer = value;}
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hits = Physics2D.GetRayIntersectionAll(ray, 1000f);
            
            foreach (var hit in hits)
            {
                if(hit.collider.name.Contains("Button")) break;
                if(!ResourceManager.Instance.playerTurn) break;
                if(hit.collider.name.Contains("Player"))
                {
                    if (selectedPlayer)
                    {
                        selectedPlayer.GetComponent<Highlighter>().Clear();
                        selectedPlayer.GetComponent<AttackHighlighter>().Clear();
                        selectedPlayer = null;
                        ResourceManager.Instance.PlaySFX("cmenu1");
                        // need to break to avoid players stacking and breaking the code
                        break;
                    }
                    selectedPlayer = hit.collider.gameObject;
                    
                    selectedPlayer.GetComponent<Highlighter>().Clear();
                    selectedPlayer.GetComponent<Highlighter>().tilesToHighlight 
                        = Player.ComputeTilesInRange(selectedPlayer.GetComponent<ActionPoints>().getActionPoints()/8,
                        new Vector2(selectedPlayer.transform.position.x,selectedPlayer.transform.position.y));
                    selectedPlayer.GetComponent<Highlighter>().Highlight();

                    
                    selectedPlayer.GetComponent<AttackHighlighter>().Clear();
                    selectedPlayer.GetComponent<AttackHighlighter>().tilesToHighlight 
                        = Player.ComputeTilesInRange(selectedPlayer.GetComponent<ActionPoints>().getActionPoints()/4,
                        new Vector2(selectedPlayer.transform.position.x,selectedPlayer.transform.position.y));
                    selectedPlayer.GetComponent<AttackHighlighter>().Highlight();

                    // play the selection sfx
                    ResourceManager.Instance.PlaySFX("clickon");
                }

                if(hit.collider.name.Contains("Attack"))
                {
                    if(selectedPlayer.GetComponent<ActionPoints>().getActionPoints()<12){ResourceManager.Instance.PlaySFX("error");break;}
                    
                    // play a random attack sfx
                    string[] options = new string[] {"attack1","attack2","attack3"};
                    System.Random random = new System.Random();
                    int randomIndex = random.Next(0, options.Length);
                    string attack = options[randomIndex];
                    ResourceManager.Instance.PlaySFX(attack);
                    
                    // camera shake
                    CameraPan cameraPan = Camera.main.GetComponent<CameraPan>();
                    StartCoroutine(cameraPan.CameraShake(0.2f));
                    
                    GameObject enemy = ResourceManager.Instance.enemies.Where(
                                                x=>(hit.collider.gameObject.transform.position.x==x.transform.position.x 
                                                    && hit.collider.gameObject.transform.position.y==x.transform.position.y)).First();
                    selectedPlayer.GetComponent<ActionPoints>().exhaust(12);
                    selectedPlayer.GetComponent<Highlighter>().Clear();
                    selectedPlayer.GetComponent<AttackHighlighter>().Clear();
                    selectedPlayer=null;

                    enemy.GetComponent<Damageable>().damage(12);
                    if(enemy.GetComponent<Damageable>().health<=0)
                    {
                        ResourceManager.Instance.enemies.Remove(enemy);
                        Destroy(enemy);
                    }

                    break;
                }

                if(hit.collider.name.Contains("Highlight"))
                {
                    List<List<Node>> Grid = new List<List<Node>>();
                    Tile[,] tileArray = ResourceManager.Instance.tileArray;

                    for(int i = 0;i<tileArray.GetLength(0);i++)
                    {
                        List<Node> subGrid = new List<Node>();
                        for(int j = 0;j<tileArray.GetLength(1);j++)
                        {
                            subGrid.Add(new Node(new System.Numerics.Vector2(i,j),!tileArray[i,j].isSolid || tileArray[i,j].type=="door"));
                        }
                        Grid.Add(subGrid);
                    }


                    Astar astar = new Astar(Grid);
                    
                    if (new Vector2(selectedPlayer.transform.position.y,selectedPlayer.transform.position.x) != new Vector2(hit.collider.gameObject.transform.position.y,hit.collider.gameObject.transform.position.x))
                    {
                        // play a random movement sfx
                        string[] options = new string[] {"roger1","roger2","roger3","roger4"};
                        System.Random random = new System.Random();
                        int randomIndex = random.Next(0, options.Length);
                        string roger = options[randomIndex];
                        ResourceManager.Instance.PlaySFX(roger);
                    
                        Stack<Node> path = astar.FindPath(new System.Numerics.Vector2(selectedPlayer.transform.position.y, selectedPlayer.transform.position.x),
                                                            new System.Numerics.Vector2(hit.collider.gameObject.transform.position.y, hit.collider.gameObject.transform.position.x));

                        selectedPlayer.GetComponent<ActionPoints>().exhaust(path.Count*8);
                        selectedPlayer.transform.position = new Vector3(hit.collider.gameObject.transform.position.x,hit.collider.gameObject.transform.position.y,-0.6f);
                        selectedPlayer.GetComponent<Highlighter>().Clear();
                        selectedPlayer.GetComponent<AttackHighlighter>().Clear();
                        selectedPlayer=null;
                        Player.ClearFog();
                    }
                }

                






            }
        }
        
        if (Input.GetMouseButtonDown(1) && selectedPlayer)
        {
            selectedPlayer.GetComponent<Highlighter>().Clear();
            selectedPlayer.GetComponent<AttackHighlighter>().Clear();
            selectedPlayer = null;
        }
    }
    
    public void FillNames()
    {
        TextAsset asset = Resources.Load<TextAsset>("CharacterNames");
        string jsonText = asset.text;
        CharacterNames names = new CharacterNames();
        names = JsonUtility.FromJson<CharacterNames>(jsonText);
        
        if (!playerLived0 || string.IsNullOrEmpty(playerName0))
        {
            string firstname = names.firstnames[UnityEngine.Random.Range(0, names.firstnames.Length)];
            string lastname = names.lastnames[UnityEngine.Random.Range(0, names.lastnames.Length)];
            playerName0 = firstname + " " + lastname;
            playerNames.Add(playerName0);
        }
        if (!playerLived1 || string.IsNullOrEmpty(playerName1))
        {
            string firstname = names.firstnames[UnityEngine.Random.Range(0, names.firstnames.Length)];
            string lastname = names.lastnames[UnityEngine.Random.Range(0, names.lastnames.Length)];
            playerName1 = firstname + " " + lastname;
            playerNames.Add(playerName1);
        }
        if (!playerLived2 || string.IsNullOrEmpty(playerName2))
        {
            string firstname = names.firstnames[UnityEngine.Random.Range(0, names.firstnames.Length)];
            string lastname = names.lastnames[UnityEngine.Random.Range(0, names.lastnames.Length)];
            playerName2 = firstname + " " + lastname;
            playerNames.Add(playerName2);
        }
    }
}
                    // Astar astar = new Astar();
                    // List<Coordinates> path = astar.Astar(new Coordinates(
                    //                                         selectedPlayer.transform.position.x,selectedPlayer.transform.position.y),
                    //                                         new Coordinates(
                    //                                         hit.collider.gameObject.transform.position.x,hit.collider.gameObject.transform.position.y),
                    //                                         ResourceManager.Instance.tileArray.GetLength(0),
                    //                                         ResourceManager.Instance.tileArray.GetLength(1)
                    //                                         );