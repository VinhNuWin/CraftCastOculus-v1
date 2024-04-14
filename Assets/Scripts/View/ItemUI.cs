using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemUI : MonoBehaviour
{
    public Transform itemListContainer; // Parent container for item entries
    public TextMeshProUGUI itemTemplateText; // Template TextMeshPro element for items, set as inactive in the Editor

    private List<TextMeshProUGUI> activeItemTexts = new List<TextMeshProUGUI>();

    // Dynamically adds an item to the UI
    public void AddItem(Item item)
    {
        var itemTextClone = Instantiate(itemTemplateText, itemListContainer);
        itemTextClone.text = $"{item.Item_Name} - Quantity: {item.Quantity}";
        itemTextClone.gameObject.SetActive(true);
        activeItemTexts.Add(itemTextClone);
    }

    // Clears all items from the UI
    public void ClearItems()
    {
        foreach (var itemText in activeItemTexts)
        {
            Destroy(itemText.gameObject);
        }
        activeItemTexts.Clear();
    }
}



// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;
// using UnityEngine.UI;
// using System.Linq;

// public class ItemUI : MonoBehaviour
// {
//     private Item item;
//     private TextLog textLog;
//     public TextMeshProUGUI itemNameText;
//     public TextMeshProUGUI itemQuantityText;

//     public void Setup(Item item)
//     {
//         this.item = item;
//         // TextLog.Instance.Log("ItemSetup assigning view for : " + this.item.Item_Name);

//         itemNameText.text = this.item.Item_Name;
//         itemQuantityText.text = this.item.Quantity;
//     }
// }