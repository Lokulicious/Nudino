using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpawner : MonoBehaviour
{

    public GameObject[] objectsToDrop;

    public float timeToDrop;
    float timer;

    public float dropRange;
    float spawnPlace;

    void Start()
    {
        timer = timeToDrop;
    }


    void Update()
    {
        DropObjects();
        

    }


    void DropObjects() 
    {
        if (timer <= 0)
        {
            spawnPlace = Random.Range(-dropRange, dropRange);
            Instantiate(objectsToDrop[Random.Range(0, objectsToDrop.Length)], new Vector3(spawnPlace, transform.position.y, transform.position.z), transform.rotation);
            timer = timeToDrop;
        }

        if (timer > 0)
        {
            timer = timer - 1;
        }

    }


}
