using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField]
    Block blockPrefab;

    [SerializeField]
    StackLabel labelPrefab;


    [SerializeField]
    Vector3 stack_offset = new Vector3(7.5f, 0f, 0f);

    [SerializeField]
    Vector3 initialPosition_XAxis = new Vector3(-9f, 0.3f, 0f);

    [SerializeField]
    Vector3 offset_XAxis = new Vector3(1.5f, 0f, 0f);

    [SerializeField]
    Vector3 initialPosition_ZAxis = new Vector3(-7.5f, 0.3f, -1.5f);

    [SerializeField]
    Vector3 offset_ZAxis = new Vector3(0f, 0f, 1.5f);

    [SerializeField]
    float block_height = .6f;

    [SerializeField]
    int blocksPerRow = 3;

    [SerializeField]
    Transform blocksParent;

    [SerializeField]
    Material[] blockMaterials;

    [SerializeField]
    Vector3 labelOffset = new Vector3(0f, 0f, -1f);

    public static BlockManager Instance;

    Dictionary<string, List<Block>> stacks = new Dictionary<string, List<Block>>();

    void Awake()
    {
        Instance = this;
    }

    public void InstantiateBlocks(List<BlockInfo> blockInfos)
    {
        if (blockInfos == null || blockInfos.Count == 0 )
        {
            Debug.LogError("block info list is null or empty");
            return;
        }

        if( blockPrefab == null )
        {
            Debug.LogError("Block prefab not found");
            return;
        }

        if( labelPrefab == null )
        {
            Debug.LogError("Label prefab not found");
            return;
        }

        List<string> grades = null;
        foreach (BlockInfo blockInfo in blockInfos)
        {
            if (!stacks.ContainsKey(blockInfo.grade))
            {
                List<Block> stack = new List<Block>();
                stacks.Add(blockInfo.grade, stack);

                grades = new List<string>(stacks.Keys);
            }

            int index = stacks[blockInfo.grade].Count;  // index of new block in current stack
            bool x_axis_align = (index / blocksPerRow) % 2 != 0;

            Vector3 blockPosition = grades.IndexOf(blockInfo.grade) * stack_offset;
            blockPosition += x_axis_align ? initialPosition_XAxis : initialPosition_ZAxis;
            blockPosition += (x_axis_align ? offset_XAxis : offset_ZAxis) * (index % blocksPerRow);
            blockPosition += index / blocksPerRow * block_height * Vector3.up;

            Block block = Instantiate(blockPrefab, blockPosition, x_axis_align ? Quaternion.AngleAxis(90f, Vector3.up) : Quaternion.identity, blocksParent);
            block.Init(blockInfo);

            stacks[blockInfo.grade].Add(block);

            if( stacks[blockInfo.grade].Count == 1 )
            {
                // new stack. add label
                StackLabel label = Instantiate(labelPrefab, blockPosition + labelOffset, Quaternion.identity);
                label.UpdateText(blockInfo.grade);
            }
        }
    }
    
    public Material GetBlockMaterial(BlockType blockType)
    {
        if( blockMaterials == null || blockMaterials.Length == 0 )
        {
            Debug.LogError("Assign block materials");
            return null;
        }

        if ((int)blockType < blockMaterials.Length)
            return blockMaterials[(int)blockType];

        Debug.LogError("Material Index out of range. Please assign more materials");
        return null;
    }
}
