using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SceneSwitcher : MonoBehaviour
{
    public void GoTutScene()
    {
        Debug.Log("Button Pressed! Attempting to load TutorialLevel...");
        SceneManager.LoadScene("TutorialLevel");
    }
}
