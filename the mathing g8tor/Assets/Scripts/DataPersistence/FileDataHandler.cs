/* Base code structure gotten from Rain Studios tutorial on "How to make a Save & Load System in Unity | 2022"
 available at https://www.youtube.com/watch?v=aUi9aijvpgs*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    // The directory path of where we want to save the data on our computer
   private string dataDirPath = "";

    // The name of the file that we want to save to
   private string dataFileName = "";

    // Constructor to take in the 2 varaibles above and set them
   public FileDataHandler(string dataDirPath, string dataFileName)
   {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
   }

    // Returns a game data object from the Json file for loading saved data
   public GameData Load()
   {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        
        if (File.Exists(fullPath))
        {
            try
            {
                // Load the serialized data from the file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                // deserialize the data from Json back into the C# game object
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);

            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
   }

    // Takes in a game data object and writes it to the Json file for saving
    public void Save(GameData data)
   {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            // Create the directory the file will be written to if it doesn't already exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // Serialize the C# game data object into Json string ('true' param means that we can format the Json data)
            string dataToStore = JsonUtility.ToJson(data, true);

            // Write the serialized data to the file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }
   }

}
