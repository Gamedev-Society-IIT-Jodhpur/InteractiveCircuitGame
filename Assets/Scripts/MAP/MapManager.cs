using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MapManager : MonoBehaviour
{
    public enum scene
    {
        tinkerlab,
        falstad,
        shop1,
        shop2,
        shop3,
        shop4,
    };
    int[,] distance;
    int current = 1;
    public List<Transform> trackerpoints;
    public List<Lean.Gui.LeanTooltipData> shopstext;
    public Lean.Gui.LeanTooltipData TinkerLab;
    public Lean.Gui.LeanTooltipData falstad;
    public GameObject tracker;
    bool btnclick = false;
    string toscene = "MAP";
    static bool isfirsttime = true;
    public bool isanimating = false;
    public GameObject todisable;
    // Start is called before the first frame update
  
    void Start()
    {
        if (isfirsttime)
        {
            MoneyXPManager.IncreaseXP(100);
            isfirsttime = false;
        }
        current = PrevCurrScene.curr;
        tracker.transform.position = trackerpoints[current].position;
        distance = new int[6, 6] { { 0, 1, 2, 7, 9, 11 }, { 1, 0, 3, 8, 10, 10 }, { 2, 3, 0, 5, 7, 11 }, { 7, 8, 5, 0, 2, 6 }, { 9, 10, 7, 2, 0, 4 }, { 11, 10, 11, 6, 4, 0 } };
        for (int i = 1; i < shopstext.Count; i++)
        {

            shopstext[i].Text = "Shop" +
                "\n By Cab:     " + (160*(distance[current, i + 2] / 4) /60).ToString() + " min " + (160 * (distance[current, i + 2] / 4)%60).ToString() +" secs "+ " ₹ " + ((distance[current, i + 2]) * 60).ToString() +
                "\n  By Walking:    " + (160*distance[current, i + 2] /60).ToString() + " min "+ (160 * (distance[current, i + 2] ) % 60).ToString() + " secs " + "  ₹ 0";
        }
        TinkerLab.Text = "TinkerLab" +
                 "\n By Cab:     " + ((distance[current, 0] *40)/60).ToString()  + " min " + (160 * (distance[current, 0] / 4) % 60).ToString() + " secs " + "  ₹" + (distance[current, 0] * 60).ToString() +
                 "\n  By Walking:    " + (160 * distance[current, 0] / 60).ToString() + " min " + (160 * (distance[current, 0]) % 60).ToString() + " secs " + "  ₹ 0";
        falstad.Text = "Company : Equipped with Circuit Simulator" +
                "\n By Cab:     " + (40 * (distance[current, 1] ) / 60).ToString() + " min " + (40 * (distance[current, 1] ) % 60).ToString() + " secs " + "  ₹" + (distance[current, 1] * 60).ToString() +
                "\n  By Walking:    " + (160 * distance[current, 1] / 60).ToString() + " min " + (160 * (distance[current, 1]) % 60).ToString() + " secs " + "  ₹ 0";
    }


    public void Change(int i)
    {
        PrevCurrScene.prev = PrevCurrScene.curr;
        PrevCurrScene.curr = i;
    }

    public float deduceMoney(ButtonFunctionWrapper wrap)
    {
        if (wrap.mode == ButtonFunctionWrapper.modeOfTransportation.Cab)
        {
            //MoneyXPManager.DeductMoney(distance[current, wrap.changeindex]*60);
            return (distance[current, wrap.changeindex]*60);
        }
        else
        {
            return (0);
        }
    }

    public void mapscenechange(ButtonFunctionWrapper wrap, float animationTime)
    {
        btnclick = true;

        if (wrap.mode == ButtonFunctionWrapper.modeOfTransportation.Cab)
        {
           
            Timer.instance.SkipTime(160*(distance[current, wrap.changeindex] / 4.0f) - animationTime);
        }
        else
        {
           
            Timer.instance.SkipTime(320*(distance[current, wrap.changeindex] / 2.0f) - animationTime);
        }
        Change(wrap.changeindex);
        LoadingManager.instance.LoadGame(SceneIndexes.MAP, wrap.toScene);
    }


    public void ShowButtons(GameObject tosetActive)
    {
        if (!isanimating)
        {
            GameObject[] withTag = GameObject.FindGameObjectsWithTag("MapButton");
            foreach (var i in withTag)
            {
                i.SetActive(false);
            }

            tosetActive.SetActive(true);
            todisable = tosetActive;
        }

    }
}
