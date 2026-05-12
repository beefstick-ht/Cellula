using UnityEngine;
using static Agent;

public class RegisterStun : MonoBehaviour
{
    public Agent agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Flashlight")
        {
            agent.RegisterStun();
        }
    }
}
