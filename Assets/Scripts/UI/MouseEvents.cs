using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEvents : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void ChangeNumber(int a, int b)
    public void ChangeNumber(int a)
    //public void ChangeNumber()
    {
        int c = 1 + a;
        Debug.Log(c);

    }
}
