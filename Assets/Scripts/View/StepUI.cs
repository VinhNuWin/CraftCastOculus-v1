using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StepUI : MonoBehaviour
{
    public Transform stepListContainer; // Parent container for step entries
    public TextMeshProUGUI stepTemplateText; // Template TextMeshPro element for steps, set as inactive in the Editor

    private List<TextMeshProUGUI> activeStepTexts = new List<TextMeshProUGUI>();

    // Adds a step to the UI
    public void AddStep(Step step)
    {
        // Clone the template text element for a new step
        var stepTextClone = Instantiate(stepTemplateText, stepListContainer);
        stepTextClone.text = $"{step.Title}: {step.Step_Instruction}";
        stepTextClone.gameObject.SetActive(true); // Make sure the clone is active (stepTemplateText should be inactive)
        activeStepTexts.Add(stepTextClone); // Keep track of it for potential future removal or updates
    }

    // Clears all steps from the UI
    public void ClearSteps()
    {
        foreach (var stepText in activeStepTexts)
        {
            Destroy(stepText.gameObject); // Destroy the text GameObject
        }
        activeStepTexts.Clear(); // Clear the list
    }
}




// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;
// using UnityEngine.UI;
// using System.Linq;

// public class StepUI : MonoBehaviour
// {
//     private TextLog textLog;
//     private Step step;
//     public TextMeshProUGUI instructionText;
//     public TextMeshProUGUI stepOrderText;

//     public void Setup(Step step)
//     {
//         this.step = step;
//         TextLog.Instance.Log($"[StepSetup] Setting up step with instruction: {this.step.Step_Instruction}");

//         instructionText.text = this.step.Step_Instruction;
//         // stepOrderText.text = this.step.Step_Order.ToString();
//     }
// }