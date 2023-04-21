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

    //[SerializeField]
    //TextMeshProUGUI[] gradeLabels;

    public void Init(BlockInfo blockInfo)
    {
        blockType = (BlockType)blockInfo.mastery;
        grade = blockInfo.grade;
        domain = blockInfo.domain;
        cluster = blockInfo.cluster;
        standarddescription = blockInfo.standarddescription;

        UpdateMaterial();

        //foreach (TextMeshProUGUI gradeLabel in gradeLabels)
        //    gradeLabel.text = grade;
    }

    public void UpdateMaterial(bool isSelected = false)
    {
        // modify material
        Material material = isSelected?BlockManager.Instance.GetSelectedBlockMaterial(): BlockManager.Instance.GetBlockMaterial(blockType);
        GetComponent<MeshRenderer>().material = material;
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}
