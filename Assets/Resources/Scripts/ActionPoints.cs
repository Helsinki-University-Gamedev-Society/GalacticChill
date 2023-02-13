using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPoints : MonoBehaviour
{
    private int maxAction = 24;
    private int action = 24;
    

    public void exhaust(int amount){
    	action -= amount;
        
        ResourceManager.Instance.UpdateAP(this.gameObject, action);
    }
    public void reset(){
    	action = maxAction;
        
        ResourceManager.Instance.UpdateAP(this.gameObject, action);
    }

    public void setActionPoints(int points){
    	this.action = points;
        
        ResourceManager.Instance.UpdateAP(this.gameObject, points);
    }
    
    public void maxActionPoints(int points){
        if(action>points) action=points;
    	this.maxAction = points;
    }

    public int getActionPoints(){
    	return action;
    }
}
