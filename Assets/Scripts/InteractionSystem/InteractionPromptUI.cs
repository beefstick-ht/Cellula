using TMPro;
using UnityEngine;
using DG.Tweening;

public class InteractionPromptUI : MonoBehaviour
{
    private Camera mainCam;
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private TextMeshProUGUI promptText;
    //tweening stuff
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeTime = 0.3f;

    private void Start()
    {
        mainCam = Camera.main;
        canvasGroup.alpha = 0f;
        uiPanel.SetActive(false);
    }

    private void LateUpdate()
    {
        var rotation = mainCam.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }

    public bool isDisplayed = false;
    public void Setup(string prompt)
    {
        promptText.text = prompt;
        uiPanel.SetActive(true);
        isDisplayed = true;
        // kill any existing tween first so they don't overlap
        canvasGroup.DOKill();
        canvasGroup.DOFade(1f, fadeTime);

    }

    public void Close()
    {
        isDisplayed = false;
        canvasGroup.DOKill();
        canvasGroup.DOFade(0f, fadeTime).OnComplete(() =>
        {
            uiPanel.SetActive(false);
        });
    }
}
