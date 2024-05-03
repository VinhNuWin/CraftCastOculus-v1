using DG.Tweening;
using UnityEngine;

public class TweenUIEffect : MonoBehaviour
{
    public CanvasGroup targetElement;

    void Start()
    {
        // Ensure the target element is active and visible
        targetElement.gameObject.SetActive(true);
        targetElement.alpha = 0;

        // Apply a fade-in effect
        targetElement.DOFade(1, 0.5f).SetDelay(1f); // Delay of 1 second, fade over 0.5 seconds
    }
}