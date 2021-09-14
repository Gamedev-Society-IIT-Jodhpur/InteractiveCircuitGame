using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    string currentScene;
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    public void ChangScene()
    {
        if (currentScene == "scene1")
        {
            SceneManager.LoadScene("scene2");
        }
        else
        {
            SceneManager.LoadScene("scene1");
        }
    }
}
