using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CartPanel : MonoBehaviour
{
    public TextMeshProUGUI Items;
    public RectTransform PanelRect;

    void Update()
    {
        if (PanelRect.rect.height>450)
        {
            Items.enabled = true;
        }
        else
        {
            Items.enabled = false;
        }
    }
}
