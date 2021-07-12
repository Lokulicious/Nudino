using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    public GameObject startText;
    int timer;
    public int blinkSpeed;
    


    void Start()
    {
        timer = blinkSpeed;
    }

     void Update()
    {
        StartGameOnTap();
        BlinkMessage(blinkSpeed, startText);
    }


    void StartGameOnTap()
    {
        if (Input.touchCount > 0)
        {
            SceneManager.LoadScene(1);
        }
    }

    void BlinkMessage(int _blinkSpeed, GameObject blinkObject)
    {
        if (timer <= 0)
        {
            blinkObject.SetActive(!blinkObject.activeInHierarchy);
            timer = _blinkSpeed;
            Debug.Log("toggle");
        }
        timer--;
    }


}

