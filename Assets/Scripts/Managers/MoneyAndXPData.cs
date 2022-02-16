public static class MoneyAndXPData
{
    public static float money=10;
    public static float xp;

    public static void InitiateMoney(float amount)
    {
        money = amount;
    }

    public static void InitiateXP(float value)
    {
        xp = value;
    }

    public static void DeductMoney(float amount)
    {
        money -= amount;
    }

    public static void DeductXP(float value)
    {
        xp -= value;
    }

    public static void AddMoney(float amount)
    {
        money += amount;
    }

    public static void IncreaseXP(float value)
    {
        xp += value;
    }
}
