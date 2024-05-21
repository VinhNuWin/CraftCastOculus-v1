using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using MyCraft.Models;

namespace MyCraft.Models
{
    [System.Serializable]
    public class Craft
    {
        public string Craft_ID;
        public string Craft_Author;
        public string Craft_Name;
        public string Craft_Details;
        public string User_ID;
        public int Likes;
        public int Shares;
        public bool Is_Saved;
        public string Craft_Image;
        public string Author_Image;
        public string Category;
        public DateTime? Post_Date;
        public Step[] Steps;
        public Item[] Items;
    }


    // [System.Serializable]
    // public class Craft
    // {
    //     [JsonProperty("Craft_ID")]
    //     public string Craft_ID { get; set; }

    //     [JsonProperty("Craft_Author")]
    //     public string Craft_Author { get; set; }

    //     [JsonProperty("Craft_Name")]
    //     public string Craft_Name { get; set; }

    //     [JsonProperty("Craft_Details")]
    //     public string Craft_Details { get; set; }

    //     [JsonProperty("User_ID")]
    //     public string User_ID { get; set; }

    //     [JsonProperty("Likes")]
    //     public int? Likes { get; set; }

    //     [JsonProperty("Shares")]
    //     public int? Shares { get; set; }

    //     [JsonProperty("Is_Saved")]
    //     public bool? Is_Saved { get; set; }

    //     [JsonProperty("Craft_Image")]
    //     public string Craft_Image { get; set; }

    //     [JsonProperty("Author_Image")]
    //     public string Author_Image { get; set; }

    //     [JsonProperty("Category")]
    //     public string Category { get; set; }

    //     [JsonProperty("Post_Date")]
    //     public DateTime? Post_Date { get; set; }

    //     // Lists for Steps and Items
    //     [JsonProperty("Steps")]
    //     public List<Step> Steps { get; set; }

    //     [JsonProperty("Items")]
    //     public List<Item> Items { get; set; }

    //     // Constructor
    //     public Craft()
    //     {
    //         Steps = new List<Step>();
    //         Items = new List<Item>();
    //     }
    // }


    [System.Serializable]
    public class CraftList
    {
        public List<Craft> data;
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
}


// { "Craft_ID": null, "Craft_Author": null, "Craft_Name": null, "Craft_Details": null, "User_ID": null, "Likes": null, "Shares": null, "Is_Saved": null, "Craft_Image": null, "Author_Image": null, "Category": null, "Post_Date": null, "Steps": null, "Items": null}
// { "Step_ID": null, "Craft_ID": null, "Step_Order": 0, "Title": null, "Step_Instruction": null, "Timer_Duration": 0, "Image_URL": null, "Video_URL": null }
// { "Step_ID": null, "Item_ID": null, "Craft_ID": null, "Item_Name": null, "Quantity": null, "IsCompleted": false, "Link_To_Purchase": null, "Item_Category": null, "Item_Image": null }