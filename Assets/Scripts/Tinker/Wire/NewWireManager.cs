using System.Collections.Generic;
using UnityEngine;

public class NewWireManager : MonoBehaviour
{
    public List<Transform> nodes;
    [SerializeField] GameObject wireNode;
    public GameObject node1;
    public GameObject node2;
    Transform[] childs;

    private void Start()
    {
        node1 = Instantiate<GameObject>(wireNode);
        node1.transform.position = nodes[0].position;
        node1.transform.SetParent(nodes[0]);
    }


    public void DestroyWire()
    {
        int multiplier = 0;
        if (node1.transform.parent.parent.tag != "Breadboard grid")
        {
            multiplier += 1;
        }
        childs = GetComponentsInChildren<Transform>();
        WireManager.isDrawingWire = false;
        nodes[0].GetComponent<NodeTinker>().wires.Remove(childs[1].gameObject);
        if (node2 != null)
        {
            if (node2.transform.parent.parent.tag != "Breadboard grid")
            {
                multiplier += 1;
            }
            nodes[nodes.Count - 1].GetComponent<NodeTinker>().wires.Remove(childs[childs.Length - 1].gameObject);
            Destroy(node2);
        }
        if (multiplier > 0)
        {
            if (!StaticData.hasSolderBroken)
            {
                FirstSolderBreakPopUp.Instance.Open(DestroyWire, "You are about to delete soldered wire.\nIt'll cost you XP.");
                return;
            }

            MoneyXPManager.DeductXP(10 * multiplier);
            ScoringScript.UpdateError(0);
            CustomNotificationManager.Instance.AddNotification(1, "Deleting wire soldered to components cost XP");
        }
        Destroy(node1);
        CircuitManagerTinker.componentList.Remove(gameObject);
        Destroy(gameObject);
    }
}
