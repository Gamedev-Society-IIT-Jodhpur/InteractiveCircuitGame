using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static LoadingManager instance;
    public GameObject loadingScreen;
    public Slider progressBar;
    public TextMeshProUGUI loadingtext;
    [SerializeField] SceneIndexes sceneIndexes;

    private void Awake()
    {
        instance = this;
        SceneManager.LoadSceneAsync((int)sceneIndexes, LoadSceneMode.Additive);
        loadingScreen.gameObject.SetActive(false);

    }

    void Start()
    {
        //TODO remove this code
       // MoneyAndXPData.InitiateMoney(50000);
        //print("remove above code in final version");
    }

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    public void LoadGame(SceneIndexes from, SceneIndexes to)
    {
        //----------------------
        //NetworkSingleton.Instance.SetXp();
        //----------------------
        loadingScreen.gameObject.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)from));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)to, LoadSceneMode.Additive));
        StartCoroutine(GetSceneLoadProgress());
        //SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int)to));
    }
    public void LoadGame(SceneIndexes from, string to)
    {
        //----------------------
        //NetworkSingleton.Instance.SetXp();
        //----------------------
        loadingScreen.gameObject.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)from));
        scenesLoading.Add(SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive));
        StartCoroutine(GetSceneLoadProgress());
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName(to));
    }

    float totalSceneProgress;
    public IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                totalSceneProgress = 0;
                foreach (AsyncOperation operation in scenesLoading)
                {
                    totalSceneProgress += operation.progress;
                }
                totalSceneProgress = (totalSceneProgress / scenesLoading.Count) * 100f;
                loadingtext.text = string.Format("Loading :{0}%", Mathf.RoundToInt(totalSceneProgress));
                progressBar.value = Mathf.RoundToInt(totalSceneProgress);

                yield return null;
            }
        }
        yield return new WaitForSeconds(2);
        loadingScreen.gameObject.SetActive(false);
    }
}
