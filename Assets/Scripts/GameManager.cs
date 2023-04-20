using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public delegate void ReceivedStacks(List<Stack> stacks);

    APIHandler apiHandler;
    void Awake()
    {
        apiHandler = GetComponent<APIHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (apiHandler == null)
        {
            Debug.LogError("API Handler not found");
            return;
        }

        StartCoroutine(apiHandler.GetStacks(OnReceivedStacks));
    }

    private void OnReceivedStacks(List<Stack> stacks)
    {
        Debug.Log(stacks.Count);
    }
}
