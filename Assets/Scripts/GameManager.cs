using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int score;
    public int multiplier = 1;

    public Text scoreDisplay;

    public int switchHeight;

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
/*        Debug.Log(score);*/
    }


    void DisplayScore()
    {
        scoreDisplay.text = score.ToString();
    }


    public void SwitchSprite(SpriteRenderer sr, Sprite sprite1, Sprite sprite2)
    {
        if (switchHeight > score)
        {
            sr.sprite = sprite1;
        }
        else if (switchHeight <= score)
        {
            sr.sprite = sprite2;
        }
    }


}
