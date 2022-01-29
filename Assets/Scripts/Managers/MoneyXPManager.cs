using TMPro;
using UnityEngine;


public class MoneyXPManager : MonoBehaviour
{
    static TMP_Text moneyText;
    static TMP_Text xpText;
    [SerializeField] GameObject popUp;
    static bool popUpOpen = false;
    [SerializeField] AudioClip moneyDeductEffectReference;
    static AudioClip moneyDeductEffect;
    static AudioSource audioSource;

    private void Start()
    {

        moneyText = GetComponentsInChildren<TMP_Text>()[0];
        xpText = GetComponentsInChildren<TMP_Text>()[1];
        UpdateMoneyAndXPText();
        audioSource = GetComponentInChildren<AudioSource>();
        moneyDeductEffect = moneyDeductEffectReference;
    }

    public static void InitiateMoney(float amount)
    {
        MoneyAndXPData.InitiateMoney(amount);
        UpdateMoneyAndXPText();
    }

    public static void InitiateXP()
    {
        MoneyAndXPData.InitiateXP(PlayerPrefs.GetInt("player_xp", 0));
        //UpdateMoneyAndXPText();
    }
    public static void OpenPopUp()
    {
        popUpOpen = true;
    }

    public static void DeductMoney(float amount)
    {
        //print(amount);
        if (MoneyAndXPData.money - amount >= 0)
        {
            MoneyAndXPData.money -= amount;
            UpdateMoneyAndXPText();
            audioSource.PlayOneShot(moneyDeductEffect);


        }
        else
        {
            popUpOpen = true;
        }
    }

    public static void DeductXP(float value)
    {
        MoneyAndXPData.xp -= value;
        UpdateMoneyAndXPText();
    }

    public static void AddMoney(float amount)
    {
        MoneyAndXPData.money += amount;
        UpdateMoneyAndXPText();
    }

    public static void IncreaseXP(float value)
    {
        MoneyAndXPData.xp += value;
        UpdateMoneyAndXPText();
    }

    static void UpdateMoneyAndXPText()
    {
        moneyText.text = /*"Money Left: \u20A8 " + */MoneyAndXPData.money.ToString();
        xpText.text = /*"XP: "+ */MoneyAndXPData.xp.ToString();
    }

    private void Update()
    {
        if (popUpOpen)
        {
            popUpOpen = false;
            popUp.GetComponent<GameOverPopUp>().Open();

        }
    }


}
