using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public static GameManager instance{ get; private set; }

    [SerializeField]
    private GameState startingState;

    public GameState GameState { get; private set; }

    public LevelManager levelManager;
    public PlayerManager playerManager;
    public UIManager uiManager;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        GameState = Instantiate(startingState);
        levelManager.GameState = GameState;
        playerManager.GameState = GameState;
    }

}
