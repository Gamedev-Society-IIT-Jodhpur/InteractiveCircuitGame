using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BJTItem : MonoBehaviour
{
    Transform[] childs;
    public bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        childs = gameObject.GetComponentsInChildren<Transform>();
        childs[4].localScale = new Vector3(Vector3.Distance(childs[0].position, childs[1].position) * 2, childs[4].localScale.y, childs[4].localScale.z);
        childs[4].position = new Vector3((childs[0].position.x + childs[1].position.x) / 2, (childs[0].position.y + childs[1].position.y) / 2, childs[4].position.z);

    }

    // Update is called once per frame
    void Update()
    {
        
        if (isMoving)
        {
            if (GetComponentInChildren<Text>() != null)
            {
                GetComponentInChildren<Text>().transform.rotation = Quaternion.identity;
            }

            childs[4].localScale = new Vector3(Vector3.Distance(childs[0].position, childs[1].position)*2, childs[4].localScale.y, childs[4].localScale.z);
            childs[4].position = new Vector3((childs[0].position.x + childs[1].position.x) / 2, (childs[0].position.y + childs[1].position.y) / 2, childs[4].position.z);
        }
    }
}
