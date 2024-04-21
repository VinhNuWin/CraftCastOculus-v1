using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataPersist : MonoBehaviour
{
    // private TextLog textLog;
    public static ItemDataPersist Instance { get; private set; }
    public Dictionary<string, List<Item>> ItemsByCraftID { get; private set; } = new Dictionary<string, List<Item>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // TextLog.Instance.Log("[IDP] Instantiated");
            LoadMockItems();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void LoadItemsForCraft(string craftId, List<Item> items)
    {
        if (!string.IsNullOrWhiteSpace(craftId) && items != null)
        {
            ItemsByCraftID[craftId] = items;
        }
    }

    public List<Item> GetItemsForCraft(string craftId)
    {
        return ItemsByCraftID.TryGetValue(craftId, out var items) ? items : new List<Item>();
    }

    //Mock Items
    public void LoadMockItems()
    {
        var demoItems = new List<Item>
        {
            new Item { Item_ID = "1", Craft_ID = "C001", Step_ID = "1", Item_Name = "Card Base", Quantity = "1", IsCompleted = false, Link_To_Purchase = "www.example.com", Item_Category = "Craft Supplies" },
            new Item { Item_ID = "2", Craft_ID = "C001", Step_ID = "2", Item_Name = "Water", Quantity = "1L", IsCompleted = false, Link_To_Purchase = "www.example.com", Item_Category = "Kitchen Supplies" },
            new Item { Item_ID = "3", Craft_ID = "C002", Step_ID = "6", Item_Name = "Noodles", Quantity = "200g", IsCompleted = false, Link_To_Purchase = "www.example.com", Item_Category = "Food" },
            new Item { Item_ID = "4", Craft_ID = "C002", Step_ID = "6", Item_Name = "Garlic", Quantity = "2 cloves", IsCompleted = false, Link_To_Purchase = "www.example.com", Item_Category = "Food" },
            // new Item { Item_ID = "5", Craft_ID = "C003", Step_ID = "8", Item_Name = "Salmon", Quantity = "1 lb", IsCompleted = false, Link_To_Purchase = "www.example.com", Item_Category = "Food" },
            // new Item { Item_ID = "6", Craft_ID = "C003", Step_ID = "8", Item_Name = "Sushi Rice", Quantity = "500g", IsCompleted = false, Link_To_Purchase = "www.example.com", Item_Category = "Food" },
            new Item { Item_ID = "7", Craft_ID = "C004", Step_ID = "10", Item_Name = "Kewpie Mayo", Quantity = "2 tbsp", IsCompleted = false, Link_To_Purchase = "www.example.com", Item_Category = "Food" },
            new Item { Item_ID = "8", Craft_ID = "C004", Step_ID = "11", Item_Name = "Nori Sheets", Quantity = "5", IsCompleted = false, Link_To_Purchase = "www.example.com", Item_Category = "Food" },
            new Item { Item_ID = "9", Craft_ID = "C005", Step_ID = "14", Item_Name = "Card Base", Quantity = "1", IsCompleted = false, Link_To_Purchase = "www.example.com", Item_Category = "Craft Supplies" },
            new Item { Item_ID = "10", Craft_ID = "C005", Step_ID = "15", Item_Name = "Water", Quantity = "1L", IsCompleted = false, Link_To_Purchase = "www.example.com", Item_Category = "Kitchen Supplies" },
            new Item { Item_ID = "11", Craft_ID = "C006", Step_ID = "18", Item_Name = "Noodles", Quantity = "200g", IsCompleted = false, Link_To_Purchase = "www.example.com", Item_Category = "Food" },
            new Item { Item_ID = "12", Craft_ID = "C006", Step_ID = "18", Item_Name = "Garlic", Quantity = "2 cloves", IsCompleted = false, Link_To_Purchase = "www.example.com", Item_Category = "Food" },
            new Item { Item_ID = "13", Craft_ID = "C007", Step_ID = "22", Item_Name = "Salmon", Quantity = "1 lb", IsCompleted = false, Link_To_Purchase = "www.example.com", Item_Category = "Food" },
            new Item { Item_ID = "14", Craft_ID = "C007", Step_ID = "24", Item_Name = "Sushi Rice", Quantity = "500g", IsCompleted = false, Link_To_Purchase = "www.example.com", Item_Category = "Food" },
            new Item { Item_ID = "15", Craft_ID = "C008", Step_ID = "15", Item_Name = "Kewpie Mayo", Quantity = "2 tbsp", IsCompleted = false, Link_To_Purchase = "www.example.com", Item_Category = "Food" },
            new Item { Item_ID = "16", Craft_ID = "C008", Step_ID = "16", Item_Name = "Nori Sheets", Quantity = "5", IsCompleted = false, Link_To_Purchase = "www.example.com", Item_Category = "Food" },
            // new Item { Item_ID = "17", Craft_ID = "C003", Step_ID = "1", Item_Name = "Steel Ruler", Quantity = "1", IsCompleted = false, Link_To_Purchase = "www.example.com", Item_Category = "Leatherworking Tools" },
            // new Item { Item_ID = "18", Craft_ID = "C003", Step_ID = "1", Item_Name = "Wax Pencil", Quantity = "1", IsCompleted = false, Link_To_Purchase = "www.example.com", Item_Category = "Leatherworking Tools" },
            new Item {
    Item_ID = "3",
    Craft_ID = "C003",
    Step_ID = "1",
    Item_Name = "Firm Tofu",
    Quantity = "16 oz",
    IsCompleted = false,
    Link_To_Purchase = "www.example.com",
    Item_Category = "Kitchen Supplies"
},
new Item {
    Item_ID = "4",
    Craft_ID = "C003",
    Step_ID = "2",
    Item_Name = "Green Onion",
    Quantity = "3 stalks",
    IsCompleted = false,
    Link_To_Purchase = "www.example.com",
    Item_Category = "Vegetables"
},
new Item {
    Item_ID = "5",
    Craft_ID = "C003",
    Step_ID = "5",
    Item_Name = "Cornstarch",
    Quantity = "6 tbsp",
    IsCompleted = false,
    Link_To_Purchase = "www.example.com",
    Item_Category = "Kitchen Supplies"
},
// Add similar entries for each tool and material mentioned in the tutorial

            // Add more items as needed
        };

        foreach (var item in demoItems)
        {
            if (!ItemsByCraftID.ContainsKey(item.Craft_ID))
            {
                ItemsByCraftID[item.Craft_ID] = new List<Item>();
            }
            ItemsByCraftID[item.Craft_ID].Add(item);
        }
    }
    public List<Item> GetItemsForStep(string craftId, string stepId)
    {
        if (ItemsByCraftID.TryGetValue(craftId, out var items))
        {
            return items.FindAll(item => item.Step_ID == stepId);
        }
        return new List<Item>(); // Return an empty list if no items found
    }

    public void AddOrUpdateItem(Item item)
    {
        TextLog.Instance.Log("[IDP] Adding Or Updating Item");
        if (!string.IsNullOrWhiteSpace(item.Craft_ID))
        {
            if (!ItemsByCraftID.ContainsKey(item.Craft_ID))
            {
                ItemsByCraftID[item.Craft_ID] = new List<Item>();
            }
            ItemsByCraftID[item.Craft_ID].Add(item);
        }
    }

}