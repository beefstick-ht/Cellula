using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    //Only works on parent objects, can also be attatched to UI components

    private static GameObject[] persistentObjects = new GameObject[3];
    public int objectIndex; //checks each slot by assigning numbers to the slots
    
    //all of the objects using this script will be using the same list of objects

    void Awake()
    {

        if (persistentObjects[objectIndex] == null)
        {
            persistentObjects[objectIndex] = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else if (persistentObjects[objectIndex] != null)
        {
            Destroy(gameObject);
        }
  
    }


}
