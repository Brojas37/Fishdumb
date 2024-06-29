using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private MusicController musicController;

    [SerializeField] public GameObject[] fishes;
    
    [SerializeField] private GameObject player;

    [SerializeField] public float playerX;

    public int score { get; private set; }
    public bool gameOver { get; set; }

    public float trashSpeed;

    [SerializeField]
    private GameUI gameUI;
    [SerializeField]
    private GameObject Battery;

    public GameObject[] Trash;

    private float trashTimer;
    private float trash2Timer;
    private float trash3Timer;
    private float trash4Timer;
    [SerializeField] private float trashWait;
    [SerializeField] private float trash2Wait;
    [SerializeField] private float trash3Wait;
    [SerializeField] private float trash4Wait;

    [SerializeField] private float scoreTimer;
    [SerializeField] private float scoreWait;

    private float spawnBuffer = 30;

    public GameObject coin;

    private float coinTimer;
    private float coinWait;
    private float actualCoinWait;

    private int coinsInGame;

    [SerializeField] Text inGameCoins;
    [SerializeField] Text doubledCoins;

    [SerializeField] private Text coinsText;

    // Start is called before the first frame update
    void Start()
    {
        //SaveCoins(10000);

        musicController = GameObject.Find("Music").GetComponent<MusicController>();
        gameOver = false;
        score = 0;
        gameUI = GameObject.Find("Canvas").GetComponent<GameUI>();
        gameUI.StartGame();
        trashTimer = 0.0f;
        trashWait = 1.5f;
        trash2Timer = 0.0f;
        trash2Wait = 5.0f;
        trash3Timer = 0.0f;
        trash3Wait = 10.0f;
        trash4Timer = 0.0f;
        trash4Wait = 15.0f;
        trashSpeed = 0.7f;
        scoreTimer = 0.0f;
        scoreWait = 1.0f;

        coinTimer = 0.0f;
        coinWait = 2.0f;
        actualCoinWait = coinWait += Random.Range(-0.2f, 0.5f);
        coinsInGame = 0;

        coinsText.text = "Coins: " + coinsInGame;

        player = fishes[LoadSelectedFish() - 1];
        player.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //SaveBestScore(0);
        //SaveBestScore(LoadBestScore());

        if (!gameOver)
        {
            CheckTrash();
            CheckScore();
            playerX = player.transform.position.x;
        }

    }

    public void addCoin()
    {
        coinsInGame++;
        Debug.Log("Coin added");

        coinsText.text = "Coins: " + coinsInGame;
    }

    public void GameOver()
    {
        if (score > LoadBestScore())
        {
            SaveBestScore(score);
        }
        gameOver = true;
        SaveCoins(LoadCoins() + coinsInGame);
        coinsText.text = "Total Coins: " + LoadCoins();
        inGameCoins.text = "Coins Gained: " + coinsInGame;
    }

    private void CheckTrash()
    {
        trashTimer += Time.deltaTime;
        if (trashTimer > trashWait)
        {
            trashWait += (0.2f - trashWait) / (spawnBuffer / trashWait);
            trashSpeed += (3.0f - trashSpeed) / (100/trashWait);
            trashTimer = 0.0f;
            CreateTrash();
        }
        trash2Timer += Time.deltaTime;
        if (trash2Timer > trash2Wait)
        {
            trash2Wait += (0.2f - trash2Wait) / (spawnBuffer / trash2Wait);
            trash2Timer = 0.0f;
            CreateTrash();
        }
        trash3Timer += Time.deltaTime;
        if (trash3Timer > trash3Wait)
        {
            trash3Wait += (0.2f - trash3Wait) / (spawnBuffer / trash3Wait);
            trash3Timer = 0.0f;
            CreateTrash();
        }
        trash4Timer += Time.deltaTime;
        if (trash4Timer > trash4Wait)
        {
            trash4Wait += (0.2f - trash4Wait) / (spawnBuffer / trash4Wait);
            trash4Timer = 0.0f;
            //CreateTrash();
        }
        coinTimer += Time.deltaTime;
        if (coinTimer > actualCoinWait)
        {
            coinWait += (0.2f - coinWait) / (spawnBuffer / coinWait);
            actualCoinWait = coinWait += Random.Range(-0.2f, 0.5f);
            coinTimer = 0.0f;
            CreateCoin();
        }
    }

    private void CheckScore()
    {
        scoreTimer += Time.deltaTime;
        if (scoreTimer > scoreWait)
        {
            if (scoreWait > 0.01)
            {
                scoreWait += (0.01f - scoreWait) / (100/scoreWait);
            } else
            {
                scoreWait = 0.01f;
            }
            scoreTimer = 0;
            score += 1;
        }
    }

    private void CreateTrash()
    {
        Instantiate(Trash[Random.Range(0, Trash.Length)]);
        //Instantiate(Trash[0]);
    }

    private void CreateCoin()
    {
        Instantiate(coin);
    }

    public void doubleCoins()
    {
        SaveCoins(LoadCoins() + coinsInGame + coinsInGame);
        coinsText.text = "Total Coins: " + LoadCoins();
        doubledCoins.text = "Coins Gained: " + (coinsInGame * 3);
    }

    [System.Serializable]
    class SaveData
    {
        public int highScore;
        public int coins;
        public int fish;
        public int selectedFish;
    }

    // ABSTRACTION
    public void SaveBestScore(int bestScore)
    {
        SaveData data = new SaveData();
        data.highScore = bestScore;
        data.coins = LoadCoins();
        data.fish = LoadFish();
        data.selectedFish = LoadSelectedFish();

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void SaveCoins(int coins)
    {
        SaveData data = new SaveData();
        data.highScore = LoadBestScore();
        data.coins = coins;
        data.fish = LoadFish();
        data.selectedFish = LoadSelectedFish();

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
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

    public int LoadCoins()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            return data.coins;
        }
        else
        {
            return 0;
        }
    }

    public int LoadFish()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            return data.fish;
        }
        else
        {
            return 1;
        }
    }

    public int LoadSelectedFish()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            return data.selectedFish;
        }
        else
        {
            return 1;
        }
    }
}
