using UnityEngine;
using UnityEngine.Events;
using System;

//static events related to the player
public class PlayerEvents
{
    //allows classes to pass data without knowing anything about each other, just passes tranform into action invoke in order to react
    public static UnityAction<Transform> onPlayerSpawned;
    public static UnityAction onPlayerDespawned;

    public event Action onDisablePlayerMovement;

    public void DisablePlayerMovement()
    {
        if (onDisablePlayerMovement != null)
        {
            onDisablePlayerMovement();
        }
    }

    public event Action onEnablePlayerMovement;
    public void EnablePlayerMovement()
    {
        if (onEnablePlayerMovement != null)
        {
            onEnablePlayerMovement();
        }
    }

}
