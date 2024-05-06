using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


[System.Serializable]
public class Item
{
    public string Item_ID;
    public string Craft_ID;
    public string Step_ID;
    public string Item_Name;
    public string Quantity;
    public bool IsCompleted;
    public string Link_To_Purchase;
    public string Item_Category;
    public string Item_Image;
}
