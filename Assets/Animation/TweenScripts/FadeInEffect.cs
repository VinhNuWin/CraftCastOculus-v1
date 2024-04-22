using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeInEffect : MonoBehaviour
{
    private TextLog textLog;
    public CanvasGroup uiElement; // Assign the CanvasGroup component in the inspector
    public float fadeInDuration = 1.0f; // Duration of the fade in seconds

    void Start()
    {
        // Start with the UI element fully transparent
        uiElement.alpha = 0;

        // Fade in the UI element
        // FadeInUIElement();
    }

    public void FadeInUIElement()
    {
        TextLog.Instance.Log("Fade In Element called");
        // DOTween ToAlpha method to change the alpha value over time
        uiElement.DOFade(1, fadeInDuration).SetEase(Ease.InOutQuad); // Adjust the easing as needed
    }
}
