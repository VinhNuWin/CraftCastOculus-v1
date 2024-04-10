using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerCreator : MonoBehaviour
{
    private TextLog textLog;
    public string videoFileName; // Name of the video file in the Assets folder

    void Start()
    {
        TextLog.Instance.Log("videoPlayer Called");
        // Add a VideoPlayer component to the GameObject this script is attached to
        VideoPlayer videoPlayer = gameObject.AddComponent<VideoPlayer>();

        // Set the video to play
        videoPlayer.source = VideoSource.VideoClip;

        // Set the video clip
        videoPlayer.clip = Resources.Load<VideoClip>(videoFileName);

        // Play on awake
        videoPlayer.playOnAwake = false;

        // Optionally, you can set other properties of the VideoPlayer here,
        // like whether it loops, its audio output mode, etc.

        // Play the video
        videoPlayer.Play();
    }
}