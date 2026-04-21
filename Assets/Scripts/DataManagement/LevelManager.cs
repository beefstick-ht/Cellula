using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LevelManager",
    menuName = "Scriptable Objects/LevelManager")]
public class LevelManager : ScriptableObject
{
    public GameState GameState { get; set; }

    private void OnEnable()
    {
        LevelEvents.levelExit += OnLevelExit;
    }

    private void OnLevelExit(string nextLevelName, string spawnName)
    {
        GameState.playerSpawnLocation = spawnName;
        SceneManager.LoadScene(nextLevelName, LoadSceneMode.Single);
    }

    private void OnDisable()
    {
        LevelEvents.levelExit -= OnLevelExit;
    }
}