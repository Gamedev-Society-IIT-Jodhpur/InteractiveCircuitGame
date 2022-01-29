using UnityEngine;


public class ButtonManager : MonoBehaviour
{
    public void DeleteComponent()
    {
        if (CircuitManager.selected)
        {
            CircuitManager.ChangeSelected(null);
            //CircuitManager.selected.GetComponent<Renderer>().material = AssetManager.GetInstance().defaultMaterial;
            CircuitManager.componentList.Remove(CircuitManager.selected);
            Destroy(CircuitManager.selected);
            if (CircuitManager.selected.tag == "Gizmo")
            {
                DragManager.isGizmoPresent = false;
            }
        }
        else
        {
            CustomNotificationManager.Instance.AddNotification(2, "No component selected");
        }
    }
}
