using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLevel : MonoBehaviour
{
    public Transform defaultPlayerSpawn;

    //start the game level
    private void Start()
    {
        LevelEvents.levelLoaded.Invoke(defaultPlayerSpawn);
    }
}
