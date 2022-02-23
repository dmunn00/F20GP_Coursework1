using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// code inspiration - https://www.youtube.com/watch?v=-EIXQHxoicg

public class crystalScript : MonoBehaviour
{

    //method called on trigger with a crystal object and the player. 
     void OnTriggerEnter(Collider other)
    {
        //if the collision object is a player so zombies cant collide with them.
        if(other.name == "Player")
        {
            //calls add score method in scoreBehaviour script. 
            scoreBehaviour.AddScore();
            //destroys the game object that was involved in the collision.
            Destroy(gameObject);
        }
    }
}
