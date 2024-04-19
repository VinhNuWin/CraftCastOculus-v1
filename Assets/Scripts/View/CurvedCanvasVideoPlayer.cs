using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CurvedCanvasVideoPlayer : MonoBehaviour
{
    public Canvas curvedCanvas;
    public RenderTexture videoTexture;
    public VideoClip videoClip;

    void Start()
    {
        // Ensure there is only one VideoPlayer component per GameObject.
        VideoPlayer videoPlayer = gameObject.GetComponent<VideoPlayer>();
        if (videoPlayer == null)
        {
            videoPlayer = new GameObject("CurvedCanvasVideoPlayer").AddComponent<VideoPlayer>();
        }

        videoPlayer.transform.SetParent(curvedCanvas.transform, false);
        videoPlayer.clip = videoClip;
        videoPlayer.renderMode = VideoRenderMode.RenderTexture;
        videoPlayer.targetTexture = videoTexture;

        RawImage videoDisplay = new GameObject("VideoDisplay").AddComponent<RawImage>();
        videoDisplay.transform.SetParent(curvedCanvas.transform, false);
        RectTransform rectTransform = videoDisplay.gameObject.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.offsetMin = rectTransform.offsetMax = Vector2.zero;
        videoDisplay.texture = videoTexture;
    }
}
