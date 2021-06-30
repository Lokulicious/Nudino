using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int score;
    public int multiplier = 1;

    public Text scoreDisplay;

    void Start()
    {
        score = 0;   

    }



    void Update()
    {
        DisplayScore();
    }


    public void AddScore(int scoreAmount)
    {
        score += scoreAmount * multiplier;
        Debug.Log(score);
    }


    void DisplayScore()
    {
        scoreDisplay.text = score.ToString();
    }

}
