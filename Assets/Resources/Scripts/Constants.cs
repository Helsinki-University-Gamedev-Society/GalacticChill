using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Constants
{
    private Constants() {}

    public static readonly string tilePropertiesPath = "Tile/properties";

    // public static readonly string testboard02 = "Assets/Resources/LevelData/testboard02.png";
    public static readonly string jamlvl = "Assets/Resources/LevelData/jamlvl.png";
    public static readonly string saucer = "Assets/Resources/LevelData/saucer.png";

    public static readonly Vector2 origin = Vector2.zero;

    public static readonly Vector3 shadowRealm = new Vector3(100,100,100);
    
    public static readonly int playerAttackRange = 6;
    public static readonly int enemyAttackRange = 6;
    public static readonly int enemyMoveRange = 6;

    public static readonly int maxPlayers = 3;
    
    public static readonly int itemDimension = 16;


    
}
