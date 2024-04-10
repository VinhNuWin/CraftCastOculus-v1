using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class StepView : MonoBehaviour
{
    private TextLog textLog;
    public static StepView Instance { get; private set; }
    public event Action<List<Step>> OnStepsFetchedComplete;
    public GameObject stepPrefab;
    public Transform stepsContentPanel;
    private List<Step> currentCraftSteps;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // TextLog.Instance.Log("[StepViewModel] has been assigned");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void HandleStepsUpdated(string craftId)
    {
        // React to the update, e.g., by re-displaying the steps for the current craft
        DisplayStepsForCraft(craftId);
    }

    public void DisplayStepsForCraft(string craftId)
    {
        TextLog.Instance.Log("[StepViewModel] DisplayStepsForCraft called ");
        // Clear previous items
        foreach (Transform child in stepsContentPanel)
        {
            Destroy(child.gameObject);
        }

        // Fetch items for the current step
        var currentCraftSteps = StepDataPersist.Instance.GetStepsForCraft(craftId);

        // Instantiate item prefabs and populate them with the fetched items data
        foreach (var step in currentCraftSteps)
        {
            var stepGO = Instantiate(stepPrefab, stepsContentPanel);
            // Assuming your stepPrefab has a script to set up the step's details
            stepGO.GetComponent<StepUI>().Setup(step);
            TextLog.Instance.Log("[StepViewModel] Prefab Instantiated");
        }
    }
}