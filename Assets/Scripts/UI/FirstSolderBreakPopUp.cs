using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirstSolderBreakPopUp : MonoBehaviour
{
    // Start is called before the first frame update

    public static bool isSuccess = false;
    public static FirstSolderBreakPopUp Instance;

    [SerializeField]
    TMP_Text title;
    [SerializeField]
    TMP_Text data;
    [SerializeField]
    Text buttonText;

    public NodeTinker currentSolderedNode;
    bool isBreadboardPresent = false;
    [SerializeField] GameObject closeButton;
    public delegate void delFunction();
    delFunction del;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        transform.localScale = Vector2.zero;
        foreach (var item in StaticData.Inventory)
        {
            if (item.name == "breadboard")
            {
                isBreadboardPresent = true;
                break;
            }
        }

        if (!isBreadboardPresent && !StaticData.isSolderingIron)
        {
            title.text = "Warning";
            data.text = "You don't have either breadboard or soldering iron.\nGo back to the shop to purchase either one of them.";
            buttonText.text = "Go to Shop";
            closeButton.SetActive(false);
            transform.LeanScale(Vector2.one, 0.5f);
        }
    }

    public void Open(delFunction function, string dataText, string titleText = "Warning", string buttonText = "Continue")
    {

        title.text = titleText;
        data.text = dataText;
        this.buttonText.text = buttonText;
        del = function;
        closeButton.SetActive(true);
        transform.LeanScale(Vector2.one, 0.5f);
        //Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void Close()
    {
        transform.LeanScale(Vector2.zero, .5f).setEaseInBack();
    }

    public void Continue()
    {
        if (!isBreadboardPresent && !StaticData.isSolderingIron)
        {
            transform.LeanScale(Vector2.zero, .5f).setEaseInBack().setOnComplete(GoToMap);
        }
        else
        {
            transform.LeanScale(Vector2.zero, .5f).setEaseInBack();
            StaticData.hasSolderBroken = true;
            del();
            //currentSolderedNode.BreakSoldered();
        }



    }

    void GoToMap()
    {
        LoadingManager.instance.LoadGame(SceneIndexes.Tinker, SceneIndexes.MAP);
        PrevCurrScene.curr = 0;
    }




}
