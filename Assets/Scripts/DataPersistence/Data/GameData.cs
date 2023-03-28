using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int score;

    public Vector3 playerPosition;

    // Default values with no data to load
    public GameData()
    {
        this.score = 0;
        this.playerPosition = new Vector3(0, 2f, 0);
    }

}
   
