using UnityEngine;

public class Store : MonoBehaviour
{
    private MealType _mealType;
    private float _money;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mealType = GameController.Instance.MealTypo;
    }
    public void StopTimeWhenOpenStore()
    {
        Time.timeScale = 0f;
    }
    public void StartTimeWhenClosingStore()
    {
        Time.timeScale = 1.0f;
    }
    public void BuyLessCookingTime()
    {
        //if
        _mealType.StoreUpgradeOvenTime();
    }
    public void UpdateMoney(float moneyQuantity)
    {
        _money = moneyQuantity;
    }
}
