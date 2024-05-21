using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; // Ensure this namespace is included for UI components
using MyCraft.Models;

public class StepUI : MonoBehaviour
{
    public Transform stepListContainer; // This should be the 'Content' inside the ScrollView
    public TextMeshProUGUI stepTemplateText; // Template TextMeshPro element for steps, set as inactive in the Editor

    private List<TextMeshProUGUI> activeStepTexts = new List<TextMeshProUGUI>();

    // Start
    void Start()
    {
        // Ensure the container has these components
        var layoutGroup = stepListContainer.GetComponent<VerticalLayoutGroup>();
        if (layoutGroup == null) stepListContainer.gameObject.AddComponent<VerticalLayoutGroup>();

        var contentSizeFitter = stepListContainer.GetComponent<ContentSizeFitter>();
        if (contentSizeFitter == null)
        {
            contentSizeFitter = stepListContainer.gameObject.AddComponent<ContentSizeFitter>();
            contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        }
    }

    // Adds a step to the UI
    public void AddStep(Step step)
    {
        var stepTextClone = Instantiate(stepTemplateText, stepListContainer);
        stepTextClone.text = $"{step.Title}: {step.Step_Instruction}";
        stepTextClone.gameObject.SetActive(true);
        activeStepTexts.Add(stepTextClone);
    }

    // Clears all steps from the UI
    public void ClearSteps()
    {
        foreach (var stepText in activeStepTexts)
        {
            Destroy(stepText.gameObject);
        }
        activeStepTexts.Clear();
    }
}
