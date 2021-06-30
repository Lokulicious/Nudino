using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{

    int resetHeight = -16;
    private float BGHeight;


    private void Start()
    {
        /*        BGHeight = GetComponent<BoxCollider2D>().size.y;*/
        BGHeight = GetComponent<Renderer>().bounds.size.y;
        Debug.Log(BGHeight);
    }

    void Update()
    {
        if (transform.position.y <= resetHeight)
        {
            transform.position += Vector3.up * (BGHeight * 2);
            Debug.Log("resetheight");
        }
    }
}
