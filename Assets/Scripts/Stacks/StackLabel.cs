using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StackLabel : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textMeshPro;

    public void UpdateText(string str)
    {
        if (textMeshPro)
            textMeshPro.text = str;
    }
}
