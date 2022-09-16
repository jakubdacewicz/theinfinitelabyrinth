using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    private GameData gameData;

    private FileDataHandler fileDataHandler;

    public string fileName;

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
        instance = this;
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        this.gameData = fileDataHandler.Load();

        if(this.gameData == null)
        {
            NewGame();
        }

        GameObject.FindWithTag("GameController").GetComponent<GameController>().LoadData(gameData);
        GameObject.FindWithTag("ItemUnlockController").GetComponent<ItemUnlockController>().LoadData(gameData);
    }

    public void LoadRecords()
    {
        GameController gm = GameObject.Find("GameController").GetComponent<GameController>();
        if (gm != null)
        {
            gm.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        GameObject.FindWithTag("GameController").GetComponent<GameController>().SaveData(ref gameData);
        GameObject.FindWithTag("ItemUnlockController").GetComponent<ItemUnlockController>().SaveData(ref gameData);

        fileDataHandler.Save(gameData);
    }
}
