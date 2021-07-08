using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    Transform[] childs;
    Vector3 node1Pos;
    Vector3 node2Pos;
    Vector3 node1Scale;
    Vector3 node2Scale;
    public bool isMoving = false;
    
    // Start is called before the first frame update
    void Start()
    {
        childs = gameObject.GetComponentsInChildren<Transform>();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            node1Pos = childs[1].position;
            node2Pos = childs[2].position;

            transform.position = (node1Pos + node2Pos) / 2;

            Vector2 angleVector = new Vector2(node1Pos.x - node2Pos.x, node1Pos.y - node2Pos.y);
            if (angleVector.x < 0)
            {
                float angle = Mathf.Atan(-angleVector.y / angleVector.x) * (180 / Mathf.PI);
                transform.eulerAngles = new Vector3(0, 0, -angle);
            }
            else
            {
                float angle = Mathf.Atan(angleVector.y / angleVector.x) * (180 / Mathf.PI);
                transform.eulerAngles = new Vector3(0, 0, angle);
            }

            childs[1].position = node1Pos;
            childs[2].position = node2Pos;

            if (childs[0].tag=="Wire")
            {
                node1Scale = new Vector3(childs[1].localScale.x * transform.localScale.x, childs[1].localScale.y * transform.localScale.y, childs[1].localScale.z * transform.localScale.z);
                node2Scale = new Vector3(childs[2].localScale.x * transform.localScale.x, childs[2].localScale.y * transform.localScale.y, childs[2].localScale.z * transform.localScale.z);

                transform.localScale = new Vector3(Vector3.Distance(node1Pos, node2Pos), transform.localScale.y, transform.localScale.z);
                childs[1].localScale = new Vector3(node1Scale.x / transform.localScale.x, node1Scale.y / transform.localScale.y, node1Scale.z / transform.localScale.z);
                childs[2].localScale = new Vector3(node2Scale.x / transform.localScale.x, node2Scale.y / transform.localScale.y, node2Scale.z / transform.localScale.z);
                childs[1].position = node1Pos;
                childs[2].position = node2Pos;
            }
            else
            {
                childs[3].localScale = new Vector3(Vector3.Distance(node1Pos, node2Pos) / 2, childs[3].localScale.y, childs[3].localScale.z);

            }
        }
        
        


    }
}
