using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using TMPro;

public sealed class ResourceManager
{
    
    private ResourceManager()
    {
        LoadTileProperties(Constants.tilePropertiesPath);   
    }

    public static ResourceManager Instance { get { 
        if(instance == null){
            instance = new ResourceManager();
        }
        return instance;
        } }
    private static ResourceManager instance;


    public Dictionary<string, Tile> hexToTile;
    
    public Tile[,] tileArray;

    public GameObject selectedPlayer;

    public bool playerTurn = true;

    public GameObject[,] tileObjects;
    public GameObject[] players;
    public List<GameObject> enemies;

    public GameObject[] extractedPlayers;

    public GameObject getTileObject(Vector2 tilecoor)
    {
        return tileObjects[(int)tilecoor.y, (int)tilecoor.x];
    }

    public Tile getTile(Vector2 tilecoor)
    {
        return tileArray[(int)tilecoor.y, (int)tilecoor.x];
    }
    
    private void LoadTileProperties(string path)
    {
        hexToTile = new Dictionary<string,Tile>();
        string jsonText = Resources.Load<TextAsset>(path).text;
        TileDataContainer tileContainer = JsonUtility.FromJson<TileDataContainer>(jsonText);
        foreach (Tile tile in tileContainer.tiles)
        {
            hexToTile[tile.color] = tile;
        }
    }

    public void LoadMap(string path)
    {
        Texture2D image = ImportTexture(path);
        // Get the pixel data from the bitmap
        int width = image.width;
        int height = image.height;
        Color32[] pixels = image.GetPixels32();
        // Convert the pixel data into a two-dimensional array of tiles based on hexadecimal color codes
        Tile[,] tileArray = new Tile[height, width];
        for (int y = 0; y < height; y++) 
        {
            for (int x = 0; x < width; x++)
            {
                Color32 pixel = pixels[y * width + x];
                tileArray[y, x] = this.hexToTile[ColorUtility.ToHtmlStringRGB(pixel)];
            }
        }
        this.tileArray = tileArray;
    }

    public Texture2D ImportTexture(string path)
    {
        
        Texture2D image = new Texture2D(1,1);
        // Load the image file into a byte array
        byte[] imageBytes = File.ReadAllBytes(path);
        // Load the byte array into the Texture2D object
        image.LoadImage(imageBytes);
        return image;
    }
    
    public void PlaySFX(string clip)
    {
        GameObject speaker = GameObject.Find("SFX Player");
        SFXPlayer sfxPlayer = speaker.GetComponent<SFXPlayer>();
        sfxPlayer.PlaySFX(clip);
    }
    
    public void UpdateHP(GameObject callingObject, int hp)
    {
        GameObject characterManager = GameObject.Find("Character Manager");
        CharacterManager characterManagerScript = characterManager.GetComponent<CharacterManager>();
        characterManagerScript.UpdateHP(callingObject, hp);
    }
    
    public void UpdateAP(GameObject callingObject, int ap)
    {
        GameObject characterManager = GameObject.Find("Character Manager");
        CharacterManager characterManagerScript = characterManager.GetComponent<CharacterManager>();
        characterManagerScript.UpdateAP(callingObject, ap);
    }
    
    public void UpdateTurnText(bool yourTurn)
    {
        GameObject textObject = GameObject.Find("Turn Indicator");
        TMP_Text textComponent = textObject.GetComponent<TMP_Text>();
        
        if (yourTurn)
        {
            textComponent.text = "Turn: You";
        } else
        {
            textComponent.text = "Turn: Enemy";
        }
    }
}
