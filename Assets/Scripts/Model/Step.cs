using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

[System.Serializable]
public class Step
{
    [JsonProperty("Step_ID")]
    public string Step_ID { get; set; }

    [JsonProperty("Craft_ID")]
    public string Craft_ID { get; set; }

    [JsonProperty("Step_Order")]
    public int Step_Order { get; set; }

    [JsonProperty("Title")]
    public string Title { get; set; }

    [JsonProperty("Step_Instruction")]
    public string Step_Instruction { get; set; }

    [JsonProperty("Timer_Duration")]
    public int Timer_Duration { get; set; }

    [JsonProperty("Image_URL")]
    public string Image_URL { get; set; }

    [JsonProperty("Video_URL")]
    public string Video_URL { get; set; }  // Assuming there might be a video URL
}
