using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentInitialization : MonoBehaviour

{
    public string a;
    // Start is called before the first frame update
    public void Initialize()
    {
        UnifiedScript.dict1[a].DynamicInvoke();
    }
}
