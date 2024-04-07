using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{

    private MusicController musicController;

    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject diedMenu;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private Text YourScore;
    [SerializeField] private Text HighScore;
    [SerializeField] private Text YourScore1;
    [SerializeField] private Text HighScore1;

    [SerializeField] RewardedAdsButton rewardedAdsButton;

    private GameManager gameManager;

    private bool watchedAd;

    private bool loadedAd;

    // Start is called before the first frame update
    void Start()
    {
        musicController = GameObject.Find("Music").GetComponent<MusicController>();
        gameOverMenu.SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        watchedAd = false;
        diedMenu.SetActive(false);
        gameUI.SetActive(true);

        loadedAd = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameOver)
        {
            gameUI.SetActive(false);
            if (!watchedAd)
            {
                diedMenu.SetActive(true);
                if (!loadedAd)
                {
                    rewardedAdsButton.LoadAd();
                    loadedAd = true;
                }
            } else
            {
                gameOverMenu.SetActive(true);
            }
            YourScore.text = "Your Score: " + gameManager.score;
            YourScore1.text = "Your Score: " + gameManager.score;
            HighScore.text = "High Score: " + gameManager.LoadBestScore();
            HighScore1.text = "High Score: " + gameManager.LoadBestScore();
        }
    }

    public void watchAd()
    {
        watchedAd = true;
        gameManager.doubleCoins();
        diedMenu.SetActive(false);
        gameOverMenu.SetActive(true);
    }

    public void ReturnToMenu()
    {
        musicController.Destroy();
        SceneManager.LoadScene(0);
    }

    public void Retry()
    {
        SceneManager.LoadScene(1);
    }
}
