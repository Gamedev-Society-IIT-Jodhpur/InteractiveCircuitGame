using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyXPManager : MonoBehaviour
{
    static TMP_Text moneyText;
    static TMP_Text xpText;

    private void Start()
    {
        moneyText = GetComponentsInChildren<TMP_Text>()[0];
        xpText = GetComponentsInChildren<TMP_Text>()[1];
        UpdateMoneyAndXPText();
    }

    public void InitiateMoney(float amount)
    {
        MoneyAndXPData.InitiateMoney(amount);
        UpdateMoneyAndXPText();
    }

    public static void InitiateXP(float value)
    {
        MoneyAndXPData.InitiateXP(value);
        UpdateMoneyAndXPText();
    }

    public static void DeductMoney(float amount)
    {
        MoneyAndXPData.money -= amount;
        UpdateMoneyAndXPText();
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            IncreaseXP(100);
            AddMoney(5);
        }
    }
}
