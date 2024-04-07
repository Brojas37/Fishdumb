using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Text highScore;
    
    // Start is called before the first frame update
    void Start()
    {
        highScore.text = "High Score: " + LoadBestScore();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        // This function only works when the game build has been exported
        Application.Quit();
        // For testing purposes only in the Unity console when in Play mode
        Debug.Log("Exited Game");
    }

    [System.Serializable]
    class SaveData
    {
        public int highScore;
        public int coins;
        public int fish;
        public int selectedFish;
    }

    public int LoadBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            return data.highScore;
        }
        else
        {
            return 0;
        }
    }
}