using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class StepUI : MonoBehaviour
{
    private TextLog textLog;
    private Step step;
    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI stepOrderText;

    public void Setup(Step step)
    {
        this.step = step;
        TextLog.Instance.Log($"[StepSetup] Setting up step with instruction: {this.step.Step_Instruction}");

        instructionText.text = this.step.Step_Instruction;
        // stepOrderText.text = this.step.Step_Order.ToString();
    }
}