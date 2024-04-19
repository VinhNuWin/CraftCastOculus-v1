using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Globalization;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class CraftView : MonoBehaviour
{
    private TextLog textLog;
    [SerializeField]
    private CraftViewModel craftViewModel; // Reference to the CraftViewModel
    [SerializeField]
    private GameObject craftPrefab; // The prefab for each craft item
    [SerializeField]
    private Transform craftListContentPanel; // The parent panel for craft items
    [SerializeField]
    private List<GameObject> prefabTemplates; // List to hold instantiated prefab GameObjects
    // private Dictionary<string, GameObject> instantiatedPrefabs;

    private void Awake()
    {
        if (craftViewModel == null)
        {
            craftViewModel = FindObjectOfType<CraftViewModel>();
            TextLog.Instance.Log("[CV] CraftViewModel was null and found via FindObjectOfType.");
        }

        if (craftViewModel != null)
        {
            craftViewModel.OnCraftsFetched += UpdateCraftFeedUI;
            TextLog.Instance.Log("[CV] Subscribed to OnCraftsFetched.");
        }
        else
        {
            TextLog.Instance.Log("[CV] Failed to find CraftViewModel.");
        }
    }

    private void OnDestroy()
    {
        // Ensure to unsubscribe from the OnCraftsFetched event
        if (craftViewModel != null)
        {
            craftViewModel.OnCraftsFetched -= UpdateCraftFeedUI;
        }
    }

    public void UpdateCraftFeedUI(List<Craft> crafts)
    {
        // Clear existing crafts before updating
        ClearExistingCrafts();

        // Limit the number of crafts to display to the smaller of the number of crafts or prefabs
        int displayLimit = Mathf.Min(crafts.Count, prefabTemplates.Count);

        for (int i = 0; i < displayLimit; i++)
        {
            string prefabName = i == 0 ? "RoundedBoxToggle" : $"RoundedBoxToggle ({i})";
            // GameObject prefab = prefabTemplates[i];
            GameObject prefab = prefabTemplates.FirstOrDefault(p => p.name == prefabName);

            if (prefab != null)
            {
                prefab.SetActive(true);
                CraftUI craftUI = prefab.GetComponent<CraftUI>();

                if (craftUI != null)
                {
                    // TextLog.Instance.Log($"[UpdateCraftFeedUI] Setting up craft UI for {crafts[i].Craft_Name}.");
                    craftUI.Setup(crafts[i]);
                }
                else
                {
                    // TextLog.Instance.Log($"[UpdateCraftFeedUI] CraftUI component not found on prefab at index {i}.");
                }
            }

            // Deactivate any remaining prefabs
            for (int j = displayLimit; j < prefabTemplates.Count; j++)
            {
                prefabTemplates[j].SetActive(false);
            }

            // TextLog.Instance.Log("[UpdateCraftFeedUI] Craft feed UI update complete.");
        }
    }


    // Clear existing crafts from the content panel
    private void ClearExistingCrafts()
    {
        foreach (var prefab in prefabTemplates)
        {
            prefab.SetActive(false); // Deactivate instead of destroying for reuse
        }
    }
}
