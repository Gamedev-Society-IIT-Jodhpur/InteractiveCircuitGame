using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireNode : MonoBehaviour
{
    Transform parentPos;
    Transform[] childs;
    Vector3 node1Pos;
    Vector3 node2Pos;
    Vector3 node1Scale;
    Vector3 node2Scale;
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
            float angle = Mathf.Atan(angleVector.y / angleVector.x) * (180 / Mathf.PI);
            parentPos.eulerAngles = new Vector3(0, 0, angle);
        }
        else
        {
            float angle = Mathf.Atan(angleVector.y / angleVector.x) * (180 / Mathf.PI);
            parentPos.eulerAngles = new Vector3(0, 0, angle+180);
        }

        node1Scale = new Vector3(childs[1].localScale.x * parentPos.localScale.x, childs[1].localScale.y * parentPos.localScale.y, childs[1].localScale.z * parentPos.localScale.z);
        node2Scale = new Vector3(childs[2].localScale.x * parentPos.localScale.x, childs[2].localScale.y * parentPos.localScale.y, childs[2].localScale.z * parentPos.localScale.z);

        parentPos.localScale = new Vector3(Vector3.Distance(node1Pos, node2Pos), parentPos.localScale.y, parentPos.localScale.z);
        childs[1].localScale = new Vector3(node1Scale.x / parentPos.localScale.x, node1Scale.y / parentPos.localScale.y, node1Scale.z / parentPos.localScale.z);
        childs[2].localScale = new Vector3(node2Scale.x / parentPos.localScale.x, node2Scale.y / parentPos.localScale.y, node2Scale.z / parentPos.localScale.z);
        childs[1].position = node1Pos;
        childs[2].position = node2Pos;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
