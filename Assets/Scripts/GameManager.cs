using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int score;
    public int multiplier = 1;

    void Start()
    {
        score = 0;   
    }



    void Update()
    {

    }


    public void AddScore(int scoreAmount)
    {
        score += scoreAmount * multiplier;
        Debug.Log(score);
    }
}
