using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public bool isActivated;
    public bool generateSecondBackground = false;
    public GameObject loopBackground;
    public float startingHeight;

    private bool switchedSprites;

    int resetHeight = -16;
    private float BGHeight;

    public SpriteRenderer sr;
    public Sprite groundBG;
    public Sprite spaceBG;

    GameObject gameManager;
    GameManager gameManagerScript;

    private void Start()
    {
        /*        BGHeight = GetComponent<BoxCollider2D>().size.y;*/
        BGHeight = GetComponent<Renderer>().bounds.size.y;
        gameManager = GameObject.Find("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        
        /*        generateSecondBackground = false;*/
    }

    void Update()
    {
        if (generateSecondBackground && isActivated)
        {
            Instantiate(loopBackground, new Vector3(transform.position.x, transform.position.y + BGHeight, transform.position.z), transform.rotation);
            Debug.Log("instantiated second bg");
            generateSecondBackground = false;

        }
        if (transform.position.y <= resetHeight && isActivated)
        {
            transform.position += Vector3.up * (BGHeight * 2);
        }
        EnvironmentTransition();
    }


    void EnvironmentTransition()
    {
        gameManagerScript.SwitchSprite(sr, groundBG, spaceBG);
        if (gameManagerScript.switchHeight <= gameManagerScript.score)
        {
            isActivated = true;
        }
    }





/*    void ImageSwitch()
    {
        if (gameManagerScript.switchHeight < gameManagerScript.score)
        {
            sr.sprite = starsBG;
        }
        else if (gameManagerScript.switchHeight >= gameManagerScript.score)
        {
            sr.sprite = skyBG;
        }
    
    }*/

}
