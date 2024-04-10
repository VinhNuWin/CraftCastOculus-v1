using System.Collections.Generic;
using UnityEngine;

public class CraftUIManager : MonoBehaviour
{
    private TextLog textLog;
    public StepView stepView;
    public ItemUI itemUI;
    public CraftUI craftUI;
    private Craft currentCraft;
    private List<Step> currentCraftSteps;
    private List<Item> currentCraftItems;
    public GameObject stepPrefab;
    public Transform stepsContentPanel;
    public GameObject itemPrefab;
    public Transform itemsContentPanel;
    public static CraftUIManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // TextLog.Instance.Log("[CraftUIManager] Craft/Step/Item detailViewModel Instantiated");
            CraftDataPersist.Instance.OnCraftSelected += OnCraftSelected;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        CraftDataPersist.Instance.OnCraftSelected -= OnCraftSelected;
    }

    private void OnCraftSelected(Craft selectedCraft)
    {
        var currentCraft = selectedCraft;
        if (currentCraft != null)
        {
            // TextLog.Instance.Log($"[CraftUIManager] craft: {currentCraft.Craft_Name}");
            UpdateUIWithCraft(currentCraft);
        }
        else
        {
            TextLog.Instance.Log("[CraftUIManager] craft is null or empty");
        }

        var craftId = currentCraft.Craft_ID;
        //Fetch Steps for SelectedCraft and Update UI
        if (craftId != null)
        {
            TextLog.Instance.Log($"[CraftUIManager] StepsView called");
            stepView.DisplayStepsForCraft(craftId);
        }
        else
        {
            TextLog.Instance.Log("[CraftUIManager] Steps craftId is null");
        }

        //Fetch items for SelectedCraft and Update UI
        var items = ItemDataPersist.Instance.GetItemsForCraft(selectedCraft.Craft_ID);
        if (items != null && items.Count > 0)
        {
            TextLog.Instance.Log($"[CraftUIManager] Items count: {items.Count}");
            UpdateUIWithItems(items);
        }
        else
        {
            TextLog.Instance.Log("[CraftUIManager] Items is null or empty");
        }
    }



    // private void UpdateUIWithSteps(List<Step> steps)
    // {
    //     // TextLog.Instance.Log($"[CraftUIManager] UpdateStepsUI beginning");
    //     foreach (Transform child in stepsContentPanel)
    //     {
    //         Destroy(child.gameObject);
    //     }

    //     foreach (var step in steps)
    //     {
    //         // TextLog.Instance.Log($"[CraftUIManager]Setting up step with instruction: {step.Title}");
    //         var stepItem = Instantiate(stepPrefab, stepsContentPanel);
    //         var stepUIComponent = stepItem.GetComponent<StepUI>();
    //         stepUI.Setup(step);
    //     }
    //     // TextLog.Instance.Log("[CraftUIManager] UpdateStepsUI complete");
    // }

    private void UpdateUIWithItems(List<Item> items)
    {
        TextLog.Instance.Log("[CraftUIManager] Updating UI with items...");

        // Example: Iterate through items and log their names
        foreach (var item in items)
        {
            var materialItem = Instantiate(itemPrefab, itemsContentPanel);
            var itemUIComponent = materialItem.GetComponent<ItemUI>();
            if (itemUIComponent != null)
            {
                itemUIComponent.Setup(item); // Use the component attached to the instantiated prefab
                // TextLog.Instance.Log($"[CraftUIManager] Item: {item.Item_Name}, Quantity: {item.Quantity}");
            }
            else
            {
                TextLog.Instance.Log("[CraftUIManager] Failed to get ItemUI component on instantiated item prefab.");
            }
        }

        // TextLog.Instance.Log("[CraftUIManager] Items UI update complete.");
    }

    private void UpdateUIWithCraft(Craft craft)
    {
        if (craftUI != null)
        {
            TextLog.Instance.Log("[CraftUIManager] UpdateUIWithCraft called");
            craftUI.Setup(craft);
        }
        else
        {
            TextLog.Instance.Log("[CraftUIManager] craftUI is null");
        }
    }
}