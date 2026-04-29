using UnityEngine;
using System.Collections.Generic;

public class KeyInventory : MonoBehaviour
{
    [SerializeField] private List<int> keyIds = new List<int>();

    public static KeyInventory Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        { 
            Instance = this;
        DontDestroyOnLoad(gameObject); //keeps inventory persistent across scenes
    }
    else
        {
            Destroy(gameObject); //ensures there's only one instance
        }
   }

    public void AddKey(Key key)
    {
        if (!keyIds.Contains(key.id))
        {
            keyIds.Add(key.id); //will add key to list if want to collect it
            Debug.Log($"Key added: {key.keyName} (ID: {key.id})");
            //update UI
        }
    }

    public bool HasKey(Key key) //will pass in the scriptable object which checks if theres a key
    {
        return keyIds.Contains(key.id);
    }
}
