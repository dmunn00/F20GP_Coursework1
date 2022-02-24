using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstaclePush : MonoBehaviour
{

//tutorial for code here - https://www.youtube.com/watch?v=3BOn2gs7z04&t=246s

//player physics for objects like balls and walls to apply force to them.

//fore magnitude = how hard the player will push
[SerializeField]
private float forceMagnitude;

    //when the player collides with a physics object 
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //get the rigid body of the object the player has collided with 
        Rigidbody rigidbody = hit.collider.attachedRigidbody; 

        //if the object the player has hit has have a rigidbody apply force 
        if (rigidbody != null)
        { 
            //apply force to the object - can be adjusted. 
            //calculate the direction based on where the object is and where the player is.
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            //so the obejct doesnt fly into the air
            forceDirection.y = 0;
            //normalize
            forceDirection.Normalize();
            //apply the force instantly in the direction of movement at a specif position on the object .
            //https://docs.unity3d.com/ScriptReference/Rigidbody.AddForceAtPosition.html 
            //https://docs.unity3d.com/ScriptReference/ForceMode.Impulse.html
            rigidbody.AddForceAtPosition(forceMagnitude * forceDirection, transform.position, ForceMode.Impulse);
        }
    }
}


