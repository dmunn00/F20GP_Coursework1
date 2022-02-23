using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevel : MonoBehaviour
{
    //this script shows the end game screen when the player enters the end zone object.
    public GameObject levelCompleteUI;

    //called when the player enters the end area
    void OnTriggerEnter(Collider other)
    {
        //only the player can end the game... no zombies or other physics objects.
         if(other.name == "Player")
         {
            //sets the UI element to active so that it is dispalyed on the screen.
            levelCompleteUI.SetActive(true);
         }
        
    }
}
