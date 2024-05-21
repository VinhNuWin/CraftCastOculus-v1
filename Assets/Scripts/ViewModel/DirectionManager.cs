using UnityEngine;
using System;
using System.IO;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using MyCraft.Models;

public class DirectionManager : MonoBehaviour
{
    private TextLog textLog;
    public TimerViewModel timerViewModel;
    private Craft currentCraft;
    private Step currentStep;
    private List<Step> currentStepList;
    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI itemListText;
    public VideoPlayer stepVideoPlayer;
    private Button nextStepButton;
    private Button previousStepButton;
    private int currentStepIndex = 0;
    public CanvasFader canvasFader;

    void Start()
    {
        // InitializeDirection();
        // Subscribe to the OnCraftSelected event
        CraftDataPersist.Instance.OnCraftSelected += HandleCraftSelected;

        // Initialize directions if the current scene is "CraftScene"
        if (SceneManager.GetActiveScene().name == "CraftScene")
        {
            InitializeDirection();
            // Start the canvas fade-in effect
            if (canvasFader != null)
            {
                canvasFader.StartFadeIn();
            }
        }
    }

    void OnDisable()
    {
        // Unsubscribe to prevent memory leak or calling on destroyed object
        CraftDataPersist.Instance.OnCraftSelected -= HandleCraftSelected;
    }

    void OnDestroy()
    {
        // Unsubscribe here as well to cover all bases
        CraftDataPersist.Instance.OnCraftSelected -= HandleCraftSelected;
    }

    private void HandleCraftSelected(Craft selectedCraft)
    {
        currentCraft = selectedCraft;
        currentStepList = StepDataPersist.Instance.GetStepsForCraft(currentCraft.Craft_ID);

        // Check again if the current scene is "CraftScene" when a craft is selected
        if (SceneManager.GetActiveScene().name == "CraftScene")
        {
            InitializeDirection();
        }
    }

    public void InitializeDirection()
    {
        DisplayCurrentStep();
        // currentCraft = CraftDataPersist.Instance?.SelectedCraft;
        // if (currentCraft != null)
        // {
        //     currentStepList = StepDataPersist.Instance.GetStepsForCraft(currentCraft.Craft_ID);
        //     if (currentStepList.Count > 0)
        //     {
        //         DisplayCurrentStep();
        //     }
        // }
    }

    private void DisplayCurrentStep()
    {
        if (currentStepList != null && currentStepIndex < currentStepList.Count)
        {
            currentStep = currentStepList[currentStepIndex];
            instructionText.text = currentStep.Step_Instruction;
            titleText.text = currentStep.Title;
            TextLog.Instance.Log($"Displaying current step: {currentStep.Step_Instruction}");
            TextLog.Instance.Log($"CurrentStepIndex: {currentStepIndex}");
            TextLog.Instance.Log($"Current Timer_Duration: {currentStep.Timer_Duration}");

            // Fetch and display items for the current step
            var items = ItemDataPersist.Instance.GetItemsForStep(currentCraft.Craft_ID, currentStep.Step_ID);
            itemListText.text = "Items needed:\n" + string.Join("\n", items.ConvertAll(item => $"{item.Item_Name} - Quantity: {item.Quantity}"));

            // Manage video player
            if (!string.IsNullOrEmpty(currentStep.Video_URL))
            {
                // Determine the correct path based on the platform
                string videoPath;
                if (Application.platform == RuntimePlatform.Android)
                    videoPath = "jar:file://" + Application.dataPath + "!/assets/" + System.IO.Path.GetFileName(currentStep.Video_URL);
                else
                    videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, System.IO.Path.GetFileName(currentStep.Video_URL));

                stepVideoPlayer.source = VideoSource.Url;
                stepVideoPlayer.url = videoPath;
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
        TextLog.Instance.Log("Attempting to go to the next step. Current index: " + currentStepIndex + ", Total steps: " + currentStepList.Count);

        if (currentStepIndex < currentStepList.Count - 1)
        {
            currentStepIndex++;
            DisplayCurrentStep();

            TextLog.Instance.Log("Step " + currentStepIndex + " displayed. Checking timer duration: " + currentStepList[currentStepIndex].Timer_Duration);

            if (currentStepList[currentStepIndex].Timer_Duration > 0)
            {
                Vector3 timerPosition = CalculateTimerPosition();
                Quaternion timerRotation = Quaternion.identity;

                TextLog.Instance.Log("Calculated timer position and rotation for step " + currentStepIndex);

                timerViewModel.AddTimer(currentStepList[currentStepIndex].Timer_Duration, timerPosition, timerRotation, currentStepList[currentStepIndex].Step_ID);
                TextLog.Instance.Log("Timer added for step " + currentStepList[currentStepIndex].Step_ID);
            }
            else
            {
                TextLog.Instance.Log("No timer duration set for step " + currentStepIndex);
            }
        }
        else
        {
            TextLog.Instance.Log("Reached the end of the steps or there are no more steps.");
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
            SceneManager.LoadScene("HomeFeed"); // Adjust according to your scene setup
        }
    }

    private void HandleVideoError(VideoPlayer source, string message)
    {
        TextLog.Instance.Log("Video Error: " + message);
    }

    private Vector3 CalculateTimerPosition()
    {
        // Placeholder for example: Position the timer 3 units in front of the origin
        // You should replace this logic with the actual calculation based on your game's context
        return new Vector3(0, 1, .45f);  // Just a simple example position
    }

    // void AdjustButtonInteractivity()
    // {
    //     nextStepButton.interactable = currentStepIndex < currentStepList.Count - 1;
    //     previousStepButton.interactable = currentStepIndex > 0;
    // }
}

