using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Store : MonoBehaviour
{

    [SerializeField] private Text coinsAvailable;
    [SerializeField] private GameObject[] fishes;

    [SerializeField] private bool[] availableFishes = new bool[5];

    [SerializeField] private GameObject left;
    [SerializeField] private GameObject right;

    [SerializeField] private GameObject selectButton;
    [SerializeField] private GameObject selectedButton;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private Text buyText;

    private int currentFish;

    private int[] costs = new int[] { 0, 100, 300, 500, 1000 };

    // Start is called before the first frame update
    void OnEnable()
    {
        coinsAvailable.text = "Coins: " + LoadCoins();
        StartStore();
    }

    // Update is called once per frame
    void Update()
    {
        //resetData();
        //SaveCoins(5000);
    }

    public void StartStore()
    {
        PopulateFish();
        currentFish = LoadSelectedFish();
        LoadNewFish();
    }

    public void Right()
    {
        currentFish++;
        LoadNewFish();
    }

    public void Left()
    {
        currentFish--;
        LoadNewFish();
    }

    private void LoadNewFish()
    {
        for (int i = 0; i < fishes.Length; i++)
        {
            fishes[i].SetActive(false);
        }
        fishes[currentFish - 1].SetActive(true);

        if (currentFish > 1)
        {
            left.SetActive(true);
        }
        else
        {
            left.SetActive(false);
        }
        if (currentFish < 5)
        {
            right.SetActive(true);
        }
        else
        {
            right.SetActive(false);
        }

        if (currentFish == LoadSelectedFish())
        {
            selectedButton.SetActive(true);
            selectButton.SetActive(false);
            buyButton.SetActive(false);
        } else if (availableFishes[currentFish - 1] == true)
        {
            selectedButton.SetActive(false);
            selectButton.SetActive(true);
            buyButton.SetActive(false);
        } else
        {
            selectedButton.SetActive(false);
            selectButton.SetActive(false);
            buyButton.SetActive(true);
            buyText.text = costs[currentFish - 1] + " Coins";
        }
    }

    public void SelectFish()
    {
        SaveLoadedFish(currentFish);
        LoadNewFish();
    }

    public void BuyFish()
    {
        if (LoadCoins() >= costs[currentFish - 1])
        {
            availableFishes[currentFish - 1] = true;
            SaveFish(availableFishes);
            SaveCoins(LoadCoins() - costs[currentFish - 1]);
            coinsAvailable.text = "Coins: " + LoadCoins();
            LoadNewFish();
        }
    }

    private void PopulateFish()
    {
        int fishNum = LoadFish();
        for (int i = 0; i < availableFishes.Length; i++)
        {
            availableFishes[i] = false;
        }
        if (fishNum >= 16)
        {
            availableFishes[4] = true;
            fishNum -= 16;
        }
        if (fishNum >= 8)
        {
            availableFishes[3] = true;
            fishNum -= 8;
        }
        if (fishNum >= 4)
        {
            availableFishes[2] = true;
            fishNum -= 4;
        }
        if (fishNum >= 2)
        {
            availableFishes[1] = true;
            fishNum -= 2;
        }
        if (fishNum >= 1)
        {
            availableFishes[0] = true;
            fishNum -= 1;
        }
    }

    public void ExitStore()
    {
        for (int i = 0; i < fishes.Length; i++)
        {
            fishes[i].SetActive(false);
        }
        gameObject.SetActive(false);
    }

    [System.Serializable]
    class SaveData
    {
        public int highScore;
        public int coins;
        public int fish;
        public int selectedFish;
    }

    public void resetData()
    {
        SaveData data = new SaveData();
        data.highScore = 0;
        data.coins = 0;
        data.fish = 1;
        data.selectedFish = 1;

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

    public void SaveLoadedFish(int fish)
    {
        SaveData data = new SaveData();
        data.highScore = LoadBestScore();
        data.coins = LoadCoins();
        data.fish = LoadFish();
        data.selectedFish = fish;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void SaveFish(bool[] fishes)
    {
        int fishNum = 0;
        if (fishes[0] == true)
        {
            fishNum += 1;
        }
        if (fishes[1] == true)
        {
            fishNum += 2;
        }
        if (fishes[2] == true)
        {
            fishNum += 4;
        }
        if (fishes[3] == true)
        {
            fishNum += 8;
        }
        if (fishes[4] == true)
        {
            fishNum += 16;
        }

        SaveData data = new SaveData();
        data.highScore = LoadBestScore();
        data.coins = LoadCoins();
        data.fish = fishNum;
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
}
