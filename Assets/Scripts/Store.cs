using TMPro;
using UnityEngine;

public class Store : MonoBehaviour
{
    private MealType _mealType;
    private TMP_Text _moneyText;
    private float _money;
    [SerializeField] private float _buyLessCookingTimePrize;
    private TMP_Text _prizeForCookingtimeText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mealType = GameController.Instance.MealTypo;
        _moneyText = GameController.Instance.MoneyText;
        _prizeForCookingtimeText = GameController.Instance.PrizeForCookingtimeText;
        _prizeForCookingtimeText.text = _buyLessCookingTimePrize.ToString() + "$";
        _moneyText.text = _money.ToString() + "$";
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
        if (_money >= _buyLessCookingTimePrize)
        {
            _money -= _buyLessCookingTimePrize;
            _moneyText.text = _money.ToString();
            Time.timeScale = 0f;
            _mealType.StoreUpgradeOvenTime();
            _buyLessCookingTimePrize += 50f;
            _prizeForCookingtimeText.text = _buyLessCookingTimePrize.ToString() + "$";
        }
    }
    public void UpdateMoney(float _moneyQuantity)
    {
        _money += _moneyQuantity;
        _moneyText.text = _money.ToString() + "$";
    }
}
