using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    [SerializeField] private float speed;

    private float speedChange;
    private float speedMove;

    private float xDir;
    private float yDir;
    private float zDir;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        transform.position = GenerateSpawnPos();
        transform.Rotate(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180));
        speedMove = speed * gameManager.trashSpeed;
        speedChange = Random.Range(-2.0f, 2.0f);
        xDir = Random.Range(-speed * 4, speed * 4);
        yDir = Random.Range(-speed * 4, speed * 4);
        zDir = Random.Range(-speed * 4, speed * 4);
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
        transform.Rotate(xDir * Time.deltaTime, yDir * Time.deltaTime, zDir * Time.deltaTime);
        if (transform.position.z < -15)
        {
            Destroy(gameObject);
        }
    }
}
