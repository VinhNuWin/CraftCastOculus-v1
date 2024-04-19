using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;

public class CraftViewModel : MonoBehaviour
{
    private TextLog textLog;
    public event Action<List<Craft>> OnCraftsFetched;
    private List<Craft> fetchedCrafts = new List<Craft>();


    void Start()
    {
        StartCoroutine(GetCraftsCoroutine());
        TextLog.Instance.Log("[CVM] GetCraftsCoroutine started");
    }

    private List<Craft> GetAllCrafts()
    {
        return fetchedCrafts;
    }

    IEnumerator GetCraftsCoroutine()
    {
        fetchedCrafts = new List<Craft>
    {
        new Craft { Craft_ID = "C001", User_ID = "User1", Craft_Name = "Spaghetti", Craft_Details = "Details of Craft 1", Likes = 10, Shares = 1, Craft_Image = "CraftImages/C001", Category = "food", Craft_Author = "CraftCast", Author_Image = "AuthorImages/U001", Post_Date = new DateTime(2023, 1, 15) },
        new Craft { Craft_ID = "C002", User_ID = "User2", Craft_Name = "Fish Burrito", Craft_Details = "Details of Craft 2", Likes = 20, Shares = 2, Craft_Image = "CraftImages/C002", Category = "food", Craft_Author = "CraftCast", Author_Image = "AuthorImages/U001", Post_Date = new DateTime(2023, 1, 15) },
        new Craft { Craft_ID = "C003", User_ID = "User3", Craft_Name = "Fried Tofu", Craft_Details = "Details of Craft 3", Likes = 30, Shares = 3, Craft_Image = "CraftImages/C003", Category = "Sewing", Craft_Author = "CraftCast", Author_Image = "AuthorImages/U001", Post_Date = new DateTime(2023, 1, 15) },
        new Craft { Craft_ID = "C004", User_ID = "Vinh Nguyen", Craft_Name = "Salmon Sushi Bake", Craft_Details = "A deconstructed sushi roll baked with salmon, rice, and various toppings.", Likes = 30, Shares = 3, Craft_Image = "CraftImages/C004", Category = "food", Craft_Author = "CraftCast", Author_Image = "AuthorImages/U001", Post_Date = new DateTime(2023, 1, 15) },
        new Craft { Craft_ID = "C005", User_ID = "Vinh Nguyen", Craft_Name = "Salmon Sushi Bake", Craft_Details = "A deconstructed sushi roll baked with salmon, rice, and various toppings.", Likes = 30, Shares = 3, Craft_Image = "CraftImages/C005", Category = "Knitting", Craft_Author = "CraftCast", Author_Image = "AuthorImages/U001", Post_Date = new DateTime(2023, 1, 15) },
        new Craft { Craft_ID = "C006", User_ID = "Vinh Nguyen", Craft_Name = "Minimal Wallet", Craft_Details = "A deconstructed wallet", Likes = 30, Shares = 3, Craft_Image = "CraftImages/C006", Category = "leatherwork", Craft_Author = "CraftCast", Author_Image = "AuthorImages/U001", Post_Date = new DateTime(2023, 1, 15) },
        new Craft { Craft_ID = "C007", User_ID = "Vinh Nguyen", Craft_Name = "Purse", Craft_Details = "A deconstructed sushi roll baked with salmon, rice, and various toppings.", Likes = 30, Shares = 3, Craft_Image = "CraftImages/C007", Category = "leatherwork", Craft_Author = "CraftCast", Author_Image = "AuthorImages/U001", Post_Date = new DateTime(2023, 1, 15) },
        new Craft { Craft_ID = "C008", User_ID = "Vinh Nguyen", Craft_Name = "Saddle Stitch", Craft_Details = "A deconstructed sushi roll baked with salmon, rice, and various toppings.", Likes = 30, Shares = 3, Craft_Image = "CraftImages/C008", Category = "leatherwork", Craft_Author = "CraftCast", Author_Image = "AuthorImages/U001", Post_Date = new DateTime(2023, 1, 15) },
        new Craft { Craft_ID = "C009", User_ID = "Vinh Nguyen", Craft_Name = "Watch Roll", Craft_Details = "A deconstructed sushi roll baked with salmon, rice, and various toppings.", Likes = 30, Shares = 3, Craft_Image = "CraftImages/C009", Category = "leatherwork", Craft_Author = "CraftCast", Author_Image = "AuthorImages/U001", Post_Date = new DateTime(2023, 1, 15) },
        new Craft { Craft_ID = "C010", User_ID = "Vinh Nguyen", Craft_Name = "Watch Roll", Craft_Details = "A deconstructed sushi roll baked with salmon, rice, and various toppings.", Likes = 30, Shares = 3, Craft_Image = "CraftImages/C010", Category = "null", Craft_Author = "CraftCast", Author_Image = "AuthorImages/U001", Post_Date = new DateTime(2023, 1, 15) },
        new Craft { Craft_ID = "C011", User_ID = "Vinh Nguyen", Craft_Name = "Caulking", Craft_Details = "A deconstructed sushi roll baked with salmon, rice, and various toppings.", Likes = 30, Shares = 3, Craft_Image = "CraftImages/C011", Category = "homediy", Craft_Author = "CraftCast", Author_Image = "AuthorImages/U001", Post_Date = new DateTime(2023, 1, 15) },
        new Craft { Craft_ID = "C012", User_ID = "Vinh Nguyen", Craft_Name = "Screen Door Install", Craft_Details = "A deconstructed sushi roll baked with salmon, rice, and various toppings.", Likes = 30, Shares = 3, Craft_Image = "CraftImages/C012", Category = "homediy", Craft_Author = "CraftCast", Author_Image = "AuthorImages/U001", Post_Date = new DateTime(2023, 1, 15) },
        new Craft { Craft_ID = "C013", User_ID = "Vinh Nguyen", Craft_Name = "Water Heater", Craft_Details = "A deconstructed sushi roll baked with salmon, rice, and various toppings.", Likes = 30, Shares = 3, Craft_Image = "CraftImages/C013", Category = "homediy", Craft_Author = "CraftCast", Author_Image = "AuthorImages/U001", Post_Date = new DateTime(2023, 1, 15) },
        new Craft { Craft_ID = "C014", User_ID = "Vinh Nguyen", Craft_Name = "Faucet", Craft_Details = "A deconstructed sushi roll baked with salmon, rice, and various toppings.", Likes = 30, Shares = 3, Craft_Image = "CraftImages/C014", Category = "homediy", Craft_Author = "CraftCast", Author_Image = "AuthorImages/U001", Post_Date = new DateTime(2023, 1, 15) },
        new Craft { Craft_ID = "C015", User_ID = "Vinh Nguyen", Craft_Name = "Faucet", Craft_Details = "A deconstructed sushi roll baked with salmon, rice, and various toppings.", Likes = 30, Shares = 3, Craft_Image = "CraftImages/C015", Category = "null", Craft_Author = "CraftCast", Author_Image = "AuthorImages/U001", Post_Date = new DateTime(2023, 1, 15) },
        new Craft { Craft_ID = "C003", User_ID = "User3", Craft_Name = "Leather Wallet", Craft_Details = "A detailed tutorial on creating a hand-stitched leather wallet, including steps for pattern making, cutting, preparing, dyeing, and stitching.", Likes = 0, Shares = 0, Craft_Image = "CraftImages/C003", Category = "leathercraft", Craft_Author = "CraftCast", Author_Image = "AuthorImages/U003", Post_Date = new DateTime(2024, 3, 30)
},

    };

        foreach (Craft craft in fetchedCrafts)
        {
            CraftDataPersist.Instance.AddOrUpdateCraft(craft);
        }
        TextLog.Instance.Log("[CVM] GetCraftsCoroutine OnCraftsFetched Completed/Invoked");
        OnCraftsFetched?.Invoke(fetchedCrafts);

        yield break;
    }

    public void FetchCraftsByCategory(string category)
    {
        // Simulate fetching crafts for the selected category
        // For the example, we filter the locally available crafts list
        List<Craft> filteredCrafts;
        if (category == "All")
        {
            filteredCrafts = GetAllCrafts(); // Implement this method to return all crafts
        }
        else
        {
            filteredCrafts = GetAllCrafts().Where(craft => craft.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        TextLog.Instance.Log("[CVM] FetchCraftsByCategory OnCraftsFetched Invoke");
        OnCraftsFetched?.Invoke(filteredCrafts);
    }
}
