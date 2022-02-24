using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//
[RequireComponent(typeof(NavMeshAgent))]

//code inspiration 
// - https://www.youtube.com/watch?v=LIn2jOyOTKQ
//main enemy movement code inspiration - https://www.youtube.com/watch?v=xppompv1DBg
// - https://www.youtube.com/watch?v=UjkSFoLxesw
//navmesh enemy wander - https://www.youtube.com/watch?v=jyMADpBEMFA


public class zombieController : MonoBehaviour
{

private Animator animator;
//how far the enemy can see.
public float zombieVisionRadius = 5f;
//movement speeds
public float speed;
public float wanderSpeed = 1f;
//how far the enemy will wander 
public float walkRadius;

Transform target;
NavMeshAgent agent;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //target the player to move towards
        //playerManger script to give instance of the player to target.
        target = playerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        
        //call to get random location on the nav mesh
        agent.SetDestination(RandomLocation());
        //animation states 
        animator.SetBool("isMoving", true);

    }

    // Update is called once per frame
    void Update()
        {
            //calculate the distance to the target (player/wandering)
        float distance = Vector3.Distance(target.position, transform.position);

        //if the player is within the look radius start moving towards them 
        if (distance <= zombieVisionRadius)
        {
            agent.speed = speed;
            //target the player
            agent.SetDestination(target.position);
            //animation if moving 
            animator.SetBool("isMoving", true);

        }
        else 
        { 
            //animation for not moving 
            //animator.SetBool("isMoving", false);
        }

        //if the player is outside of the look radius continue wandering randomly.
        if (distance > zombieVisionRadius)
        {
            if(agent.remainingDistance <= agent.stoppingDistance)
            {
                agent.speed = wanderSpeed;
                agent.SetDestination(RandomLocation());
                //animation moving state 
                animator.SetBool("isMoving", true);
            }
            
        }
    }
    //gets a random location on the nav mesh within the walk radius - link for code inspiration at top
    public Vector3 RandomLocation()
    {
        Vector3 finalPosition= Vector3.zero;
        //get random position inside the walk radius 
        Vector3 randomPosition = Random.insideUnitSphere * walkRadius;
        randomPosition += transform.position;
        //https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        //find the nearest point on the nvmesh within the range of walk radius. 
        if(NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, walkRadius, 1))
        {
            finalPosition = hit.position;
        }
        //return the final position to move to. 
        return finalPosition;
    }

}
