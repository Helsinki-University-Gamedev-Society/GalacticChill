using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterManager : MonoBehaviour
{
    public GameObject player0;
    public GameObject player1;
    public GameObject player2;
    public Image playerImage0;
    public Image playerImage1;
    public Image playerImage2;
    
    public GameObject playerLoot0;
    public GameObject playerLoot1;
    public GameObject playerLoot2;
    
    public TMP_Text playerName0;
    public TMP_Text playerName1;
    public TMP_Text playerName2;
    
    public Slider playerHealth0;
    public Slider playerAP0;
    public Slider playerHealth1;
    public Slider playerAP1;
    public Slider playerHealth2;
    public Slider playerAP2;
    
    void Start()
    {
        playerLoot0.SetActive(false);
        playerLoot1.SetActive(false);
        playerLoot2.SetActive(false);
    }
    
    public void DisplayCharacterSprites()
    {
        player0 = GameObject.FindWithTag("Player0");
        player1 = GameObject.FindWithTag("Player1");
        player2 = GameObject.FindWithTag("Player2");
        
        playerImage0.sprite = player0.GetComponent<SpriteRenderer>().sprite;
        playerImage1.sprite = player1.GetComponent<SpriteRenderer>().sprite;
        playerImage2.sprite = player2.GetComponent<SpriteRenderer>().sprite;
    }
    
    public void GiveNames()
    {
        GameManager timeTraveler = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        playerName0.text = timeTraveler.playerName0;
        playerName1.text = timeTraveler.playerName1;
        playerName2.text = timeTraveler.playerName2;
    }
    
    public void UpdateHP(GameObject callingObject, int hp)
    {        
        if (callingObject.tag == "Player0")
        {
            playerHealth0.value = hp;
        } else if (callingObject.tag == "Player1")
        {
            playerHealth1.value = hp;
        } else if (callingObject.tag == "Player2")
        {
            playerHealth2.value = hp;
        }
    }
    
    public void UpdateAP(GameObject callingObject, int ap)
    {
        if (callingObject.tag == "Player0")
        {
            playerAP0.value = ap;
        } else if (callingObject.tag == "Player1")
        {
            playerAP1.value = ap;
        } else if (callingObject.tag == "Player2")
        {
            playerAP2.value = ap;
        }
    }
    
    public void ShowLoot(GameObject player)
    {
        if (player.tag == "Player0")
        {
            playerLoot0.SetActive(true);
        } else if (player.tag == "Player1")
        {
            playerLoot1.SetActive(true);
        } else if (player.tag == "Player2")
        {
            playerLoot2.SetActive(true);
        }
    }
    
    public void HideLoot(GameObject player)
    {
        if (player.tag == "Player0")
        {
            playerLoot0.SetActive(false);
        } else if (player.tag == "Player1")
        {
            playerLoot1.SetActive(false);
        } else if (player.tag == "Player2")
        {
            playerLoot2.SetActive(false);
        }
    }
    
    public bool GetHasLoot(GameObject player)
    {
        if (player.tag == "Player0")
        {
            return playerLoot0.activeSelf;
        } else if (player.tag == "Player1")
        {
            return playerLoot1.activeSelf;
        } else if (player.tag == "Player2")
        {
            return playerLoot2.activeSelf;
        }
        return false;
    }
    
    public void UpdateExtracted(GameObject player)
    {
        if (player.tag == "Player0")
        {
            GameObject.Find("Time Traveler").GetComponent<GameManager>().playerLived0 = true;
        } else if (player.tag == "Player1")
        {
            GameObject.Find("Time Traveler").GetComponent<GameManager>().playerLived1 = true;
        } else if (player.tag == "Player2")
        {
            GameObject.Find("Time Traveler").GetComponent<GameManager>().playerLived2 = true;
        }
    }
}
