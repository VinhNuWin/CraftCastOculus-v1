using System.Collections.Generic;
using UnityEngine;

public class CraftUIManager : MonoBehaviour
{
    private TextLog textLog;

    public StepUI stepUI; // Assuming this is a single UI element for displaying a step.
    public ItemUI itemUI; // Assuming this is a single UI element for displaying an item.
    public CraftUI craftUI; // UI component to display craft details.
    private Craft currentCraft;
    private List<Step> currentCraftSteps;
    private List<Item> currentCraftItems;
    // Removed prefab references since they are not needed now.
    public static CraftUIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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

    private void OnCraftSelected(Craft selectedCraft)
    {
        if (selectedCraft != null)
        {
            UpdateUIWithCraft(selectedCraft);
            var steps = StepDataPersist.Instance.GetStepsForCraft(selectedCraft.Craft_ID);
            UpdateUIWithSteps(steps); // Updated method to display steps without prefabs.
            var items = ItemDataPersist.Instance.GetItemsForCraft(selectedCraft.Craft_ID);
            UpdateUIWithItems(items); // Updated method to display items without prefabs.
        }
    }

    private void UpdateUIWithSteps(List<Step> steps)
    {
        stepUI.ClearSteps(); // Clears existing steps
        foreach (var step in steps)
        {
            stepUI.AddStep(step); // Adds each step dynamically
        }
    }

    private void UpdateUIWithItems(List<Item> items)
    {
        itemUI.ClearItems(); // Clears existing items
        foreach (var item in items)
        {
            itemUI.AddItem(item); // Adds each item dynamically
        }
    }


    private void UpdateUIWithCraft(Craft craft)
    {
        craftUI.Setup(craft); // No change needed here, assuming craftUI.Setup(craft) is already implemented correctly.
    }
}
