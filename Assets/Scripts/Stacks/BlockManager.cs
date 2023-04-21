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
    public float stack_offset = 7.5f;

    [SerializeField]
    public Vector3 initialPosition_XAxis = new Vector3(-9f, 0.3f, 0f);

    [SerializeField]
    public float block_offset = 1.5f;

    [SerializeField]
    public Vector3 initialPosition_ZAxis = new Vector3(-7.5f, 0.3f, -1.5f);

    [SerializeField]
    float block_height = .6f;

    [SerializeField]
    int blocksPerRow = 3;

    [SerializeField]
    Transform blocksParent;

    [SerializeField]
    Material[] blockMaterials;

    [SerializeField]
    Material selectedBlockMaterial;

    [SerializeField]
    Vector3 labelOffset = new Vector3(0f, 0f, -1f);

    public const int NumberOfStacks = 3;

    public static BlockManager Instance;

    Dictionary<string, List<Block>> stacks = new Dictionary<string, List<Block>>();

    public bool isReady = false;

    private Block prevSelectedBlock = null;

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
                if( grades != null && grades.Count == NumberOfStacks )
                {
                    // maximum number of stacks reached. ignore rest of the blocks
                    isReady = true;
                    return;
                }
                List<Block> stack = new List<Block>();
                stacks.Add(blockInfo.grade, stack);

                grades = new List<string>(stacks.Keys);
            }

            int index = stacks[blockInfo.grade].Count;  // index of new block in current stack
            bool x_axis_align = (index / blocksPerRow) % 2 != 0;

            Vector3 blockPosition = grades.IndexOf(blockInfo.grade) * stack_offset * Vector3.right;
            blockPosition += x_axis_align ? initialPosition_XAxis : initialPosition_ZAxis;
            blockPosition += (x_axis_align ? Vector3.right : Vector3.forward) * block_offset * (index % blocksPerRow);
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

        isReady = true;
    }
    
    public Material GetSelectedBlockMaterial()
    {
        return selectedBlockMaterial;
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

    public void OnBlockSelected(Block block)
    {
        if (block == null)
            return;

        if (prevSelectedBlock)
            prevSelectedBlock.UpdateMaterial(false);

        block.UpdateMaterial(true);
        prevSelectedBlock = block;
    }

    public void TestStack(int stackIndex)
    {
        if (stackIndex >= stacks.Count)
            return;

        List<string> grades = new List<string>(stacks.Keys);

        for(int i = 0; i < stacks[grades[stackIndex]].Count; i ++ )
        {
            if (stacks[grades[stackIndex]][i].blockType == BlockType.Glass)
            {
                stacks[grades[stackIndex]][i].Remove();
                stacks[grades[stackIndex]].RemoveAt(i--);
            }
        }
    }
}
