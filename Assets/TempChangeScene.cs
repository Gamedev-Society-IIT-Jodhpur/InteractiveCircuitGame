using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempChangeScene : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            SceneManager.LoadScene(0);
        }else if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            SceneManager.LoadScene(1);
        }else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            SceneManager.LoadScene(2);
        }else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            SceneManager.LoadScene(3);
        }else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            SceneManager.LoadScene(4);
        }else if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            SceneManager.LoadScene(5);
        }else if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            SceneManager.LoadScene(6);
        }
    }


}
