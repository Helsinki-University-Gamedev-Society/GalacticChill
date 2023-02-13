using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int health = 24;
    
    public void damage(int amount)
    {
        health -= amount;
        
        ResourceManager.Instance.UpdateHP(this.gameObject, health);
        Player.DealDeath(this.gameObject);
    }

    public void setHealth(int health)
    {
        this.health = health;
        
        ResourceManager.Instance.UpdateHP(this.gameObject, health);
    }

}
