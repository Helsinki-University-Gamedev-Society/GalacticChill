using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour
{
    private GameObject selectedPlayer;

    public void ChangeTurn()
    {
        ResourceManager.Instance.PlaySFX("cmenu2");
        
        ResourceManager.Instance.UpdateTurnText(false);
        selectedPlayer = GameObject.FindWithTag("Player0");
        selectedPlayer.GetComponent<ActionPoints>().reset();
        selectedPlayer.GetComponent<Highlighter>().Clear();
        
        selectedPlayer = GameObject.FindWithTag("Player1");
        selectedPlayer.GetComponent<ActionPoints>().reset();
        selectedPlayer.GetComponent<Highlighter>().Clear();
        
        selectedPlayer = GameObject.FindWithTag("Player2");
        selectedPlayer.GetComponent<ActionPoints>().reset();
        selectedPlayer.GetComponent<Highlighter>().Clear();

        ResourceManager.Instance.playerTurn = false;

        EnemyAI.TakeTurn();

        
        // cease player control
        // start AI turn
        // AI turn ends turn
        HeatManager.propagateHeat();
        HeatManager.damageHeat();

        ResourceManager.Instance.playerTurn = true;
        ResourceManager.Instance.UpdateTurnText(true);
    }
}
