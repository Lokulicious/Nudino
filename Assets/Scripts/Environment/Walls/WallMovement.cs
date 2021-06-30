using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    public GameObject resetPoint;

    private float wallHeight;
    public float speed;



    void Start()
    {
        wallHeight = GetComponent<BoxCollider2D>().size.y;
        rb = GetComponent<Rigidbody2D>();
        speed = 1;
/*        transform.position = new Vector3(transform.position.x, 8, transform.position.z);*/
    }


    private void Update()
    {
        WallReset();
    }

    void FixedUpdate()
    {

        transform.Translate(Vector3.down * speed * Time.deltaTime);
        /*rb.velocity = -transform.forward * speed * Time.deltaTime;*/
    }


    void WallReset()
    {
/*        if (transform.position.y <= 8 - (wallHeight * 14))
        {
            transform.position = new Vector3(transform.position.x, 8f, transform.position.z);
        }*/

        if (transform.position.y <= 8 - (wallHeight * 14))
        {
            transform.position += Vector3.up * (wallHeight * 14);
            GameObject gameManager = GameObject.Find("GameManager");
            GameManager gameManagerScript = gameManager.GetComponent<GameManager>();
            gameManagerScript.AddScore(1);
        }

    }

}
