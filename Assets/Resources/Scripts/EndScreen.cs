using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndScreen : MonoBehaviour
{
    private GameManager gameManager;
    public string endText;
    public string credits;
    private string lootText;
    public TMP_Text textBox;
    public TMP_Text creditsBox;
    private float typingSpeed = 0.08f;
    public GameObject continueButton;
    public GameObject sliderObject;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Time Traveler").GetComponent<GameManager>();
        
        if (gameManager.astroFreeze < 0)
        {
            endText = "The ship ran out of AstroFreeze, and due to the excessive heat of high-speed space travel, life support systems failed. The crew's journey ends here."+ Environment.NewLine + "RIP: ";
            foreach (string name in gameManager.playerNames)
            {
                endText += name + ". ";
            }
        } else if (!gameManager.playerLived0 && !gameManager.playerLived1 && !gameManager.playerLived2)
        {
            endText = "No one made it out alive. The crew's journey ends here." + Environment.NewLine + "RIP: ";
            foreach (string name in gameManager.playerNames)
            {
                endText += name + ". ";
            }
        } else if (gameManager.playerLived0 && gameManager.playerLived1 && gameManager.playerLived2)
        {
            endText = "The crew all made it out. " + Environment.NewLine + checkLoot();
        } else 
        {
            endText = "RIP: ";
            if (!gameManager.playerLived0)
            {
                endText += gameManager.playerName0 + ", ";
            }
            if (!gameManager.playerLived1)
            {
                endText += gameManager.playerName1 + ", ";
            }
            if (!gameManager.playerLived2)
            {
                endText += gameManager.playerName2 + ", ";
            }
            endText += "they will be missed." + Environment.NewLine + "Although, some of the crew made it out this time. " + Environment.NewLine + checkLoot();
        }
        
        credits = "This game was made in 10 days for the Helsinki-Abertay University Game Jam. " + Environment.NewLine;
        credits += "Game Design: " + "ArdenDemonic and TheWarpedMage " + Environment.NewLine;
        credits += "Programming: " + "danii and KauhaDev"+ Environment.NewLine;
        credits += "Audio: " + "Finket"+ Environment.NewLine;
        credits += "Art: " + "Ketchup";
        
        StartCoroutine(TypeText());
    }
    
    private string checkLoot()
    {
        if (gameManager.playerLoot0 || gameManager.playerLoot1 || gameManager.playerLoot2)
        {
            if (gameManager.playerLoot0)
            {
                lootText += gameManager.playerName0 + ", ";
            }
            if (gameManager.playerLoot1)
            {
                lootText += gameManager.playerName1 + ", ";
            }
            if (gameManager.playerLoot2)
            {
                lootText += gameManager.playerName2 + ", ";
            }
            lootText += "made it out with some AstroFreeze.";
        } else
        {
            lootText += "But no one got any AstroFreeze.";
        }
        
        return lootText;
    }
    
    IEnumerator TypeText()
    {
        textBox.text = "";
        foreach (char letter in endText.ToCharArray())
        {
            textBox.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        
        // now display buttons to continue
        continueButton.SetActive(true);
        StartCoroutine(TypeCredits());
        
        if (gameManager.astroFreeze >= 0)
        {
            try
            {
                sliderObject.SetActive(true);
                sliderObject.GetComponent<Slider>().value = gameManager.astroFreeze;
            } catch (Exception e)
            {
                Debug.Log(e);
                Debug.Log("Gameover scene");
            }
        }
    }
    
    IEnumerator TypeCredits()
    {
        creditsBox.text = "";
        foreach (char letter in credits.ToCharArray())
        {
            creditsBox.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
