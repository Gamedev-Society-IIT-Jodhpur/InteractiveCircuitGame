using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static LoadingManager instance;
    public GameObject loadingScreen;
    public Slider progressBar;
    public TextMeshProUGUI loadingtext;
    
   private void Awake()
    {
        instance = this;
       SceneManager.LoadSceneAsync((int)SceneIndexes.Avatar, LoadSceneMode.Additive);
        loadingScreen.gameObject.SetActive(false);
    }

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    // Update is called once per frame
   public void LoadGame()
    {
        loadingScreen.gameObject.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.Avatar));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.Falstad, LoadSceneMode.Additive));
        StartCoroutine(GetSceneLoadProgress());
    }

    float totalSceneProgress;
    public IEnumerator GetSceneLoadProgress()
    {
        for(int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                totalSceneProgress = 0;
                foreach(AsyncOperation operation in scenesLoading)
                {
                    totalSceneProgress += operation.progress;
                }
                totalSceneProgress = (totalSceneProgress / scenesLoading.Count) * 100f;
                loadingtext.text = string.Format("Loading Enviornments:{0}%", totalSceneProgress);
                progressBar.value = Mathf.RoundToInt(totalSceneProgress);

                yield return null;
            }
        }
        yield return new WaitForSeconds(2);
        loadingScreen.gameObject.SetActive(false);
        
    }
}
