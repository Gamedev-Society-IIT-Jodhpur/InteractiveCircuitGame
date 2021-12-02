using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class Store : MonoBehaviour
{

    public static List<string> Items;

    void Start()
    {
        Items = new List<string>();

        //StartCoroutine(getData());
        Debug.Log("yo");
        //StartCoroutine(getData());
        
    }


}
