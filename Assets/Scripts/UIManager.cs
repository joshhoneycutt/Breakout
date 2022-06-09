using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    GameObject[] pauseObjects;
    GameObject[] finishObjects;
    GameObject[] startObjects;
    GameObject[] winObjects;
    Ball ballController;

    // Start is called before the first frame update
    void Start()
    {
        GameObject ballGameObject = GameObject.Find("Ball");
        if(ballGameObject != null)
        {
            ballController = ballGameObject.GetComponentInChildren<Ball>();
        }

        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        finishObjects = GameObject.FindGameObjectsWithTag("ShowOnFinish");
        startObjects = GameObject.FindGameObjectsWithTag("ShowOnStart");
        winObjects = GameObject.FindGameObjectsWithTag("ShowOnWin");

		hidePaused();
        hideFinished();
        hideStart();
        hideWin();
    }

    // Update is called once per frame
    void Update()
    {
        if(ballController != null)
        {
            //uses the p button to pause and unpause the game
            if(Input.GetKeyDown(KeyCode.P))
            {
                if(Time.timeScale == 1 && ballController.alive == true)
                {
                    Time.timeScale = 0;
                    showPaused();
                } 
                else if (Time.timeScale == 0 && ballController.alive == true)
                {
                    Time.timeScale = 1;
                    hidePaused();
                }
            }

            //shows finish gameobjects if player is dead and timescale = 0
            if(ballController.alive == false)
            {
                showFinished();
            }

            if(ballController.ballServed == false)
            {
                showStart();
            }
            else
            {
                hideStart();
            }

            //toggle win screen
            GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
            GameObject[] blueBricks = GameObject.FindGameObjectsWithTag("Blue Brick");
            if(bricks.Length == 0 && blueBricks.Length == 0)
            {
                if(SceneManager.GetActiveScene().name == "LevelOne")
                {
                    SceneManager.LoadScene("LevelTwo");
                }
                else
                {
                    showWin();
                }
            }
        }
    }

    //controls the pausing of the scene
	public void pauseControl()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPaused();
        } 
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePaused();
        }
	}

	//shows objects with ShowOnPause tag
	public void showPaused()
    {
		foreach(GameObject element in pauseObjects)
        {
			element.SetActive(true);
		}
	}

	//hides objects with ShowOnPause tag
	public void hidePaused()
    {
		foreach(GameObject element in pauseObjects)
        {
			element.SetActive(false);
		}
	}

    //shows objects with ShowOnFinish tag
	public void showFinished()
    {
		foreach(GameObject element in finishObjects)
        {
			element.SetActive(true);
		}
	}

    //hides objects with ShowOnFinish tag
	public void hideFinished()
    {
		foreach(GameObject element in finishObjects)
        {
			element.SetActive(false);
		}
	}

    //shows objects with ShowOnStart tag
	public void showStart()
    {
		foreach(GameObject element in startObjects)
        {
			element.SetActive(true);
		}
	}

    //hides objetcs with ShowOnStart tag
    public void hideStart()
    {
		foreach(GameObject element in startObjects)
        {
			element.SetActive(false);
		}
	}

    //hide objects with ShowOnWin tag
    public void hideWin()
    {
		foreach(GameObject element in winObjects)
        {
			element.SetActive(false);
		}
	}

    //show objects with ShowOnWin tag
    public void showWin()
    {
        foreach(GameObject element in winObjects)
        {
            element.SetActive(true);
            Time.timeScale = 0;
            showPaused();
        }
    }

    //loads inputted level
	public void LoadLevel(string level)
    {
		Application.LoadLevel(level);
	}

    //exit game
    public void doExitGame() 
    {
        Application.Quit();
    }
}
