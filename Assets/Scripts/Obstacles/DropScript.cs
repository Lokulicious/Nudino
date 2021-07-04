using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropScript : MonoBehaviour
{

    public Animator animator;
    public Collider2D collider;
    public Rigidbody2D rb;



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.gravityScale = 0;
            animator.SetBool("PlayerCollision", true);
        }
        else if (collision.gameObject.name == "BottomTrigger")
        {
            Destroy(gameObject);
        }
    }


}
    