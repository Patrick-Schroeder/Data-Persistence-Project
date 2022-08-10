using System.Collections;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string Name;
    public string BestScoreName;
    public int BestScore;

    private void Awake()
    {
        Instance = this;

        LoadScore();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        StartCoroutine(StartLoad());
    }

    IEnumerator StartLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

        while (!operation.isDone)
        {
            yield return null;
        }
    }

    public void StartNew()
    {
        SceneManager.UnloadSceneAsync(1);
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

    // Most of the time you won’t save everything inside your classes. It’s good practice and more efficient to use a small class that only contains the specific data that you want to save.
    [System.Serializable]
    class SaveData
    {
        public string Name;
        public int Score;
    }

    public void SaveScore(int currentScore)
    {
        if (currentScore < BestScore)
        {
            return;
        }

        SaveData data = new SaveData();

        data.Name = Name;
        data.Score = currentScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            BestScoreName = data.Name;
            BestScore = data.Score;
        }
    }
}
