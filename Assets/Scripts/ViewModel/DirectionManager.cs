using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class DirectionManager : MonoBehaviour
{
    private TextLog textLog;
    public TimerViewModel timerViewModel;
    public VideoView videoView;
    private Craft currentCraft;
    private Step currentStep;
    private List<Step> currentStepList;
    public TextMeshProUGUI instructionText;
    public VideoPlayer stepVideoPlayer; // Ensure this component is attached to a GameObject in the scene
    public Button nextStepButton;
    public Button previousStepButton;
    private int currentStepIndex = 0;

    void Start()
    {
        InitializeDirection();
    }

    void OnEnable()
    {
        nextStepButton.onClick.AddListener(GoToNextStep);
        previousStepButton.onClick.AddListener(GoToPreviousStep);
    }

    void OnDestroy()
    {
        nextStepButton.onClick.RemoveListener(GoToNextStep);
        previousStepButton.onClick.RemoveListener(GoToPreviousStep);
    }

    void InitializeDirection()
    {
        currentCraft = CraftDataPersist.Instance?.SelectedCraft;
        if (currentCraft != null)
        {
            currentStepList = StepDataPersist.Instance.GetStepsForCraft(currentCraft.Craft_ID);
            if (currentStepList.Count > 0)
            {
                DisplayCurrentStep();
            }
        }
    }

    private void DisplayCurrentStep()
    {
        if (currentStepList != null && currentStepIndex < currentStepList.Count)
        {
            currentStep = currentStepList[currentStepIndex];
            instructionText.text = currentStep.Step_Instruction;

            // Manage video player
            if (!string.IsNullOrEmpty(currentStep.Step_Video))
            {
                stepVideoPlayer.source = VideoSource.Url;
                stepVideoPlayer.url = currentStep.Step_Video;
                stepVideoPlayer.errorReceived += HandleVideoError;

                stepVideoPlayer.Prepare();

                stepVideoPlayer.prepareCompleted += (source) =>
                {
                    TextLog.Instance.Log("Video prepared successfully.");
                    stepVideoPlayer.Play();
                };
            }
            else
            {
                TextLog.Instance.Log("No video to play for this step.");
                stepVideoPlayer.Stop(); // Stop the video if there is no associated video
            }
        }
    }

    public void GoToNextStep()
    {
        if (currentStepIndex < currentStepList.Count - 1)
        {
            currentStepIndex++;
            DisplayCurrentStep();
        }
        else
        {
            TextLog.Instance.Log("You have reached the end of the steps.");
        }
    }

    public void GoToPreviousStep()
    {
        if (currentStepIndex > 0)
        {
            currentStepIndex--;
            DisplayCurrentStep();
        }
        else
        {
            SceneManager.LoadScene("CraftPlay"); // Adjust according to your scene setup
        }
    }

    private void HandleVideoError(VideoPlayer source, string message)
    {
        TextLog.Instance.Log("Video Error: " + message);
    }

}

// void AdjustButtonInteractivity()
// {
//     nextStepButton.interactable = currentStepIndex < currentStepList.Count - 1;
//     previousStepButton.interactable = currentStepIndex > 0;
// }

