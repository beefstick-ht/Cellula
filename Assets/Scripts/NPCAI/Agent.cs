using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    public enum EnemyState 
    { 
        Roam = 0, 
        Stalk = 1, 
        Stunned = 2,
        Attack = 3
    }
    public EnemyState state;
    public Transform[] waypoints;
    int currentWaypoint;
    public Transform player;
    //ask joey how to find player if theyre instantiated into scene and not just there

    NavMeshAgent harpie;
    public float speed;
    public bool isStunned;
    public bool isAttacking;
   
    
    public Animator anim;

    [Header("Death UI")]
    public Camera deathCam;

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
            case EnemyState.Attack:
                Attack();
                break;
        }
    }



    public void Roam()
    {
        float speedValue = harpie.speed;
        harpie.speed = 1.2f;
        harpie.SetDestination(waypoints[currentWaypoint].position);
        float distance = Vector3.Distance(transform.position, waypoints[currentWaypoint].position);
        if(distance < 1f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
        if (anim != null)
        {
            anim.SetFloat("Speed", speedValue);
        }
    }
    public void Stalk()
    {
        float speedValue = harpie.speed;
        harpie.speed = 3.5f;
        harpie.SetDestination(player.position);
        if(anim != null)
        {
            anim.SetFloat("Speed", speedValue);
        }
    }
    public void Stunned()
    {
        if (isStunned == false)
        {
            StartCoroutine(Stun());
        }
        if(anim != null)
        {
            anim.SetBool("Stunned", isStunned);
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

    public void Attack()
    {
        float playerDistance = Vector3.Distance(transform.position, player.position);
        if (playerDistance < 2f)
        {
            isAttacking = true;
            deathCam.gameObject.SetActive(true);
            anim.SetBool("Attack", isAttacking);
        }
        Debug.Log("YouDied!");
        
    }

    IEnumerator Stun()
    {
        Debug.Log("I'm stunned");
        isStunned = true;
        harpie.speed = 0f;
        //play the cool animation
        yield return new WaitForSeconds(5f);
        harpie.speed = 3.5f;
        
        isStunned = false;
        Collider[] hits = Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius);
        //search whole sphere if player's there
        bool chasePlayer = false;
        foreach (Collider hit in hits) 
        {
            if(hit.tag == "Player")
            {
                chasePlayer = true;
            }

        }
        if (chasePlayer == true)
        {
            state = EnemyState.Stalk;
        }
        else
        {
            state = EnemyState.Roam;
        }

    }
}
