using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{

    //script creates an instance of the player for the zombies to target 
    #region Singleton

    public static playerManager instance;

    void Awake ()
    {

    instance = this;

    }

    #endregion

    public GameObject player;
    
}   
