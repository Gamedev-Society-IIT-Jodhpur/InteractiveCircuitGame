using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breadboard : MonoBehaviour
{
    [SerializeField] BreadboardGrid[] childs;
    public Transform[] rows = new Transform[4];
    public Transform[,] columns = new Transform[60, 2];
    Transform[] grandChilds;

    // Start is called before the first frame update
    void Start()
    {
        childs = GetComponentsInChildren<BreadboardGrid>();
        int x = 0;
        int y = 0;
        for (int k = 0; k < childs.Length; k++)
        {
            grandChilds = childs[k].GetComponentsInChildren<Transform>();
            if (childs[k].isPowerGrid)
            {
                rows[0+x] = grandChilds[1];
                rows[1+x] = grandChilds[2];
                x+=2;
            }
            else
            {
                for (int i = 1, j = 0; i < grandChilds.Length; i += 5, j++)
                {
                    columns[j+y, 0] = grandChilds[i];
                    columns[j+y, 1] = grandChilds[i + 4];
                }
                y += 30;
            }
        }
        
    }

}
