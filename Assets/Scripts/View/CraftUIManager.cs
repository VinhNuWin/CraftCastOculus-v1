using System.Collections.Generic;
using UnityEngine;

public class CraftUIManager : MonoBehaviour
{
    private TextLog textLog;
    public StepUI stepUI; // Assuming this is a single UI element for displaying a step.
    public ItemUI itemUI; // Assuming this is a single UI element for displaying an item.
    public CraftUI craftUI; // UI component to display craft details.
    private Craft currentCraft;
    public GameObject detailPanel;
    private List<Step> currentCraftSteps;
    private List<Item> currentCraftItems;
    public static CraftUIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            TextLog.Instance.Log($"[CraftUIManager] OnCraftSelected subscribed");
            CraftDataPersist.Instance.OnCraftSelected += OnCraftSelected;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        CraftDataPersist.Instance.OnCraftSelected -= OnCraftSelected;
    }

    private void HandleCraftSelected(Craft craft)
    {
        TextLog.Instance.Log($"Craft selected: {craft.Craft_Name}");

    }

    private void OnCraftSelected(Craft selectedCraft)
    {
        TextLog.Instance.Log("[CraftUIMan] OnCraftSelected - called");
        if (selectedCraft != null)
        {
            TextLog.Instance.Log($"[CraftUIMan] OnCraftSelected Updating UI Manager for {selectedCraft.Craft_ID}");
            UpdateUIWithCraft(selectedCraft);
            var steps = StepDataPersist.Instance.GetStepsForCraft(selectedCraft.Craft_ID);
            UpdateUIWithSteps(steps); // Updated method to display steps without prefabs.
            var items = ItemDataPersist.Instance.GetItemsForCraft(selectedCraft.Craft_ID);
            UpdateUIWithItems(items); // Updated method to display items without prefabs.
        }
        else
        {
            TextLog.Instance.Log("[CraftUIMan] OnCraftSelected - No craft selected or craft is null");
        }

        TextLog.Instance.Log("[CraftUIMan]Popup isOpen: true");
    }

    private void UpdateUIWithSteps(List<Step> steps)
    {
        stepUI.ClearSteps(); // Clears existing steps
        foreach (var step in steps)
        {
            TextLog.Instance.Log($"[UIM] Setting up step UI for {step.Step_Instruction}.");
            stepUI.AddStep(step); // Adds each step dynamically
        }
    }

    private void UpdateUIWithItems(List<Item> items)
    {
        itemUI.ClearItems(); // Clears existing items
        foreach (var item in items)
        {
            TextLog.Instance.Log($"[UIM] Setting up Item UI for {item.Item_Name}.");
            itemUI.AddItem(item); // Adds each item dynamically
        }
    }


    private void UpdateUIWithCraft(Craft craft)
    {
        TextLog.Instance.Log($"[CraftUIManager] UpdateUIWithCraft called");
        craftUI.Setup(craft); // No change needed here, assuming craftUI.Setup(craft) is already implemented correctly.
    }
}
