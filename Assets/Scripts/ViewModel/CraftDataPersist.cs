using System;
using System.Collections.Generic;
using UnityEngine;

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
                TextLog.Instance.Log($"[CDP] SelectedCraft updated with {selectedCraft.Craft_Name}");
                if (OnCraftSelected != null)
                {
                    OnCraftSelected?.Invoke(selectedCraft);
                }
                else
                {
                    TextLog.Instance.Log("[CDP] No subscribers to OnCraftSelected when trying to invoke.");
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
            TextLog.Instance.Log("[CDP] Instantiated");
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Method to add or update Crafts in the dictionary
    public void AddOrUpdateCraft(Craft craft)
    {
        if (Crafts.ContainsKey(craft.Craft_ID))
        {
            Crafts[craft.Craft_ID] = craft;
        }
        else
        {
            Crafts.Add(craft.Craft_ID, craft);
        }
    }

    public void LoadCrafts(IEnumerable<Craft> loadedCrafts)
    {
        foreach (var craft in loadedCrafts)
        {
            AddOrUpdateCraft(craft);
        }
    }
}