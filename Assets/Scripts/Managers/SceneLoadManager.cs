using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneLoadManager : MonoBehaviour
{
    public string sceneToLoad;
    AsyncOperation loadingOperation;
    Slider progressBar;

    public TMP_Text percentLoaded;

    void Start()
    {
        loadingOperation = SceneManager.LoadSceneAsync(sceneToLoad);
    }

    void Update()
    {
        float progressValue = Mathf.Clamp01(loadingOperation.progress / 0.9f);
        progressBar.value = progressValue;
        percentLoaded.text = Mathf.Round(progressValue * 100) + "%";
    }
}
