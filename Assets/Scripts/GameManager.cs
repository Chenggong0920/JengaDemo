using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(APIHandler))]
[RequireComponent(typeof(BlockManager))]
public class GameManager : MonoBehaviour
{
    public delegate void ReceivedStacks(List<BlockInfo> blockInfos);

    APIHandler apiHandler;
    BlockManager blockManager;

    void Awake()
    {
        apiHandler = GetComponent<APIHandler>();
        blockManager = GetComponent<BlockManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(apiHandler.GetStacks(OnReceivedStacks));
    }

    private void OnReceivedStacks(List<BlockInfo> blockInfos)
    {
        // sort
        blockInfos = blockInfos.OrderBy(blockInfo => blockInfo.grade).ThenBy(blockInfo => blockInfo.domain).ThenBy(blockInfo => blockInfo.cluster).ThenBy(blockInfo => blockInfo.standardid).ToList();

        // instantiate blocks
        blockManager.InstantiateBlocks(blockInfos);
    }
}
