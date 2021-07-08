using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] DragManager dragManager;
    float gridSpace;
    Transform parentPos;
    Transform[] childs;
    Vector3 node1Pos;
    Vector3 node2Pos;
    // Start is called before the first frame update
    void Start()
    {
        parentPos = transform.parent;
        childs = parentPos.gameObject.GetComponentsInChildren<Transform>();
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;

        transform.position = new Vector3(x, y, 0);

        node1Pos = childs[1].position;
        node2Pos = childs[2].position;
        parentPos.position = (node1Pos + node2Pos) / 2;
        Vector2 angleVector = new Vector2(node1Pos.x - node2Pos.x, node1Pos.y - node2Pos.y);
        if (angleVector.x < 0)
        {
            float angle = Mathf.Atan(-angleVector.y / angleVector.x) * (180 / Mathf.PI);
            parentPos.eulerAngles = new Vector3(0, 0, -angle);
        }
        else
        {
            float angle = Mathf.Atan(angleVector.y / angleVector.x) * (180 / Mathf.PI);
            parentPos.eulerAngles = new Vector3(0, 0, angle);
        }
        childs[1].position = node1Pos;
        childs[2].position = node2Pos;
        childs[3].localScale = new Vector3(Vector3.Distance(node1Pos, node2Pos) / 2, childs[3].localScale.y, childs[3].localScale.z);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
