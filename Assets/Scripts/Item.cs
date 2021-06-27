using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    Transform[] childs;
    Vector3 node1Pos;
    Vector3 node2Pos;
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
            //childs[3].position = (node1Pos + node2Pos) / 2;

            transform.position = (node1Pos + node2Pos) / 2;

            Vector2 angleVector = new Vector2(node1Pos.x - node2Pos.x, node1Pos.y - node2Pos.y);
            if (angleVector.x < 0)
            {
                float angle = Mathf.Atan(-angleVector.y / angleVector.x) * (180 / Mathf.PI);
                transform.eulerAngles = new Vector3(0, 0, -angle);
                //childs[2].transform.eulerAngles = childs[2].transform.eulerAngles * 0.5f;
            }
            else
            {
                float angle = Mathf.Atan(angleVector.y / angleVector.x) * (180 / Mathf.PI);
                transform.eulerAngles = new Vector3(0, 0, angle);
                //childs[2].transform.eulerAngles = childs[2].transform.eulerAngles * 0.5f;
            }

            childs[1].position = node1Pos;
            childs[2].position = node2Pos;
            //childs[3].position = (node1Pos + node2Pos) / 2;

            childs[3].localScale = new Vector3(Vector3.Distance(node1Pos, node2Pos) / 2, childs[3].localScale.y, childs[3].localScale.z);
        }
        
        


    }
}
