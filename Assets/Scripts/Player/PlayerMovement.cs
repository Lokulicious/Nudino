using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpPower = 5f;
    public float maxDrag = 3f;
    public Rigidbody2D rb;
    public LineRenderer lr;

    Vector3 dragStartPos;
    Touch touch;

    bool hitWall = false;
    bool isOnWall = false;

    float slideSpeed = 0.5f;


    bool hasShield;
    int shields;

    bool dead;



    void Update()
    {
        if (Input.touchCount > 0 && isOnWall)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                DragStart();
            }

            if(touch.phase == TouchPhase.Moved)
            {
                Dragging();
            }

            if(touch.phase == TouchPhase.Ended)
            {
                DragRelease();
            }
        }

        ResetForceOnWall();
    }




    void DragStart()
    {
        dragStartPos = Camera.main.ScreenToWorldPoint(touch.position);
        dragStartPos.z = 0f;
        lr.positionCount = 1;
        lr.SetPosition(0, dragStartPos);
    }


    void Dragging()
    {
        Vector3 draggingPos = Camera.main.ScreenToWorldPoint(touch.position);
        draggingPos.z = 0f;
        lr.positionCount = 2;
        lr.SetPosition(1, draggingPos);
    }


    void DragRelease()
    {
        lr.positionCount = 0;

        Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(touch.position);
        dragReleasePos.z = 0f;

        Vector3 force = dragStartPos - dragReleasePos;
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * jumpPower;

        rb.AddForce(-clampedForce, ForceMode2D.Impulse);
        isOnWall = false;
        rb.gravityScale = 0.8f;
    }



    void ResetForceOnWall()
    {
        if (hitWall)
        {
            rb.velocity = new Vector2(0f, -1f - slideSpeed);


            hitWall = false;
        }


    }


    void PlayerHit()
    {
        //takes away 1 shield
        if (hasShield)
        {
            shields -= 1;
        }
        else
        {
            //sets player to dead if no shields
            dead = true;
            Debug.Log("Collison!");
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checks for collision with deadly objects (obstacles / out of bounds)
        if (collision.gameObject.tag == "Deadly")
        {
            PlayerHit();
            Destroy(collision);
        }
    }
}
