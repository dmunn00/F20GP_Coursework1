using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{

    //idea for this code taken form here - https://www.youtube.com/watch?v=xppompv1DBg
    //script creates an instance of the player for the zombies to target 

    public static playerManager instance;

    void Awake ()
    {
    //instance of the player
    instance = this;

    }

    public GameObject player;
    
}   
