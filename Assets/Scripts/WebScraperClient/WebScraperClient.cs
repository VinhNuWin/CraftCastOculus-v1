using UnityEngine;
using System.Collections;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

public class WebScraperClient : MonoBehaviour
{
    // URL of your web scraper server
    private string baseUrl = "http://yourserver.com/api/scrape?url=";

    // HttpClient is intended to be instantiated once and re-used throughout the life of an application.
    private static readonly HttpClient client = new HttpClient();

    async void Start()
    {
        // Example URL to scrape
        string targetUrl = "http://example.com";
        await FetchAndProcessData(targetUrl);
    }

    async Task FetchAndProcessData(string url)
    {
        try
        {
            string fullUrl = baseUrl + UnityWebRequest.EscapeURL(url);
            HttpResponseMessage response = await client.GetAsync(fullUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            // Assuming your JSON structure from the scraper includes crafts, steps, and items
            // Deserialize JSON response
            var result = JsonConvert.DeserializeObject<DataContainer>(responseBody);

            if (result != null)
            {
                if (result.Crafts != null && result.Crafts.Count > 0)
                {
                    foreach (Craft craft in result.Crafts)
                    {
                        CraftDataPersist.Instance.AddOrUpdateCraft(craft);
                        Debug.Log("Updated Craft: " + craft.Craft_Name);
                    }
                }

                if (result.Steps != null && result.Steps.Count > 0)
                {
                    foreach (Step step in result.Steps)
                    {
                        StepDataPersist.Instance.AddOrUpdateStep(step);
                        Debug.Log("Updated Step: " + step.Title);
                    }
                }

                if (result.Items != null && result.Items.Count > 0)
                {
                    foreach (Item item in result.Items)
                    {
                        ItemDataPersist.Instance.AddOrUpdateItem(item);
                        Debug.Log("Updated Item: " + item.Item_Name);
                    }
                }
            }
        }
        catch (HttpRequestException e)
        {
            Debug.LogError("\nException Caught!");
            Debug.LogError("Message :{0} " + e.Message);
        }
    }


    // void UpdateSelectedCraft(string json)
    // {
    //     Craft craft = JsonUtility.FromJson<Craft>(json);
    //     if (craft != null)
    //     {
    //         CraftDataPersist.Instance.SelectedCraft = craft;
    //     }
    //     else
    //     {
    //         TextLog.Instance.Log("Failed to parse craft data from JSON.");
    //     }
    // }
}


// using System.Collections;
// using UnityEngine;
// using UnityEngine.Networking;

// public class WebScraperClient : MonoBehaviour
// {
//     private TextLog textLog;

//     string baseURL = "http://localhost:3000/scrape?url=";

//     // Start is called before the first frame update
//     void Start()
//     {
//         StartCoroutine(ScrapeWebsite("https://skinnyspatula.com/keto-beef-stroganoff/"));
//     }

//     IEnumerator ScrapeWebsite(string url)
//     {
//         UnityWebRequest request = UnityWebRequest.Get(baseURL + System.Uri.EscapeUriString(url));
//         yield return request.SendWebRequest();

//         if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
//         {
//             TextLog.Instance.Log(request.error);
//         }
//         else
//         {
//             TextLog.Instance.Log(request.downloadHandler.text);
//             UpdateSelectedCraft(request.downloadHandler.text);
//             Debug.Log(request.downloadHandler.text);
//         }
//     }

//     void UpdateSelectedCraft(string json)
//     {
//         Craft craft = JsonUtility.FromJson<Craft>(json);
//         if (craft != null)
//         {
//             CraftDataPersist.Instance.SelectedCraft = craft;
//         }
//         else
//         {
//             TextLog.Instance.Log("Failed to parse craft data from JSON.");
//         }
//     }
// }
