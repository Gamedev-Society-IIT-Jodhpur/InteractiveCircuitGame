using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ModelBox : MonoBehaviour
{
    public Transform box;
    public CanvasGroup background;
    [SerializeField] TMP_Text panelText;
    string solderAvailableText;
    string solderNotAvailableText;
    string finalSolderText;

    private void OnEnable()
    {
        background.alpha = 0;
        background.LeanAlpha(1, 0.5f);
        //print(SceneManager.GetSceneAt(1).name);
        if (SceneManager.GetSceneAt(1).name == "Dialogue")
        {
            solderAvailableText = "Soldering Iron (01)";
            solderNotAvailableText = "Sorry, the inventory is empty.";
            box.LeanMoveLocalY(0, 0.5f).setEaseOutExpo();

            if (StaticData.isSolderingIron)
            {
                finalSolderText = solderAvailableText;
            }
            else
            {
                finalSolderText = solderNotAvailableText;
            }

            panelText.text = "The gizmo requires a voltage of precisely <b>6.0 Volts</b>, and at a maximum will require a power of " +
                "<b>1.2 Watts</b>. It should use standard components (the kind you can buy at any electronics store), with <b>as high a source</b>" +
                " <b>voltage as possible</b>. However, <b>at least 60% of this battery power</b> must be delivered to it. The regulator must " +
                "likewise be simple and cheap.\n\n" +
                "<b>Available in Inventory of Tinkering Lab</b>\n" + finalSolderText;
        }
        else
        {
            solderAvailableText = "Soldering Iron (01)";
            solderNotAvailableText = "Sorry, the inventory is empty.";
            box.LeanMoveLocalY(0, 0.5f).setEaseOutExpo();

            if (StaticData.isSolderingIron)
            {
                finalSolderText = solderAvailableText;
            }
            else
            {
                finalSolderText = solderNotAvailableText;
            }

            panelText.text = "The gizmo requires a voltage of precisely <b>6.0 Volts</b>, and at a maximum will require a power of " +
                "<b>1.2 Watts</b>. It should use standard components (the kind you can buy at any electronics store), with <b>as high a source</b>" +
                " <b>voltage as possible</b>. However, <b>at least 60% of this battery power</b> must be delivered to it. The regulator must " +
                "likewise be simple and cheap.\n\n" +
                "<b>Available in Inventory of Tinkering Lab</b>\n" + finalSolderText;
        }
        //box.localPosition = new Vector2(0, -Screen.height);

        box.LeanMoveLocalY(0, 0.5f).setEaseOutExpo().delay = 0.8f;
    }

    public void CloseDialogue()
    {
        background.LeanAlpha(0, 0.5f);
        box.LeanMoveLocalY(-2160, 1.0f).setEaseOutExpo().setOnComplete(OnComplete);
    }

    void OnComplete()
    {
        gameObject.SetActive(false);
    }

    public void StartQuest()
    {
        MoneyAndXPData.InitiateMoney(10000);
        MoneyXPManager.InitiateXP();
        LoadingManager.instance.LoadGame(SceneIndexes.Dialogue, SceneIndexes.Falstad);
    }

}
