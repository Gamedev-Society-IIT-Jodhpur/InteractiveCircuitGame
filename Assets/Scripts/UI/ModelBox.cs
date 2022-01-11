using UnityEngine;
using TMPro;

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
        box.localPosition = new Vector2(0, -Screen.height);
        solderAvailableText = "<b>Luckily you already have a Soldering iron present in the tinkering lab</b> so you don't need to buy one. Though you can also get a breadboard from the shop " +
            "for your convenience. ";
        solderNotAvailableText = "<b>Unfortunately you don't have either of them in the tinkering lab. Don't forget to get atleast one of them from the shop.</b>";
        box.LeanMoveLocalY(0, 0.5f).setEaseOutExpo();
        
        if (StaticData.isSolderingIron)
        {
            finalSolderText = solderAvailableText;
        }
        else
        {
            finalSolderText = solderNotAvailableText;
        }
        
        panelText.text = "The gizmo requires a voltage of precisely <b>6.0 Volts</b>, and at a maximum will require power of <b>1.2 Watts</b>. The gizmo will require a standard battery(s)" +
            " as a source (the kind you buy at Quick Trip), with as high a voltage as possible. However, at least <b>60%</b> of this battery power must be delivered to the gizmo.\n\n" +
            "- Your first step is to design a circuit in a circuit simulator.\n" +
            "- Then you should go to the shop via map to buy the required components.Note that you can buy only standard components, <b>So, use only standard components in your design.</b> \n" +
            "- Then go to the Tinkering lab to fabricate your design. <b>Note that you have to make the same circuit as you designed in the circuit simulator.</b> \n"+
            "- You'll need either a breadboard or a Soldering iron to fabricate your design in the Tinkering lab. "+finalSolderText;

        //box.LeanMoveLocalY(0, 0.5f).setEaseOutExpo().delay = 0.1f;
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
        MoneyAndXPData.InitiateMoney(1000);
        MoneyXPManager.InitiateXP();
        LoadingManager.instance.LoadGame(SceneIndexes.Dialogue, SceneIndexes.Falstad);
    }

}
