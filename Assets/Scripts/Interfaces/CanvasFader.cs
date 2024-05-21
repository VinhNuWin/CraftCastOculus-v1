using UnityEngine;
using DG.Tweening;
using System.Collections;

public class CanvasFader : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1.0f;
    public float delayDuration = 1.0f; // Delay before starting fade-in

    private void Start()
    {
        // Ensure the canvas is invisible at the start
        canvasGroup.alpha = 0;
    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeInWithDelay());
    }

    private IEnumerator FadeInWithDelay()
    {
        yield return new WaitForSeconds(delayDuration);
        canvasGroup.DOFade(1, fadeDuration);
    }
}
