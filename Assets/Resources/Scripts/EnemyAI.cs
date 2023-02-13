using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public static void TakeTurn()
    {
        //attack
        foreach(GameObject enemy in ResourceManager.Instance.enemies)
        {
            bool hit = false;
            foreach(GameObject player in ResourceManager.Instance.players.OfType<GameObject>().ToList().OrderBy(_ => Random.Range(0,100)).ToArray())
            {
                // player.transform.position = new Vector3(5f,5f,5f);
                if(Player.CheckClear(player.transform.position, enemy.transform.position) && (enemy.transform.position-player.transform.position).magnitude<Constants.enemyAttackRange)
                {
                    // player.transform.position = new Vector3(5f,5f,5f);
                    // Debug.Log(player);
                    // Debug.Log(player.GetComponent<ActionPoints>());
                    player.GetComponent<Damageable>().damage(Random.Range(1,10));
                    hit=true;
                    break;
                }
            }


            //move
            if(!hit)
            {
                Vector2[] toMove = Player.ComputeTilesInRange(3, enemy.transform.position);
                Vector2 movePosition = toMove[Random.Range(0,toMove.Length-1)];
                enemy.transform.position = new Vector3(movePosition.x, movePosition.y,-0.6f);
            }




        }





        

        // Check if the enemy can move
        // if (CanAttack())
        // {
        //     // Determine the best target based on player position
        //     Vector2Int target = DetermineBestTarget();
        //     // Attack the target
        //     AttackTarget(target);
        // }else if(CanMove()){
        //     // Determine the best move based on player position
        //     Vector2Int move = DetermineBestMove();
        //     // Move the enemy
        //     MoveEnemy(move);
        
        // }

        //EndTurn();
    }
    
    /* private bool CanMove()
    {
        // Determine if the enemy can move based on its current position and any obstacles
        // Code to check for available moves
        // Can borrow from player movement, if NPCs use AP
        return true;
    }

    private Vector2Int DetermineBestMove()
    {
        // Determine the best move based on player position and any obstacles
        // Code to determine best move
        // Can borrow from player movement, if NPCs use AP
        return new Vector2Int(0, 0);
    }

    private void MoveEnemy(Vector2Int move)
    {
        // Move the enemy to the determined position
        // Code to move enemy
        // Can just be teleport or the astar
    }

    private bool CanAttack()
    {
        // Determine if the enemy can attack based on its current position and any obstacles
        // Code to check for available attacks
        return true;
    }

    private Vector2Int DetermineBestTarget()
    {
        // Determine the best target based on player position and any other factors
        // Code to determine best target
        return new Vector2Int(0, 0);
    }

    private void AttackTarget(Vector2Int target)
    {
        // Attack the target
        // Code to attack target
    }

    public void EndTurn()
    {
        // reset AI AP and stuff
        ResourceManager.Instance.UpdateTurnText(true);
    } */
}
