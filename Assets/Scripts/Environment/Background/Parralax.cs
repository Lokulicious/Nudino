using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour
{

    private float length, startpos;
    public GameObject player;
    public float parralaxEffect, wallSpeed;

    void Start()
    {
        startpos = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.y;
    }


    void FixedUpdate()
    {
        float dist = (wallSpeed * parralaxEffect);

/*        transform.position = new Vector3(transform.position.x, startpos + dist, transform.position.z);*/

        transform.Translate(Vector3.down * dist * Time.deltaTime);
    }
}
