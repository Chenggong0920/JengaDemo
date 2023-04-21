using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Button prevButton;

    [SerializeField]
    private Button nextButton;

    private int currentStackSelected = 1;

    void Start()
    {
        prevButton.onClick.AddListener(OnPrevButtonClicked);
        nextButton.onClick.AddListener(OnNextButtonClicked);
    }

    void Update()
    {
        prevButton.interactable = BlockManager.Instance.isReady && currentStackSelected != 0;
        nextButton.interactable = BlockManager.Instance.isReady && currentStackSelected < BlockManager.NumberOfStacks - 1;
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
}
