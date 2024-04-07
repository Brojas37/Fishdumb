using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    [SerializeField]
    private Text YourScore;
    [SerializeField] private Text HighScore;

    private float highScore;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
    }

    public void StartGame()
    {
        UpdateScore();
        HighScore.text = "High Score: " + gameManager.LoadBestScore();
    }

    public void UpdateScore()
    {
        YourScore.text = "Your Score: " + gameManager.score;
    }
}
