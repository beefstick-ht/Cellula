using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    public int state;
    Transform[] waypoints;
    int currentWaypoint;
    public Transform player;

    NavMeshAgent harpie;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        harpie = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
   /* void Update()
    {
        switch (state)
        {
            case Roam:
                Roam();
                break;
            case Stalk:
                Stalk();
                break;
        }
    }*/
    public void Stalk()
    {
        harpie.SetDestination(player.position);
    }
    public void Roam()
    {

    }
}
