using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpawner : MonoBehaviour
{

    GameObject gameManager;
    GameManager gameManagerScript;

    public GameObject[] objectsToDrop;
    public GameObject[] pickupsToDrop;

    public Transform objectParent;
    public Transform pickupParent;

    public float timeToDrop;
    float timer;

    public float dropRange;
    float spawnPlace;

    private int objectArrayStart;
    private int objectArrayStop;

    public float pickupChance;


    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();

        timer = timeToDrop;
    }


    void Update()
    {
        DropObjects();
        SwitchEnvironment();
    }


    void SwitchEnvironment()
    {
        if (gameManagerScript.switchHeight > gameManagerScript.score)
        {
            objectArrayStart = 0;
            objectArrayStop = 2;
        }
        else if (gameManagerScript.switchHeight <= gameManagerScript.score)
        {
            objectArrayStart = 2;
            objectArrayStop = 4;
        }
    }


    bool DropPickup()
    {
        if (Random.value < pickupChance)
        {
            return true;
        }
        return false;
    }



    void DropObjects() 
    {
        if (timer <= 0)
        {
            spawnPlace = Random.Range(-dropRange, dropRange);
            if (DropPickup())
            {
                Instantiate(pickupsToDrop[Random.Range(0, pickupsToDrop.Length)], new Vector3(spawnPlace, transform.position.y, transform.position.z), transform.rotation, pickupParent);
            }
            else if (!DropPickup())
            {
                Instantiate(objectsToDrop[Random.Range(objectArrayStart, objectArrayStop)], new Vector3(spawnPlace, transform.position.y, transform.position.z), transform.rotation, objectParent);
            }
            timer = timeToDrop;
        }

        if (timer > 0)
        {
            timer = timer - 1;
        }

    }


}
