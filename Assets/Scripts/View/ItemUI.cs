using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Transform itemListContainer; // Parent container for item entries
    public TextMeshProUGUI itemTemplateText; // Template TextMeshPro element for items, set as inactive in the Editor

    private List<TextMeshProUGUI> activeItemTexts = new List<TextMeshProUGUI>();

    void Start()
    {
        SetupLayoutComponents();
    }

    private void SetupLayoutComponents()
    {
        // Ensure there is a Vertical Layout Group and Content Size Fitter
        var layoutGroup = itemListContainer.GetComponent<VerticalLayoutGroup>();
        if (layoutGroup == null)
        {
            layoutGroup = itemListContainer.gameObject.AddComponent<VerticalLayoutGroup>();
            layoutGroup.childForceExpandHeight = false;
        }

        var contentSizeFitter = itemListContainer.GetComponent<ContentSizeFitter>();
        if (contentSizeFitter == null)
        {
            contentSizeFitter = itemListContainer.gameObject.AddComponent<ContentSizeFitter>();
            contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        }
    }

    public void AddItem(Item item)
    {
        var itemTextClone = Instantiate(itemTemplateText, itemListContainer);
        itemTextClone.text = $"{item.Item_Name}  {item.Quantity}";
        itemTextClone.gameObject.SetActive(true);
        activeItemTexts.Add(itemTextClone);
    }

    public void ClearItems()
    {
        foreach (var itemText in activeItemTexts)
        {
            Destroy(itemText.gameObject);
        }
        activeItemTexts.Clear();
    }
}


// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;

// public class ItemUI : MonoBehaviour
// {
//     public Transform itemListContainer; // Parent container for item entries
//     public TextMeshProUGUI itemTemplateText; // Template TextMeshPro element for items, set as inactive in the Editor

//     private List<TextMeshProUGUI> activeItemTexts = new List<TextMeshProUGUI>();

//     // Dynamically adds an item to the UI
//     public void AddItem(Item item)
//     {
//         var itemTextClone = Instantiate(itemTemplateText, itemListContainer);
//         itemTextClone.text = $"{item.Item_Name}  {item.Quantity}";
//         itemTextClone.gameObject.SetActive(true);
//         activeItemTexts.Add(itemTextClone);
//     }

//     // Clears all items from the UI
//     public void ClearItems()
//     {
//         foreach (var itemText in activeItemTexts)
//         {
//             Destroy(itemText.gameObject);
//         }
//         activeItemTexts.Clear();
//     }
// }

