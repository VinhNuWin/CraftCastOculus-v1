// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class LoadDataFromAPI : MonoBehaviour
// public void Start()
// {
//     LoadAllDataFromAPI();
// }

// private void LoadAllDataFromAPI()
// {
//     StartCoroutine(LoadDataCoroutine());
// }

// private IEnumerator LoadDataCoroutine()
// {
//     // Example API call for crafts
//     string craftUrl = "https://yourapi.com/crafts";
//     UnityWebRequest craftRequest = UnityWebRequest.Get(craftUrl);
//     yield return craftRequest.SendWebRequest();

//     if (craftRequest.isNetworkError || craftRequest.isHttpError)
//     {
//         Debug.LogError(craftRequest.error);
//     }
//     else
//     {
//         List<Craft> crafts = JsonConvert.DeserializeObject<List<Craft>>(craftRequest.downloadHandler.text);
//         CraftDataPersist.Instance.LoadCrafts(crafts);

//         foreach (Craft craft in crafts)
//         {
//             // Assume API endpoints or JSON data for items and steps
//             UnityWebRequest itemRequest = UnityWebRequest.Get($"https://yourapi.com/items/{craft.Craft_ID}");
//             yield return itemRequest.SendWebRequest();
//             List<Item> items = JsonConvert.DeserializeObject<List<Item>>(itemRequest.downloadHandler.text);
//             ItemDataPersist.Instance.LoadItemsForCraft(craft.Craft_ID, items);

//             UnityWebRequest stepRequest = UnityWebRequest.Get($"https://yourapi.com/steps/{craft.Craft_ID}");
//             yield return stepRequest.SendWebRequest();
//             List Step steps = JsonConvert.DeserializeObject < List

// Step >> (stepRequest.downloadHandler.text);
//             StepDataPersist.Instance.LoadStepsForCraft(craft.Craft_ID, steps);
//         }
//     }
// }

