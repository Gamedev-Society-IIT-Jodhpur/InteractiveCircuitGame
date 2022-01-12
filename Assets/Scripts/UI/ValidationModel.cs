using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ValidationModel : MonoBehaviour
{
    // Start is called before the first frame update

    public static bool isSuccess = false;
    public static bool isfirsttime = true;
    public static ValidationModel Instance;

    [SerializeField]
    TMP_Text title;
    [SerializeField]
    TMP_Text data;
    [SerializeField]
    Text buttonText;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        print(isfirsttime);
        transform.localScale = Vector2.zero;
        if (isfirsttime && SceneManager.GetSceneAt(1).name=="MAP") {
            
            title.text = "Note ";
            data.text = "There are 3 Shops in your locality. You may choose any.";
            buttonText.text = "Continue to Map";
            transform.LeanScale(Vector2.one, 0.5f);
            isfirsttime = false;
            
        }
        
    }

    public void Open()
    {

        if (isSuccess)
        {
            title.text = "Success";
            data.text = "Your circuit is valid.\nProceed to store to buy the components.\nChoose mode of travel wisely in the map.";
            buttonText.text = "Continue to Map";
        }
        else
        {
            title.text = "Error";
            data.text = "Your circuit does not meet the specifications";
            buttonText.text = "Close";
        }
        transform.LeanScale(Vector2.one, 0.5f);
    }

    

    public void Close()
    {
        if (isSuccess)
        {
            transform.LeanScale(Vector2.zero, .5f).setEaseInBack().setOnComplete(Success);
        }
        else
        {
            transform.LeanScale(Vector2.zero, .5f).setEaseInBack();
        }
    }

    public void Success()
    {
        Debug.Log("Success");
        LoadingManager.instance.LoadGame(SceneIndexes.Falstad, SceneIndexes.MAP);
        PrevCurrScene.curr = 1;
    }
}
