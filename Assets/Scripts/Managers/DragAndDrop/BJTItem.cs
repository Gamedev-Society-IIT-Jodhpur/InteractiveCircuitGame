using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BJTItem : MonoBehaviour
{
    Transform[] childs;
    public bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        childs = gameObject.GetComponentsInChildren<Transform>();
        childs[4].localScale = new Vector3(Vector3.Distance(childs[0].position, childs[3].position) * 2, childs[4].localScale.y, childs[4].localScale.z);
        childs[4].position = new Vector3((childs[0].position.x + childs[3].position.x) / 2, (childs[0].position.y + childs[3].position.y) / 2, childs[4].position.z);
        //print(childs[3].position);
        //print(childs[0].position);

    }

    // Update is called once per frame
    void Update()
    {
        
        if (isMoving)
        {
            childs[4].localScale = new Vector3(Vector3.Distance(childs[0].position, childs[3].position)*2, childs[4].localScale.y, childs[4].localScale.z);
            childs[4].position = new Vector3((childs[0].position.x + childs[3].position.x) / 2, (childs[0].position.y + childs[3].position.y) / 2, childs[4].position.z);
        }
    }
}
