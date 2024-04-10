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
    public VideoPlayer stepVideoPlayer;
    public Button nextStepButton;
    public Button previousStepButton;
    private int currentStepIndex = 0;

    void Start()
    {
        // string currentCraftId = "C001"; // Example craft ID, assumed to be set or retrieved appropriately
        InitializeDirection();
        // DisplayCurrentStep(); // Initially display the first step without starting the timer
    }

    void OnEnable()
    {
        // SceneManager.sceneLoaded += OnSceneLoaded;
        nextStepButton.onClick.AddListener(GoToNextStep);
        previousStepButton.onClick.AddListener(GoToPreviousStep);
    }

    void OnDestroy()
    {
        // SceneManager.sceneLoaded -= OnSceneLoaded;
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
                // AdjustButtonInteractivity();
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeDirection();
    }

    private void DisplayCurrentStep()
    {
        if (currentStepList != null && currentStepIndex < currentStepList.Count)
        {
            currentStep = currentStepList[currentStepIndex]; // Ensure currentStep is updated here
            instructionText.text = currentStep.Step_Instruction;
            // DisplayItemsForCurrentStep();

            // Assuming TextLog.Instance.Log is your custom logging method.
            // TextLog.Instance.Log($"Displaying currentStep: + {currentStep.Step_Instruction}");
            // TextLog.Instance.Log($"currentStepIndex: + {currentStepIndex}");

            // AdjustButtonInteractivity();
        }
    }

    // private void DisplayItemsForCurrentStep()
    // {
    //     // Fetch items for the current craft and current step
    //     var itemsForCurrentStep = ItemDataPersist.Instance.GetItemsForStep(currentCraft.Craft_ID, currentStep.Step_ID);

    //     // Assuming you have a method to update your UI with these items
    //     UpdateUIWithItems(itemsForCurrentStep);
    // }

    // private void UpdateUIWithItems(List<Item> items)
    // {
    //     // Update your UI elements here, e.g., a list or set of text fields
    //     // For example: foreach (var item in items) { /* Update UI */ }
    // }

    public void GoToNextStep()
    {
        if (currentStepIndex < currentStepList.Count - 1) // Check if there is a next step
        {
            if (currentStep.Timer_Duration > 0)
            {
                if (int.TryParse(currentStep.Step_ID, out int stepIdInt))
                {
                    TextLog.Instance.Log($"[DM] Duration found, Adding timer with duration of: {currentStep.Timer_Duration} and ID {stepIdInt}");
                    timerViewModel.AddTimer(currentStep.Timer_Duration, stepIdInt);
                    currentStepIndex++;
                    currentStep = currentStepList[currentStepIndex];
                    DisplayCurrentStep();
                }
                else
                {
                    TextLog.Instance.Log("[DM] No Duration Found, moving to next step");
                    currentStepIndex++;
                    currentStep = currentStepList[currentStepIndex];
                    DisplayCurrentStep();
                }
            }
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
            SceneManager.LoadScene("CraftPlay");
        }
    }

    // void AdjustButtonInteractivity()
    // {
    //     nextStepButton.interactable = currentStepIndex < currentStepList.Count - 1;
    //     previousStepButton.interactable = currentStepIndex > 0;
    // }
}

