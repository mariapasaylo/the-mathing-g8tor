/* Base code structure gotten from Rain Studios tutorial on "How to make a Save & Load System in Unity | 2022"
 available at https://www.youtube.com/watch?v=aUi9aijvpgs*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // Aids in finding the data persistence of objects

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    /* { get; private set; }, means we will be able to get the instance publically, however we can
    only modify the instance privately within this class */
    public static DataPersistenceManager instance { get; private set; }

    /* This class keeps track of the current state of the game state, there can only be one
    in the scene at any given time */
    private void Awake()
    {
        if (instance != null) 
        {
            Debug.LogError("Found more than one Data Persistance Manager in the scene.");
        }
        instance = this;
    }

    private void Start()
    {
        // 'Application.persistentDataPath' gives the operating system a standard directory for persisting data in Unity
        // It saves the game data here - C:\Users\<user name>\AppData\LocalLow\DefaultCompany\the mathing g8tor
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        
        // For now, we load the game on startup and save the game when the app exits, see OnApplicationQuit() below
        LoadGame();
    }

    public void NewGame()
    {
        // Initialize game data to be a new game data object
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        /* Load any saved data from a file using the data handler, if the data does not 
        exist, this value 'dataHandler.Load()' will be null */
        this.gameData = dataHandler.Load();

        // If no data can be loaded, initialize to a new game
        if (this.gameData == null)
        {
            Debug.Log("No data was found. Initialzing data to defaults.");
            NewGame();
        }

        // Push the loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }

        // for debugging if needed ...
        //Debug.Log("Loaded score = " + gameData.score);

    }

    public void SaveGame()
    {
        // Pass the data to other scripts so they can update it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        // for debugging if needed ...
        //Debug.Log("Saved score = " + gameData.score);

        // Save that data to a file using the data handler
        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        // Because we are using System.Linq, we can find all scripts that implement the data persistence interface
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
