using UnityEngine;
using UnityEngine.Video;

public class StepVideoPlayer : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    // Call this method with the video URL or path of the current step
    public void AssignAndPlayVideo(string videoPath)
    {
        if (!string.IsNullOrEmpty(videoPath))
        {
            // Stop the current video if one is playing
            if (videoPlayer.isPlaying)
            {
                videoPlayer.Stop();
            }

            // Assign the new video to the VideoPlayer
            videoPlayer.url = videoPath;
            videoPlayer.Prepare();

            videoPlayer.prepareCompleted += (source) =>
            {
                videoPlayer.Play();
            };
        }
        else
        {
            Debug.Log("No video for the current step.");
        }
    }

    // Optionally, methods to pause or stop the video if needed
    public void PauseVideo()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
        }
    }

    public void StopVideo()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
        }
    }
}
