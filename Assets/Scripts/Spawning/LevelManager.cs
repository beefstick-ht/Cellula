using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Events;
using UnityEditor;

[CreateAssetMenu(fileName = "LevelManager", menuName = "Scriptable Objects/LevelManager", order = 1)]
public class LevelManager : ScriptableObject
{
    public GameState GameState { get; set; }

    private void OnEnable()
    {
        LevelEvents.levelExit += OnLevelExit;
    }

    //set the playerSpawnLocation in the game state for the next level and load the next level

    private void OnLevelExit(SceneAsset nextLevel, string playerSpawnTransformName)
    {
        GameState.playerSpawnLocation = playerSpawnTransformName;
        SceneManager.LoadScene(nextLevel.name, LoadSceneMode.Single);
    }

    private void OnDisable()
    {
        LevelEvents.levelExit -= OnLevelExit;
    }
}
