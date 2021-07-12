using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayer : MonoBehaviour
{
    //rigidbody
    Rigidbody2D rb;

    //force
    public float forceX;
    public float forceY;
    Vector3 force;
    float slideSpeed = 0.5f;


    //wall checks
    bool isOnWall;
    bool hitWall;
    string wall;

    //level at which to jump
    public float jumpLevel;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector3(50, 100, 0));
    }


    void Update()
    {
        AutoJump();
        ResetForceOnWall();
    }


    void AutoJump()
    {
        //sets direction based on what wall player is on
        if (GetWall() == "left")
        {
            force = new Vector3(forceX, forceY, 0);
        }
        else if (GetWall() == "right")
        {
            force = new Vector3(-forceX, forceY, 0);
        }
        else
        {
            force = new Vector3(0, 0, 0);
        }

        //makes player jump at set interval
        if (transform.position.y <= jumpLevel)
        {
            rb.gravityScale = 0.8f;
            //adds force to player rigidbody
            rb.AddForce(force);
            isOnWall = false;
        }
    }


    string GetWall()
    {
        //checks what wall player is on
        if (isOnWall && transform.position.x > 0)
        {
            return "right";
        }
        else if (isOnWall && transform.position.x < 0)
        {
            return "left";
        }
        return null;
    }

    void ResetForceOnWall()
    {
        if (hitWall)
        {
            rb.velocity = new Vector2(0f, -1f - slideSpeed);


            hitWall = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //checks for collision with wall
        if (collision.gameObject.tag == "Wall")
        {
            hitWall = true;
            isOnWall = true;
            rb.gravityScale = 0;
        }
    }
}
