// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.SceneManagement;
// using System.Linq;

// public class ItemViewModel : MonoBehaviour
// {
//     // private TextLog textLog;
//     public static ItemViewModel Instance { get; private set; }
//     public event Action<List<Item>> OnItemsFetched;
//     public GameObject itemPrefab;
//     public Transform itemPanel;


//     public void Awake()
//     {
//         if (Instance == null)
//         {
//             Instance = this;
//             DontDestroyOnLoad(gameObject);
//             // TextLog.Instance.Log("ItemViewModel has been assigned");
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }

//     public void DisplayItemsForStep(string craftId, string stepId)
//     {
//         // Clear previous items
//         foreach (Transform child in itemPanel)
//         {
//             Destroy(child.gameObject);
//         }

//         // Fetch items for the current step
//         var itemsForStep = ItemDataPersist.Instance.GetItemsForStep(craftId, stepId);

//         // Instantiate item prefabs and populate them with the fetched items data
//         foreach (var item in itemsForStep)
//         {
//             var itemGO = Instantiate(itemPrefab, itemPanel);
//             // Assuming your itemPrefab has a script to set up the item's details
//             itemGO.GetComponent<ItemUI>().Setup(item);
//         }
//     }

// }