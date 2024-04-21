using UnityEngine;
using System;
using System.Collections;
// using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using UnityEngine.Networking;

public class Response
{
    public string chatResponse;
}

public class CraftResponse
{
    public Craft Craft;
}

public class WebScraperClient : MonoBehaviour
{
    private TextLog textLog;
    // URL of your web scraper server
    private string baseUrl = "http://localhost:3000/scrape?url=";

    async void Start()
    {
        TextLog.Instance.Log("Starting WebScraper Script");

        string targetUrl = "https://skinnyspatula.com/keto-beef-stroganoff/";
        StartCoroutine(FetchAndProcessData(targetUrl));
    }

    IEnumerator FetchAndProcessData(string url)
    {
        string fullUrl = baseUrl + UnityWebRequest.EscapeURL(url);
        using (UnityWebRequest request = UnityWebRequest.Get(fullUrl))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                TextLog.Instance.Log("Error: " + request.error);
            }
            else
            {
                try
                {
                    string responseBody = request.downloadHandler.text;
                    // TextLog.Instance.Log("Response Body: " + responseBody);

                    Response response = JsonConvert.DeserializeObject<Response>(responseBody);
                    if (response == null || string.IsNullOrEmpty(response.chatResponse))
                    {
                        TextLog.Instance.Log("Response or chatResponse is null");
                        yield break; ; // Exit if response is null to prevent further errors
                    }

                    // Now parse the inner JSON string contained in 'chatResponse':
                    CraftResponse craftResponse = JsonConvert.DeserializeObject<CraftResponse>(response.chatResponse.Replace("```json\n", "").Replace("\n```", ""));
                    if (craftResponse == null || craftResponse.Craft == null)
                    {
                        TextLog.Instance.Log("No craft data found in the response.");
                        yield break; ; // Exit if craftResponse is null to prevent further errors
                    }

                    // Now you can access the Craft object and its properties:
                    TextLog.Instance.Log("Craft Name: " + craftResponse.Craft.Craft_Name);
                    ProcessCraft(craftResponse.Craft);
                }
                catch (Exception e)
                {
                    TextLog.Instance.Log("Exception Caught!");
                    TextLog.Instance.Log("Message: " + e.Message);
                }
            }
        }
    }

    private void ProcessCraft(Craft craft)
    {
        TextLog.Instance.Log("Processing Craft: " + craft.Craft_Name);
        CraftDataPersist.Instance.AddOrUpdateCraft(craft);

        if (craft.Steps == null)
        {
            TextLog.Instance.Log("Steps list is null.");
        }
        else
        {
            foreach (Step step in craft.Steps)
            {
                if (step == null)
                {
                    TextLog.Instance.Log("A step in Steps list is null.");
                    continue;
                }
                StepDataPersist.Instance.AddOrUpdateStep(step);
            }
        }

        if (craft.Items == null)
        {
            TextLog.Instance.Log("Items list is null.");
        }
        else
        {
            foreach (Item item in craft.Items)
            {
                if (item == null)
                {
                    TextLog.Instance.Log("An item in Items list is null.");
                    continue;
                }
                ItemDataPersist.Instance.AddOrUpdateItem(item);
            }
        }
        CraftDataPersist.Instance.SelectedCraft = craft;
    }
}
