using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    readonly float LEFTRIGHT = 7.90f;
    readonly float TOPBOTTOM = 4.4f;
    Vector2 speed = new Vector2(4, -4);
    public bool ballServed = false;
    public bool alive = true;
    public int playerLives = 3;
    public static int livesLost = 0;
    public AudioClip brick;
    public AudioClip clank;
    AudioSource AudioSourceComponent;

    //Start is called before the first frame update
    void Start()
    {
        alive = true;
        playerLives = 3;
        AudioSourceComponent = GetComponent<AudioSource>();
    }

    //Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space") && !ballServed) 
        {
            ballServed = true;
            StartCoroutine(Run());
        }

        if(SceneManager.GetActiveScene().name == "LevelTwo")
        {
            if(livesLost > 0 && livesLost < 10)
            {
                if(livesLost == 1)
                {
                    removeHeartFromScreen();
                }
                else if(livesLost == 2)
                {
                    removeHeartFromScreen();
                    removeHeartFromScreen();
                }
                else if(livesLost == 3)
                {
                    removeHeartFromScreen();
                    removeHeartFromScreen();
                    removeHeartFromScreen();
                }

                livesLost = 11;
            }
        }
    }

    //Check position each frame
    IEnumerator Run()
    {
        while(ballServed)
        {
            Vector3 delta = speed * Time.deltaTime;
            Vector3 newPos = transform.position + delta;

            if(newPos.x < -LEFTRIGHT)
            {
                newPos.x = -LEFTRIGHT;
                speed.x *= -1;
            }
            else if(newPos.x > LEFTRIGHT)
            {
                newPos.x = LEFTRIGHT;
                speed.x *= -1;
            }
            else if(newPos.y > TOPBOTTOM)
            {
                newPos.y = TOPBOTTOM;
                speed.y *= -1;
            }
            else if(newPos.y < -TOPBOTTOM)
            {
                playerLives = playerLives-1;
                removeHeartFromScreen();
                if(playerLives <= 0)
                {
                    alive = false;
                }
                else
                {
                    newPos.y = -TOPBOTTOM;
                    ballServed = false;
                    newPos.x = 0; 
                    newPos.y = 0;
                }
            }
        
            transform.position = newPos;
            yield return new WaitForEndOfFrame();
        }
    }

    //On collision
    void OnCollisionEnter2D(Collision2D col)
    {
        speed.y *= -1;
        if(col.gameObject.tag == "Brick")
        {
            AudioSourceComponent.PlayOneShot(brick, 0.7F);
            Destroy(col.gameObject);
        }
        else if(col.gameObject.tag == "Blue Brick")
        {
            AudioSourceComponent.PlayOneShot(clank, 0.7F);
            col.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/BrokenBlueBrick");
            col.gameObject.tag = "Brick";
        }
    }

    // remove heart from screen
    void removeHeartFromScreen()
    {
        livesLost++;
        GameObject heart2 = GameObject.Find("Heart2");        
        if(heart2 != null)
        {
            Destroy(heart2);
        }
        else
        {
            GameObject heart1 = GameObject.Find("Heart1");
            if(heart1 != null)
            {
                Destroy(heart1);
            }
            else
            {
                GameObject heart0 = GameObject.Find("Heart0");
                if(heart0 != null)
                {
                    Destroy(heart0);
                }
            }
        }
    }
}