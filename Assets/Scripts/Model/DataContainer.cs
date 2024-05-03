using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class CraftData
{
    public Craft Craft;        // Contains details about the craft itself
    public List<Step> Steps; // A list of steps associated with this craft
    public List<Item> Items; // A list of items required for the craft
}