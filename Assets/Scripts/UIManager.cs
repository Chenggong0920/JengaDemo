using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Button prevButton;

    [SerializeField]
    private Button nextButton;

    private int currentStackSelected = 1;

    [SerializeField]
    private TextMeshProUGUI txtDetails;

    [SerializeField]
    private Button testStackButton;

    void Start()
    {
        prevButton.onClick.AddListener(OnPrevButtonClicked);
        nextButton.onClick.AddListener(OnNextButtonClicked);
        testStackButton.onClick.AddListener(OnTestStackClicked);
    }

    void Update()
    {
        prevButton.interactable = BlockManager.Instance.isReady && currentStackSelected != 0;
        nextButton.interactable = BlockManager.Instance.isReady && currentStackSelected < BlockManager.NumberOfStacks - 1;
        testStackButton.interactable = BlockManager.Instance.isReady;

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Block")))
            {
                Block block = hit.collider.gameObject.GetComponent<Block>();
                if( block )
                {
                    UpdateDetails(block.grade, block.domain, block.cluster, block.standarddescription);
                    BlockManager.Instance.OnBlockSelected(block);
                }
            }
        }
    }

    void OnPrevButtonClicked()
    {
        currentStackSelected = Mathf.Max(currentStackSelected - 1, 0);

        OnStackIndexUpdated();
    }

    void OnNextButtonClicked()
    {
        currentStackSelected = Mathf.Min(currentStackSelected + 1, BlockManager.NumberOfStacks - 1);

        OnStackIndexUpdated();
    }

    void OnStackIndexUpdated()
    {
        Camera.main.BroadcastMessage("OnStackSelected", currentStackSelected, SendMessageOptions.DontRequireReceiver);
    }

    void UpdateDetails(string grade, string domain, string cluster, string standarddescription)
    {
        if (txtDetails)
            txtDetails.text = string.Format("{0}: {1}\n{2}\n{3}", grade, domain, cluster, standarddescription);
    }

    void OnTestStackClicked()
    {
        BlockManager.Instance.TestStack(currentStackSelected);
    }
}
