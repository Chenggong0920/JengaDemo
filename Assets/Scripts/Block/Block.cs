using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(MeshRenderer))]
public class Block : MonoBehaviour
{
    public BlockType blockType { get; private set; }
    public string grade { get; private set; }
    public string domain { get; private set; }
    public string cluster { get; private set; }
    public string standarddescription { get; private set; }

    [SerializeField]
    TextMeshProUGUI[] gradeLabels;

    public void Init(BlockInfo blockInfo)
    {
        blockType = (BlockType)blockInfo.mastery;
        grade = blockInfo.grade;
        domain = blockInfo.domain;
        cluster = blockInfo.cluster;
        standarddescription = blockInfo.standarddescription;

        // modify material;
        Material material = BlockManager.Instance.GetBlockMaterial(blockType);
        GetComponent<MeshRenderer>().material = material;

        foreach (TextMeshProUGUI gradeLabel in gradeLabels)
            gradeLabel.text = grade;
    }
}
