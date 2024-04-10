using UnityEngine;
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
    private List<GameObject> preloadedGameObjectPrefabs; // List to hold instantiated prefab GameObjects

    private void Awake()
    {
        TextLog.Instance.Log("[CV] OnCraftsFetched subscribed.");
        // If craftViewModel is not set in the inspector, try to find it
        if (craftViewModel == null)
        {
            craftViewModel = FindObjectOfType<CraftViewModel>();
        }

        // Subscribe to the OnCraftsFetched event
        if (craftViewModel != null)
        {
            craftViewModel.OnCraftsFetched += UpdateCraftFeedUI;
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
        // ClearExistingCrafts();
        // TextLog.Instance.Log($"[UpdateCraftFeedUI] Cleared existing crafts.");

        // Limit the number of crafts to display to the smaller of the number of crafts or prefabs
        int displayLimit = Mathf.Min(crafts.Count, preloadedGameObjectPrefabs.Count);
        // TextLog.Instance.Log($"[UpdateCraftFeedUI] Displaying up to {displayLimit} crafts out of {crafts.Count} available.");

        for (int i = 0; i < displayLimit; i++)
        {
            // Activate the prefab and set up its data
            GameObject prefab = preloadedGameObjectPrefabs[i];
            prefab.SetActive(true);
            CraftUI craftUI = prefab.GetComponent<CraftUI>();
            if (craftUI != null)
            {
                // TextLog.Instance.Log($"[UpdateCraftFeedUI] Setting up craft UI for {crafts[i].Craft_Name}.");
                craftUI.Setup(crafts[i]);
            }
            else
            {
                TextLog.Instance.Log($"[UpdateCraftFeedUI] CraftUI component not found on prefab at index {i}.");
            }
        }

        // Deactivate any remaining prefabs
        // for (int i = displayLimit; i < preloadedGameObjectPrefabs.Count; i++)
        // {
        //     preloadedGameObjectPrefabs[i].SetActive(false);
        // }

        TextLog.Instance.Log("[UpdateCraftFeedUI] Craft feed UI update complete.");
    }


    // Clear existing crafts from the content panel
    private void ClearExistingCrafts()
    {
        foreach (var prefab in preloadedGameObjectPrefabs)
        {
            prefab.SetActive(false); // Deactivate instead of destroying for reuse
        }
    }
}


// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;
// using System;
// using System.Globalization;
// using System.Collections;
// using System.Linq;
// using System.Collections.Generic;

// public class CraftView : MonoBehaviour
// {
//     private TextLog textLog;
//     [SerializeField]
//     private CraftViewModel craftViewModel;
//     public GameObject craftPrefab;
//     public Transform craftListContentPanel;
//     public List<GameObject> preloadedGameObjectPrefabs = new List<GameObject>();
//     [SerializeField] private List<CraftUI> preloadedCraftUIComponents;
//     // Assign in the editor


//     private void Awake()
//     {
//         TextLog.Instance.Log("[CV] OnCraftsFetched subscribed");
//         craftViewModel.OnCraftsFetched += UpdateCraftFeedUI;
//         // If not set in the inspector, try to find it
//         if (craftViewModel == null)
//         {
//             craftViewModel = FindObjectOfType<CraftViewModel>();
//         }
//     }

//     private void OnDestroy()
//     {
//         craftViewModel.OnCraftsFetched -= UpdateCraftFeedUI;
//     }

//     public void ClearExistingCrafts(Transform targetPanel)
//     {
//         foreach (Transform child in targetPanel)
//         {
//             Destroy(child.gameObject);
//         }
//     }

//     //  List for Craft Feed

//     public void UpdateCraftFeedUI(List<Craft> crafts)
//     {
//         int displayLimit = Mathf.Min(crafts.Count, preloadedPrefabs.Count);
//         for (int i = 0; i < displayLimit; i++)
//         {
//             // Assume preloadedPrefabs is a List<GameObject> with your prefab instances
//             GameObject prefab = preloadedPrefabs[i];
//             prefab.SetActive(true); // Activate the prefab
//                                     // Assume each prefab has a CraftUI component to set up its data
//             CraftUI craftUI = prefab.GetComponent<CraftUI>();
//             if (craftUI != null)
//             {
//                 TextLog.Instance.Log($"[CV] Calling Setup for {crafts[i].Craft_Name}");
//                 craftUI.Setup(crafts[i]);
//             }
//         }

//         // Deactivate any remaining prefabs
//         for (int i = displayLimit; i < preloadedPrefabs.Count; i++)
//         {
//             preloadedPrefabs[i].SetActive(false);
//         }
//     }

// }
