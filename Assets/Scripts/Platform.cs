using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class Platform : MonoBehaviour
{
    readonly float EDGE = 7.1f;
    public float PlayerSpeed = 20;
    Ball ballController;

    // Start is called before the first frame update
    void Start()
    {
        GameObject ballGameObject = GameObject.Find("Ball");
        if(ballGameObject != null)
        {
            ballController = ballGameObject.GetComponentInChildren<Ball>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(ballController != null)
        {
            if(ballController.ballServed)
            {
                Vector3 pos = transform.position;
                float delta = Input.GetAxis("Horizontal") * Time.deltaTime * PlayerSpeed;
                pos.x += delta;

                if(pos.x <- EDGE)
                {
                    pos.x = -EDGE;
                }
                else if(pos.x > EDGE)
                {
                    pos.x = EDGE;
                }

                transform.position = pos;
            }
        }
    }
}
