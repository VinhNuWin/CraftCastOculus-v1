using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public CanvasGroup videoPanel;
    public CanvasGroup instructionPanel;
    public CanvasGroup timerPanel;
    public CanvasGroup videoIcon;
    public VideoPlayer videoPlayer;
    public float fadeInDuration = 0.5f;
    public float staggerDelay = 0.5f;
    public Vector2 minimizedSize = new Vector2(100, 100); // Size of the video panel when minimized

    void Start()
    {
        // Start the staggered fade-in sequence
        FadeInPanels();

        // Subscribe to the video player's end event
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    private void FadeInPanels()
    {
        // videoPanel.alpha = 0;
        // videoPanel.gameObject.SetActive(true);
        // videoPanel.DOFade(1, fadeInDuration);

        // instructionPanel.alpha = 0;
        // instructionPanel.gameObject.SetActive(true);
        // instructionPanel.DOFade(1, fadeInDuration);

        // Fade in the video panel first
        videoPanel.alpha = 0;
        videoPanel.gameObject.SetActive(true);
        videoPanel.DOFade(1, fadeInDuration).OnComplete(() =>
        {
            // After the video panel fades in, fade in the instruction panel
            instructionPanel.alpha = 0;
            instructionPanel.gameObject.SetActive(true);
            instructionPanel.DOFade(1, fadeInDuration).SetDelay(staggerDelay);
        });
    }

    private void OnVideoFinished(VideoPlayer source)
    {
        // Minimize the video panel when the video finishes
        videoPanel.transform.DOScale(minimizedSize, fadeInDuration).OnComplete(() =>
        {
            videoPanel.gameObject.SetActive(false); // Optionally hide the video panel
        });
    }

    public void InstantiateTimerPanel()
    {
        // This method is called when the timer panel needs to be instantiated
        timerPanel.alpha = 0;
        timerPanel.gameObject.SetActive(true);
        timerPanel.transform.localScale = Vector3.zero; // Start from a scaled down state
        timerPanel.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack); // Pop out effect
        timerPanel.DOFade(1, 0.3f);
    }
}
