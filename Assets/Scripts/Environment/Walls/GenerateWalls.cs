using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GenerateWalls : MonoBehaviour
{

    public GameObject wall;
    public int amountOfWalls;
    private float wallHeight;
    public Transform parent;

    void Start()
    {
        wallHeight = wall.GetComponent<BoxCollider2D>().size.y;

        /*   distance = */

        for (int i = 0; i < amountOfWalls; i++)
        {
            Instantiate(wall, new Vector3(transform.position.x, i * wallHeight - 30, 0f), wall.transform.rotation, parent);
        }   
    }


    void Update()
    {
        
    }
}
