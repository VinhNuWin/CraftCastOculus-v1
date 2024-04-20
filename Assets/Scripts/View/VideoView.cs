using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;


public class VideoView : MonoBehaviour
{
    private TextLog textLog;
    public VideoPlayer videoPlayer; // Assign this in the inspector
    public RawImage videoDisplay;

    void Awake()
    {
        if (videoPlayer == null)
        {
            TextLog.Instance.Log("VideoPlayer not assigned in VideoManager.");
            return;
        }
        if (videoDisplay == null)
        {
            TextLog.Instance.Log("VideoDisplay (RawImage) not assigned in VideoManager.");
            return;
        }


        // Subscribe to the video end event
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    public void PlayVideo(VideoClip clip)
    {
        TextLog.Instance.Log("Attempting to play video clip");
        if (clip == null)
        {
            TextLog.Instance.Log("Video clip is null.");
            return;
        }

        videoPlayer.clip = clip;
        videoPlayer.gameObject.SetActive(true);
        videoDisplay.gameObject.SetActive(true); // Make the RawImage visible
        videoPlayer.Play();
    }

    public void PauseVideo()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
        }
    }

    private void OnVideoEnd(VideoPlayer source)
    {
        TextLog.Instance.Log("Video ended. Hiding VideoPlayer.");
        // Hide the video player when the video ends
        videoPlayer.gameObject.SetActive(false);
        videoDisplay.gameObject.SetActive(false);
    }

    public void HideVideoPlayer()
    {
        TextLog.Instance.Log("Hiding VideoPlayer.");
        videoPlayer.gameObject.SetActive(false);
        videoDisplay.gameObject.SetActive(false);
    }

    public void PlayVideoByUrl(string url)
    {
        if (string.IsNullOrEmpty(url))
        {
            TextLog.Instance.Log("Video URL is null or empty.");
            return;
        }

        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = url;
        videoPlayer.gameObject.SetActive(true);
        videoDisplay.gameObject.SetActive(true); // Make the RawImage visible
        videoPlayer.Prepare(); // Prepare the video (asynchronously loads the video)
        videoPlayer.prepareCompleted += (source) => videoPlayer.Play(); // Play the video once it's prepared
    }

}