using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    private string selectedProfileId;

    public static DataPersistenceManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
        
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load(selectedProfileId);

        if (this.gameData == null)
        {
            Debug.Log ("No game data to load.");
            return;
        }

        foreach (IDataPersistence dataPersistenceObject in dataPersistenceObjects)
        {
            dataPersistenceObject.LoadData(gameData);
        }

        int sceneToLoad = gameData.completedQuest;
        if (sceneToLoad == 0)
        {
            SceneManager.LoadScene(1);
        }
        else if (sceneToLoad == 1)
        {
            SceneManager.LoadScene(2);
        }
        else if (sceneToLoad == 2)
        {
            SceneManager.LoadScene(3);
        }
        else if (sceneToLoad == 3)
        {
            SceneManager.LoadScene(3);
        }
        else
        {
            SceneManager.LoadScene(5);
        }
    }

    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObject in dataPersistenceObjects)
        {
            dataPersistenceObject.SaveData(ref gameData);
            Debug.Log("Saving data: " + dataPersistenceObject.ToString());
        }
        gameData.lastSavedDate = System.DateTime.Now.ToString();
        dataHandler.Save(gameData, selectedProfileId);
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        
        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public bool HasGameData()
    {
        return gameData != null;
    }

    public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return dataHandler.LoadAllProfiles();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void OnSceneUnloaded(Scene scene)
    {
        SaveGame();
    }

    public void ChangeSelectedProfileId(string profileId)
    {
        this.selectedProfileId = profileId;
    }

    public void SetSaveName(string saveName)
    {
        gameData.saveName = saveName;
    }
}
