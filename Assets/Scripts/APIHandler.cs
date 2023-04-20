using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIHandler : MonoBehaviour
{
    private const string url = "https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack";

    public IEnumerator GetStacks(GameManager.ReceivedStacks onReceivedStacks)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        // Check for errors
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
            yield break;
        }

        // Get the response data
        string responseData = www.downloadHandler.text;

        if (onReceivedStacks != null)
            onReceivedStacks(new List<Stack>(JsonHelper.FromJson<Stack>(responseData)));
    }
}
