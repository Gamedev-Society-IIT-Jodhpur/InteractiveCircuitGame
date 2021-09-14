using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSub : MonoBehaviour
{
    public void ChangeVariable()
    {
        if (gameObject.name == "add")
        {
            StaticVariable.staticVariable += 1;
        }
        else
        {
            StaticVariable.staticVariable -= 1;
        }
    }
}
