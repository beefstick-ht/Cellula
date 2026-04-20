using UnityEngine;
using System.Collections.Generic;
using System.ComponentModel.Design;

[CreateAssetMenu(fileName = "PlayerManager", menuName = "Scriptable Objects/PlayerManager")]
public class PlayerManager : ScriptableObject
{
    public PlayerStats PlayerStats {  get; private set; }
    public GameState GameState { get; set; }

    //tag of transforms that are locations where the player can spawn
    public string spawnTag;

    private void OnEnable()
    {
        LevelEvents.levelLoaded += SpawnPlayer;

        //dupliate on start so values do not change in the editor
        PlayerStats = Instantiate(startingPlayerStats);
    }

    protected void SpawnPlayer(Transform spawnTransform)
    {
        if(GameState.playerSpawnLocation != "")
        {
            GameObject[] spawns = GameObject.FindGameObjectsWithTag(spawnTag);
            bool foundSpawn = false;

            foreach(GameObject spawn in spawns)
            {
                //if matching spawn name
                if(spawn.name == GameState.playerSpawnLocation)
                {
                    foundSpawn = true;

                    //spawn location set, so spawn player there

                    ActivePlayer = Instantiate(playerPrefab, spawn.transform.position, Quaternion.identity);
                    break;
                }

            }
            if(!foundSpawn)
            {
                throw new MissingReferenceException("Could not find the player spawn location with this name " + GameState.playerSpawnLocation);
            }
        }
        else
        {
            //create instance of player prefab at default spawn location for level
            ActivePlayer = Instantiate(playerPrefab, defaultSpawnTransform.position, Quaternion.identity);
            Debug.Log("Player spawned at default location " + defaultSpawnTransform);
        }

        if (ActivePlayer)
        {
            PlayerEvents.onPlayerSpawned.Invoke(ActivePlayer.transform);
        }
        else
        {
            throw new MissingReferenceException("No ActivePlayer in PlayerManager. May have failed to spawn.");
        }
        
    }

    private void OnDisable()
    {
        LevelEvents.levelLoaded -= SpawnPlayer;
    }
}
