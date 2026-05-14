using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ThankYou : MonoBehaviour
{
    public GameObject textbox;
    public GameObject dialogueBox;
    public GameObject background;

    public TextMeshProUGUI text;

    public bool isTriggered;
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            dialogueBox.SetActive(true);
            background.SetActive(true);
            textbox.SetActive(true);
            isTriggered = true;

            text.text = "Thanks for playing! This is the end for now! Made by Finn and Hayden :]";

            StartCoroutine(Quit());
        }

    }

    IEnumerator Quit()
    {
        yield return new WaitForSeconds(5f);
        Application.Quit();
    }
}
