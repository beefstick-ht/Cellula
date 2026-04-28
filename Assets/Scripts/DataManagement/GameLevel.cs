using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLevel : MonoBehaviour
{
    public Transform defaultPlayerSpawn;

    //start the game level
    private void Start()
    {
        // find the fade canvas if it persisted
        FadeController fader = FindFirstObjectByType<FadeController>();

        if (fader != null)
        {
            // fade out from black when new scene loads
            fader.FadeOut(() =>
            {
                Destroy(fader.gameObject);
            });
        }

        LevelEvents.levelLoaded?.Invoke(defaultPlayerSpawn);
    }
}
