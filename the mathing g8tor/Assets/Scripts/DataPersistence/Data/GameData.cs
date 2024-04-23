/* Base code structure gotten from Rain Studios tutorial on "How to make a Save & Load System in Unity | 2022"
 available at https://www.youtube.com/watch?v=aUi9aijvpgs*/

using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;


[System.Serializable]
public class GameData
{
    // Player Position
    public UnityEngine.Vector2 position;

    // ...add more initializations here as scripts are written
    // An example using 'score' gotten from Maria's pseudocode below
    public int score;
    

    /* The values defined in this constructor will be the default values
    when the game starts and there is no data to load */
    public GameData()
    {
        // Set the player position at start flag
        position = new UnityEngine.Vector2(-13, -2);

        // Example
        this.score = 0;
        // ...add more game data here as scripts are written
    }
}


// Maria's pseudocode for reference
/* If batteryCollected == portalBatteryNeeded
     score = score + 1

If batteryCollected > portalBatteryNeeded
     score = score - 1
     batteryCollected = 0
     respawnBatteries()
Else
     batteryCollected = batteryCollected + batteryEnergyAmount */