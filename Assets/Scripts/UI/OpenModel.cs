using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenModel : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
