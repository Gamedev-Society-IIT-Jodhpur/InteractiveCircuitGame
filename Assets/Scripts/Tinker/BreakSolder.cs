using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakSolder : MonoBehaviour
{
    public List<BreakSolder> connecteds;
    GameObject soldered;


    // Start is called before the first frame update
    void Start()
    {
        //connecteds.Add(gameObject.GetComponent<BreakSolder>());
        soldered = AssetManager.soldered;

    }

    public void Break()
    {
        GameObject newSoldered = Instantiate<GameObject>(soldered);
        newSoldered.transform.position = transform.position;
        transform.SetParent(newSoldered.transform);
        for (int i = 0; i < connecteds.Count; i++)
        {
            if (connecteds[i].transform.parent != newSoldered)
            {
                connecteds[i].ChangeParent(newSoldered.transform);
            }
        }
    }

    public void ChangeParent(Transform parent)
    {
        transform.SetParent(parent);
        for (int i = 0; i < connecteds.Count; i++)
        {
            if (connecteds[i].transform.parent!=parent)
            {
                connecteds[i].ChangeParent(parent);
            }
        }
    }


}

