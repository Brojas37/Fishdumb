using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float speed;

    private float speedChange;
    private float speedMove;

    private float yDir;

    private GameManager gameManager;

    private int added;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        transform.position = GenerateSpawnPos();
        transform.Rotate(0, Random.Range(-180, 180), 0);
        speedMove = speed * gameManager.trashSpeed;
        speedChange = Random.Range(-2.0f, 2.0f);
        yDir = Random.Range(-speed * 4, speed * 4);

        added = 0;
    }

    // Update is called once per frame
    void Update()
    {
        speedMove = speed * gameManager.trashSpeed;
        Move();
        if (gameManager.gameOver)
        {
            Destroy(gameObject);
        }
    }

    public Vector3 GenerateSpawnPos()
    {
        float xPos = Random.Range(-6.0f, 6.0f);
        float towards = Random.Range(0.0f, 100.0f) / 100.0f;
        xPos += (gameManager.playerX - xPos) * towards;
        //Vector3 SpawnPos = new Vector3(Random.Range(-6.0f, 6.0f), 0.5f, 15.0f);
        Vector3 SpawnPos = new Vector3(xPos, 0.5f, 15.0f);
        return SpawnPos;
    }

    public void Move()
    {
        transform.position += new Vector3(0.0f, 0.0f, -speedMove) * Time.deltaTime;
        transform.Rotate(0, yDir * Time.deltaTime, 0);
        if (transform.position.z < -15)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fish")
        {
            if (added == 0)
            {
                added = 1;
                gameManager.addCoin();
                Debug.Log("From coin");
                Destroy(gameObject);
            }
        }
    }
}