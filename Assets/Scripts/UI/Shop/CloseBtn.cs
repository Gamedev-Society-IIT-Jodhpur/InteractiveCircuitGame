using UnityEngine;
using UnityEngine.EventSystems;

public class CloseBtn : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData pointerEventData)
    {

        TooltipSystem.Hide();

    }
}
