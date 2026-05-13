using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCWalk : MonoBehaviour
{
    public enum EnemyState 
    { 
        Roam = 0, 
        Stalk = 1, 
        Stunned = 2 
    }
    public EnemyState state;
    public Transform[] waypoints;
    int currentWaypoint;
    public Transform player;
    //ask joey how to find player if theyre instantiated into scene and not just there

    NavMeshAgent harpie;
    public float speed;
    public bool isStunned;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        harpie = GetComponent<NavMeshAgent>();
        state = EnemyState.Roam;
        isStunned = false;
    }


    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case EnemyState.Roam:
                Roam();
                break;
            case EnemyState.Stalk:
                Stalk();
                break;
            case EnemyState.Stunned:
                Stunned();
                break;
        }
    }


    public void Stalk()
    {
        harpie.SetDestination(player.position);
    }
    public void Roam()
    {
        harpie.SetDestination(waypoints[currentWaypoint].position);
        float distance = Vector3.Distance(transform.position, waypoints[currentWaypoint].position);
        if(distance < 1f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }
    public void Stunned()
    {
        if (isStunned == false)
        {

        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //play an alert animation
            state = EnemyState.Stalk;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {

            state = EnemyState.Roam;
        }
    }    
    public void RegisterStun()
    {
        state = EnemyState.Stunned;
    }

}
