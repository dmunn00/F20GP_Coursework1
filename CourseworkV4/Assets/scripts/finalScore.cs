using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class finalScore : MonoBehaviour
{
    //text variable and final score variable 
    private Text scoreText;
    private static int fScore;

    // Start is called before the first frame update
    void Start()
    {
        //set scoreText to be the value of the text component. 
        scoreText = GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //get the players score and set it to fScore 
        fScore = scoreBehaviour.getScore();
        //debugging
        Debug.Log(fScore);
        //set the score on the UI to display correclty 
        scoreText.text = "Final Score: " + fScore;
    }

    public static void updateScore()
    {
        
    }
}
