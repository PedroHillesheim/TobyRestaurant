using TMPro;
using UnityEngine;

public class Store : MonoBehaviour
{
    private MealType _mealType;
    private TMP_Text _moneyText;
    private float _money;
    [SerializeField] private float _buyLessCookingTimePrize;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mealType = GameController.Instance.MealTypo;
        _moneyText = GameController.Instance.MoneyText;
        _moneyText.text = _money.ToString();
    }
    public void StopTimeWhenOpenStore()
    {
        Time.timeScale = 0f;
    }
    public void StartTimeWhenClosingStore()
    {
        print("Vamos Ver se é store");
        Time.timeScale = 1.0f;
    }
    public void BuyLessCookingTime()
    {
        if (_money >= _buyLessCookingTimePrize)
        {
            _money -= _buyLessCookingTimePrize;
            _moneyText.text = _money.ToString();
            Time.timeScale = 0f;
            _mealType.StoreUpgradeOvenTime();
            _buyLessCookingTimePrize += 50f;
        }
    }
    public void UpdateMoney(float _moneyQuantity)
    {
        _money += _moneyQuantity;
        _moneyText.text = _money.ToString();
    }
}
