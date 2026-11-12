using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Store : MonoBehaviour
{
    private MealType _mealType;
    private OrderSystem _orderSystem;
    private ClientElement[] _clientElement;
    private TMP_Text _moneyText;
    private float _money;
    private int _quantityOfTimeOfBuyingInflation;
    [Header("Prize")]
    [SerializeField] private float _buyLessCookingTimePrize;
    [SerializeField] private float _buyClientPaciencePrize;
    [SerializeField] private float _buyInflatingPrize;
    [Header("Text")]
    private TMP_Text _prizeForCookingtimeText;
    [SerializeField] private TMP_Text _prizeForClientPacienceText;
    [SerializeField] private TMP_Text _prizeForInflationText;
    [Header("Event")]
    [SerializeField] private UnityEvent _pacienceEvent;
    [SerializeField] private UnityEvent _lessCookingEvent;
    [SerializeField] private UnityEvent _endOfInflationEvent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mealType = GameController.Instance.MealTypo;
        _orderSystem = GameController.Instance.OrderSystem;
        _moneyText = GameController.Instance.MoneyText;
        _prizeForCookingtimeText = GameController.Instance.PrizeForCookingtimeText;
        _prizeForCookingtimeText.text = _buyLessCookingTimePrize.ToString() + "$";
        _moneyText.text = _money.ToString() + "$";
        _prizeForClientPacienceText.text = _buyClientPaciencePrize.ToString() + "$";
        _prizeForInflationText.text = _buyInflatingPrize.ToString() + "$";
        for (int i = 0; i <= 3; i++)
        {
            _clientElement = GameController.Instance.ClientsElements;
        }
    }
    public void StopTimeWhenOpenStore()
    {
        Time.timeScale = 0f;
    }
    public void StartTimeWhenClosingStore()
    {
        Time.timeScale = 1.0f;
    }
    public void BuyClientMorePacience()
    {
        if(_money >= _buyClientPaciencePrize)
        {
            _money -= _buyClientPaciencePrize;
            _moneyText.text = _money.ToString();
            _buyClientPaciencePrize += 50;
            _prizeForClientPacienceText.text = _buyClientPaciencePrize.ToString() + "$";
            _orderSystem.UpgradeClientPacience(0.2f);
        }
    }
    public void NoMoreClientPacience()
    {
        _prizeForClientPacienceText = null;
        _pacienceEvent.Invoke();
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
    public void NoLessCookingTime()
    {
        _prizeForCookingtimeText = null;
        _lessCookingEvent.Invoke();
    }
    public void MoreMoneyForAtending()
    {
        if(_money >= _buyInflatingPrize)
        {
            _money -= _buyInflatingPrize;
            _moneyText.text = _money.ToString();
            _buyInflatingPrize += 50;
            _prizeForInflationText.text = _buyInflatingPrize.ToString() + "$";
            _quantityOfTimeOfBuyingInflation++;
            for (int i = 0; i <= 3; i++)
            {
                _clientElement[i].Inflation();
            }
            if (_quantityOfTimeOfBuyingInflation >= 3)
            {
                NoMoreInflation();
            }
        }
    }
    public void NoMoreInflation()
    {
        _prizeForInflationText = null;
        _endOfInflationEvent.Invoke();
    }
    public void UpdateMoney(float _moneyQuantity)
    {
        _money += _moneyQuantity;
        _moneyText.text = _money.ToString() + "$";
    }
}
