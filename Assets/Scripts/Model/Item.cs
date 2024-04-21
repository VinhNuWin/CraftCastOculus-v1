using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class Item
{
    [JsonProperty("Item_ID")]
    public string Item_ID { get; set; }

    [JsonProperty("Craft_ID")]
    public string Craft_ID { get; set; }

    [JsonProperty("Step_ID")]
    public string Step_ID { get; set; }

    [JsonProperty("Item_Name")]
    public string Item_Name { get; set; }

    [JsonProperty("Quantity")]
    public string Quantity { get; set; }

    [JsonProperty("isCompleted")]
    public bool IsCompleted { get; set; }  // Note: JSON property name should match the case sensitivity

    [JsonProperty("Link_To_Purchase")]
    public string Link_To_Purchase { get; set; }

    [JsonProperty("Item_Category")]
    public string Item_Category { get; set; }

    [JsonProperty("Item_Image")]
    public string Item_Image { get; set; }  // Assuming there might be an image URL
}
