using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class Craft
{
    [JsonProperty("Craft_ID")]
    public string Craft_ID { get; set; }

    [JsonProperty("Craft_Details")]
    public string Craft_Details { get; set; }

    [JsonProperty("User_ID")]
    public string User_ID { get; set; }

    [JsonProperty("Likes")]
    public int Likes { get; set; }

    [JsonProperty("Shares")]
    public int Shares { get; set; }

    [JsonProperty("Is_Saved")]
    public bool Is_Saved { get; set; }

    [JsonProperty("Craft_Author")]
    public string Craft_Author { get; set; }

    [JsonProperty("Craft_Name")]
    public string Craft_Name { get; set; }

    [JsonProperty("Craft_Image")]
    public string Craft_Image { get; set; }

    [JsonProperty("Author_Image")]
    public string Author_Image { get; set; }

    [JsonProperty("Category")]
    public string Category { get; set; }

    [JsonProperty("Post_Date")]
    public DateTime Post_Date { get; set; }
}

[System.Serializable]
public class CraftList
{
    public List<Craft> data;
}

[System.Serializable]
public class Step
{
    public string Step_ID { get; set; }
    public string Craft_ID { get; set; }
    public int Step_Order { get; set; }
    public string Step_Video { get; set; }
    public string Title { get; set; }
    public string Step_Instruction { get; set; }
    public int Timer_Duration { get; set; }
}

[System.Serializable]
public class StepList
{
    public List<Step> data;
}

[System.Serializable]
public class Item
{
    public string Step_ID { get; set; }
    public string Item_ID { get; set; }
    public string Craft_ID { get; set; }
    public string Item_Name;
    public string Item_Image;
    public string Quantity;
    public string Item_Category;
    public string Link_To_Purchase;
    public bool isCompleted;
}

[System.Serializable]
public class ItemList
{
    public List<Item> items;
}

[System.Serializable]
public class Note
{
    public string Craft_ID { get; set; }
    public string User_ID { get; set; }
    public string Note_ID { get; set; }
    public string note;
}

[System.Serializable]
public class NoteList
{
    public List<Note> data;
}

public class Comment
{
    public string Comment_ID { get; set; }
    public string Craft_ID { get; set; }
    public string User_ID { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
}

[System.Serializable]
public class CommentList
{
    public List<Comment> data;
}

public class CraftMaterial
{
    public int Material_ID { get; set; }
    public string Craft_ID { get; set; }
    public string Material_Name { get; set; }
    public string Quantity { get; set; }
}

public class UserFollow
{
    public int Follow_ID { get; set; }
    public string Follower_ID { get; set; }
    public string Following_ID { get; set; }
    public DateTime Follow_Date { get; set; }
}

public class CraftCategory
{
    public int Category_ID { get; set; }
    public string Category_Name { get; set; }
}

public class CraftInCategory
{
    public string Craft_ID { get; set; }
    public int Category_ID { get; set; }
}
