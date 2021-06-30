using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
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
    public int shields;

    bool hasDash;
    public int dashes;

    bool isDead;


    public Text shieldDisplay;
    public Text dashDisplay;



    void Update()
    {
        if (Input.touchCount > 0 && isOnWall)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                DragStart();
            }

            if (touch.phase == TouchPhase.Moved)
            {
                Dragging();

                Vector3 _dragReleasePos = Camera.main.ScreenToWorldPoint(touch.position);
                Vector2 velocity = (dragStartPos - _dragReleasePos) * jumpPower;

                Vector2[] trajectory = Plot(rb, (Vector2)transform.position, velocity, 500);

                lr.positionCount = trajectory.Length;

                Vector3[] positions = new Vector3[trajectory.Length];
                for (int i = 0; i < trajectory.Length; i++)
                {
                    positions[i] = trajectory[i];
/*                    positions[i].z = -11;*/
                }
                lr.SetPositions(positions);
                lr.sortingLayerName = "UI";
            }

            if (touch.phase == TouchPhase.Ended)
            {
                DragRelease();
            }
        }

        ResetForceOnWall();
        PickupDisplay();
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
            isDead = true;
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

        if (collision.gameObject.tag == "Pickup")
        {
            //detects what kind of pickup player collided with
            if (collision.gameObject.name == "Shield")
            {
                //adds shield
                shields += 1;
            }
            else if (collision.gameObject.name == "Dash")
            {
                //adds dash
                dashes += 1;
            }
            else if (collision.gameObject.name == "2XMultiplier")
            {
                //sets multiplier to 2
                GameObject gameManager = GameObject.Find("GameManager");
                GameManager gameManagerScript = gameManager.GetComponent<GameManager>();
                gameManagerScript.multiplier = 2;
            }
            //destroys the collider of the pickup
            Destroy(collision);
            //destroys the pickup (Change when we have an animation)
            Destroy(collision.gameObject);
        }
    }

    void PickupDisplay()
    {
        shieldDisplay.text = shields.ToString();
        dashDisplay.text = dashes.ToString();
    }


    public Vector2[] Plot(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity, int steps)
    {
        Vector2[] results = new Vector2[steps];

        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Physics2D.gravity * rigidbody.gravityScale * timestep * timestep;

        float drag = 1f - timestep * rigidbody.drag;
        Vector2 moveStep = velocity * timestep;

        for (int i = 0; i < steps; i++)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            results[i] = pos;
        }

        return results;
    }

}
