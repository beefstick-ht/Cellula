using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class DialoguePanelUI : MonoBehaviour
{
    // Make this a Singleton so any NPC can find it easily
    public static DialoguePanelUI instance;

    [Header("Components")]
    [SerializeField] private GameObject contentParent;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private void Awake()
    {
        if (instance == null) instance = this;

        contentParent.SetActive(false);
        dialogueText.text = "";
    }

    public void OpenPanel()
    {
        contentParent.SetActive(true);
    }

    public void ClosePanel()
    {
        contentParent.SetActive(false);
        dialogueText.text = "";
    }
    public void UpdateText(string line)
    {
        dialogueText.text = line;
    }
}