using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// code inpiration - https://www.youtube.com/watch?v=-EIXQHxoicg
// - https://www.studytonight.com/game-development-in-2D/update-ui-element-in-realtime-unity


public class scoreBehaviour : MonoBehaviour
{   
    //varibles for holding text and score values.
    private Text thisText;
    private static int score;
    //old code
    private static int finalScore;
    
    void Start()
    {
        //get the text fron the text object and store it in variable thisText to be updated later.
        thisText = GetComponent<Text>();
        //start score at 0
        score = 0;
    }
    
    void Update() 
    {
        //update the text to show the new calculated score on the UI.
        thisText.text = "Score: " + score;
    }
    
    //method to give other scripts access to score
    public static int getScore()
    {
        return score;
    }
    

    //method that can be called when zombies are hit to decrease score.
    public static void LoseScore()
    {
        //decrease score.
        score -= 50;
    }

    //method that can be called when crystals are collected to increase score.
    public static void AddScore()
    {
        //increase score.
        score += 100;
    }

}