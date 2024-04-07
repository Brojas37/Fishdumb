using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private GameManager gameManager;

    [SerializeField] private float xSpeed;
    [SerializeField] private float ySpeed;

    [SerializeField] private float xPosRestraint;
    [SerializeField] private float yPosRestraint;
    [SerializeField] private float minYPosRestraint;

    [SerializeField] private float maxXControl;
    [SerializeField] private float minXControl;
    [SerializeField] private float maxYControl;
    [SerializeField] private float minYControl;

    [SerializeField] private float xChange;
    [SerializeField] private float yChange;
    [SerializeField] private float maxHold;

    [SerializeField] private Vector2 startPos;
    [SerializeField] private Vector2 endPos;

    //public GameObject big;
    //public GameObject small;

    private bool move;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //big.SetActive(false);
        //small.SetActive(false);
        xChange = 0;
        yChange = 0;
        move = true;
    }

    // Update is called once per frame
    void Update()
    {
        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical") / 2;

        if (move)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPosition = new Vector2(Camera.main.ScreenToWorldPoint(touch.position).x, Camera.main.ScreenToWorldPoint(touch.position).z);

                if (touch.phase == TouchPhase.Began)
                {
                    startPos = touch.position;
                    endPos = touch.position;
                    xChange = 0;
                    yChange = 0;
                    //big.SetActive(true);
                    //small.SetActive(true);
                    //big.transform.position = startPos;
                    //small.transform.position = endPos;

                    /*if (startPos.y > maxYControl)
                    {
                        endPos.y += maxYControl - startPos.y;
                        startPos.y += maxYControl - startPos.y;
                    }
                    if (startPos.y < minYControl)
                    {
                        endPos.y += minYControl - startPos.y;
                        startPos.y += minYControl - startPos.y;
                    }
                    if (startPos.x > maxXControl)
                    {
                        endPos.x += maxXControl - startPos.x;
                        startPos.x += maxXControl - startPos.x;
                    }
                    if (startPos.x < minXControl)
                    {
                        endPos.x += minXControl - startPos.x;
                        startPos.x += minXControl - startPos.x;
                    }*/
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    endPos = touch.position;

                    //if (Vector2.Distance(new Vector2(Camera.main.ScreenToWorldPoint(startPos).x, Camera.main.ScreenToWorldPoint(startPos).z), new Vector2(Camera.main.ScreenToWorldPoint(endPos).x, Camera.main.ScreenToWorldPoint(endPos).z)) > maxHold)
                    if (Vector2.Distance(startPos, endPos) > maxHold)
                    {
                        float distance = Vector2.Distance(startPos, endPos);

                        startPos.y = startPos.y + ((endPos.y - startPos.y) * (1 - (maxHold / distance)));
                        startPos.x = startPos.x + ((endPos.x - startPos.x) * (1 - (maxHold / distance)));

                        //endPos.y = endPos.y - ((endPos.y - startPos.y) * (1 - (maxHold / distance)));
                        //endPos.x = endPos.x - ((endPos.x - startPos.x) * (1 - (maxHold / distance)));
                    }

                    xChange = endPos.x - startPos.x;
                    yChange = endPos.y - startPos.y;
                    //big.transform.position = startPos;
                    //small.transform.position = endPos;
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    //big.SetActive(false);
                    //small.SetActive(false);
                    xChange = 0;
                    yChange = 0;
                }
            }

            float horizontalInput = xChange / maxHold;
            float verticalInput = yChange / maxHold;

            transform.Translate(Vector3.right * xSpeed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.forward * ySpeed * verticalInput * Time.deltaTime);

            if (transform.position.x < -xPosRestraint)
            {
                transform.position = new Vector3(-xPosRestraint, transform.position.y, transform.position.z);
            }
            if (transform.position.x > xPosRestraint)
            {
                transform.position = new Vector3(xPosRestraint, transform.position.y, transform.position.z);
            }

            if (transform.position.z < -minYPosRestraint)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -minYPosRestraint);
            }
            if (transform.position.z > yPosRestraint)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, yPosRestraint);
            }

            if (gameManager.gameOver)
            {
                //big.SetActive(false);
                //small.SetActive(false);
                gameObject.SetActive(false);
                move = false;
            }
        }

        if (!gameManager.gameOver)
        {
            if (!move)
            {
                xChange = 0;
                yChange = 0;
                move = true;
                transform.position = new Vector3(0, 0.5f, 0);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            
        }
        else
        {
            gameManager.GameOver();
            //big.SetActive(false);
            //small.SetActive(false);
            gameObject.SetActive(false);
            move = false;
        }
    }
}
