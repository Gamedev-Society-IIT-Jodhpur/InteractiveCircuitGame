using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        transform.localScale = Vector2.zero;
        foreach(var item in StaticData.Inventory)
        {
            if (item.name== "breadboard")
            {
                isBreadboardPresent = true;
                break;
            }
        }

        if(!isBreadboardPresent && !StaticData.isSolderingIron)
        {
            title.text = "Warning";
            data.text = "You don't have either breadboard or soldering iron.\nGo back to the shop to purchase either one of them.";
            buttonText.text = "Go to Shop";
            closeButton.SetActive(false);
            transform.LeanScale(Vector2.one, 0.5f);
        }
    }

    public void Open(NodeTinker currentNode)
    {

        title.text = "Warning";
        data.text = "You are about to break soldered components.\nIt'll cost you XP.";
        buttonText.text = "Continue";
        currentSolderedNode = currentNode;
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
            currentSolderedNode.BreakSoldered();
        }

        

    }

    void GoToMap()
    {
        LoadingManager.instance.LoadGame(SceneIndexes.Tinker, SceneIndexes.MAP);
        PrevCurrScene.curr = 1;
    }

    
}
