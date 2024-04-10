using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class ItemUI : MonoBehaviour
{
    private Item item;
    private TextLog textLog;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemQuantityText;

    public void Setup(Item item)
    {
        this.item = item;
        // TextLog.Instance.Log("ItemSetup assigning view for : " + this.item.Item_Name);

        itemNameText.text = this.item.Item_Name;
        itemQuantityText.text = this.item.Quantity;
    }
}