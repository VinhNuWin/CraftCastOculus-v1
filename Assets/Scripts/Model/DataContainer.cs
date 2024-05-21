using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyCraft.Models;



[System.Serializable]
public class CraftData
{
    public Craft craft;
    public List<Step> steps;
    public List<Item> items;
}