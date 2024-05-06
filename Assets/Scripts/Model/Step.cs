using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

[System.Serializable]
public class Step
{
    public string Step_ID;

    public string Craft_ID;

    public int Step_Order;

    public string Title;

    public string Step_Instruction;

    public int Timer_Duration;

    public string Image_URL;

    public string Video_URL;
}
