using System;
using System.Collections.Generic;
using UnityEngine;
using MyCraft.Models;

public class CraftDataPersist : MonoBehaviour
{
    private TextLog textLog;
    public static CraftDataPersist Instance { get; private set; }
    public event Action<Craft> OnCraftSelected;
    public List<Craft> crafts { get; private set; } = new List<Craft>();
    public Dictionary<string, Craft> Crafts { get; private set; } = new Dictionary<string, Craft>();
    private Craft selectedCraft;

    public Craft SelectedCraft
    {
        get => selectedCraft;
        set
        {
            if (selectedCraft != value)
            {
                selectedCraft = value;
                Debug.Log($"[CDP] SelectedCraft updated with {selectedCraft.Craft_Name}");
                if (OnCraftSelected != null)
                {
                    OnCraftSelected?.Invoke(selectedCraft);
                }
                else
                {
                    Debug.Log("[CDP] No subscribers to OnCraftSelected when trying to invoke.");
                }
            }
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("[CDP] Instantiated");
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Method to add or update Crafts in the dictionary
    public void AddOrUpdateCraft(Craft craft)
    {
        if (craft != null && !string.IsNullOrWhiteSpace(craft.Craft_ID))
        {
            if (Crafts.ContainsKey(craft.Craft_ID))
            {
                Debug.Log("[CDP] Updating Craft");
                Crafts[craft.Craft_ID] = craft;
            }
            else
            {
                Debug.Log("[CDP] Adding New Craft");
                Crafts.Add(craft.Craft_ID, craft);
            }
        }
        else
        {
            Debug.Log("[CDP] Invalid craft or Craft_ID is null/empty, not added or updated.");
        }
    }


    public void LoadCrafts(IEnumerable<Craft> loadedCrafts)
    {
        foreach (var craft in loadedCrafts)
        {
            AddOrUpdateCraft(craft);
        }
    }

    public void ProcessWebSocketData(Craft craft)
    {
        try
        {
            AddOrUpdateCraft(craft);  // Assumes this method updates the craft dictionary but does not trigger the event
            ProcessStepsData(craft.Steps);
            ProcessItemsData(craft.Items);

            // Trigger the OnCraftSelected event only after all data is processed
            if (OnCraftSelected != null)
            {
                // Assigns selectedCraft and Invokes OnCraftSelected
                SelectedCraft = craft;
            }
            Debug.Log($"Processed and stored craft from WebSocket: {craft.Craft_Name}");
        }
        catch (Exception ex)
        {
            Debug.Log($"Error processing WebSocket data: {ex.Message}");
        }
    }

    private void ProcessStepsData(Step[] steps)
    {
        foreach (Step step in steps)
        {
            StepDataPersist.Instance.AddOrUpdateStep(step);
        }
    }
    private void ProcessItemsData(Item[] items)
    {
        foreach (Item item in items)
        {
            ItemDataPersist.Instance.AddOrUpdateItem(item);
        }
    }
}