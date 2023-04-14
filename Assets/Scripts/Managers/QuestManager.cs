using UnityEngine;
using UnityEngine.UI;
public class QuestManager : MonoBehaviour, IDataPersistence
{
    private static QuestManager instance;
    public static int CompletedQuest = 1;
    private Text text;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        text = GetComponent <Text> ();
    }

    public static void UpdateQuest()
    {
        CompletedQuest += 1;
    }
    
    private void Update()
    {
        text.text = $"Quest: ({CompletedQuest}/4)";
    }

    public void LoadData(GameData data)
    {
        CompletedQuest = data.completedQuest;
    }

    public void SaveData(ref GameData data)
    {
        data.completedQuest = CompletedQuest;
    }
}

