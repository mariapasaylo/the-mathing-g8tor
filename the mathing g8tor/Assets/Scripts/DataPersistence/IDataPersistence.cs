/* Base code structure gotten from Rain Studios tutorial on "How to make a Save & Load System in Unity | 2022"
 available at https://www.youtube.com/watch?v=aUi9aijvpgs*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This interface needs to be added to the top of any script that will be implementing save or load. For example, if we have
'public class Score : MonoBehaviour', you would add it like this: 'public class Score : MonoBehaviour, IDataPersistence'.
Then these methods below for LoadData() and SaveData() are defined within the script that is using this interface. */
public interface IDataPersistence
{
    void LoadData(GameData data);
    void SaveData(ref GameData data);
}

// Example:

/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour, IDataPersistence
{
    // Score class attributes code would be here

    public void LoadData(GameData data)
    {
        // set the score in this class to the score in the game data
        this.score = data.score;
    }

    public void SaveData(ref GameData data)
    {
        // set the game data score to be equal to the score in this class
        data.score = this.score;
    }
} */
