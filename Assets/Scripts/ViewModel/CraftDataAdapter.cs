// using Newtonsoft.Json;
// using System;
// using System.Collections.Generic;

// public static class CraftDataAdapter
// {
//     public static Craft AdaptCraftFromJson(string jsonData)
//     {
//         try
//         {
//             // Directly deserialize the JSON data into the Craft object
//             Craft craft = JsonConvert.DeserializeObject<Craft>(jsonData);

//             // Additional processing if needed
//             // For example, handling null checks or default values
//             if (craft.Steps == null)
//                 craft.Steps = new List

//             if (craft.Items == null)
//                 craft.Items = new List<Item>();

//             // Example of additional logic: sort steps by Step_Order if needed
//             craft.Steps.Sort((x, y) => x.Step_Order.CompareTo(y.Step_Order));

//             return craft;
//         }
//         catch (Exception e)
//         {
//             // Log or handle the error appropriately
//             UnityEngine.Debug.LogError("Error deserializing Craft data: " + e.Message);
//             return null;
//         }
//     }
// }
