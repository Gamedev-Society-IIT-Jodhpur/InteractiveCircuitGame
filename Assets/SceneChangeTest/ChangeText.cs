using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = StaticVariable.staticVariable.ToString();
    }
}
