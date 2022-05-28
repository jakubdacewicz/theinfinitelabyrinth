using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    [SerializeField] private string fileName;

    private GameData gameData;

    public List<GameObject> startItemList;

    private FileDataHandler fileDataHandler;

    public static DataPersistenceManager instance
    {
        get;
        private set;
    }

    private void Start()
    {
        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        LoadGame();
    }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("DataPersistenceManager currently exists in scene.");
        }        
        instance = this;
    }

    public void NewGame()
    {
        this.gameData = new GameData(startItemList);
    }

    public void LoadGame()
    {
        this.gameData = fileDataHandler.Load();

        if(this.gameData == null)
        {
            Debug.Log("No game data found! Creating new data.");
            NewGame();
        }

        GameObject.FindWithTag("GameController").GetComponent<GameController>().LoadData(gameData);
        GameObject.FindWithTag("ItemUnlockController").GetComponent<ItemUnlockController>().LoadData(gameData);      
    }

    public void SaveGame()
    {
        GameObject.FindWithTag("GameController").GetComponent<GameController>().SaveData(ref gameData);
        GameObject.FindWithTag("ItemUnlockController").GetComponent<ItemUnlockController>().SaveData(ref gameData);

        fileDataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
