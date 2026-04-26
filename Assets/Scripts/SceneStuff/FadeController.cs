using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 0.5f;

    private void Awake()
    {
        transform.SetParent(null); // make it a root object before DontDestroyOnLoad
        Canvas canvas = gameObject.AddComponent<Canvas>();
        gameObject.AddComponent<CanvasScaler>();
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        // fade in (black screen appears)
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1f, fadeDuration).OnComplete(() =>
        {
            // once fully black, fade back out
            canvasGroup.DOFade(0f, fadeDuration).OnComplete(() =>
            {
                Destroy(gameObject); // clean up when done
            });
        });
    }

    public void FadeIn(System.Action onComplete)
    {
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1f, fadeDuration).OnComplete(() => onComplete?.Invoke());

    }

    public void FadeOut(System.Action onComplete)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.DOFade(0f, fadeDuration).OnComplete(() =>
        {
            onComplete?.Invoke();
            Destroy(gameObject);
        });
    }
}
