using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClientElement : MonoBehaviour
{
    [SerializeField] private Slot _slot;
    private ClientSystem _clientSystem;
    private OrderSystem _orderSystem;
    private TypeOfMealAvailable _typeOfMeals;
    private MealType _type;
    private Sprite _clientSprite;
    private float _clientWaitingTime;
    private bool _isSlotAvalable = true;
    private int _damege;
    private int _order;
    private int _slotInt;
    private int _readyAppertizer;
    private int _readyDessert;
    private Image _clientUI;
    private Image[] _orderDisplay;
    private Sprite[] _mealSprite;
    private Image[] _timeBar;
    private float _presentTime;
    public void GetValue(/*Sprite clientSprite,*/ float clientWaitingTime, int damege, Slot slot, TypeOfMealAvailable typeOfMeal)
    {
        if(slot != _slot)
            return;
        _orderDisplay[_slotInt].enabled = true;
        _typeOfMeals = typeOfMeal;
        _isSlotAvalable = false;
        _clientUI.color = Color.gold;
        if (typeOfMeal == TypeOfMealAvailable.Appetizer)
        {
            _orderDisplay[_slotInt].sprite = _mealSprite[0];
        }
        else if (typeOfMeal == TypeOfMealAvailable.Dessert)
        {
            _orderDisplay[_slotInt].sprite = _mealSprite[1];
        }
        //_clientSprite = clientSprite;
        _clientWaitingTime = clientWaitingTime;
        _presentTime = _clientWaitingTime;
        _damege = damege;
        _typeOfMeals = typeOfMeal;
        _order = 1;
        StartCoroutine(ClientWaitingTime());
    }
    public void GetMealsValue(int appetizer, int dessert)
    {
        _readyAppertizer = appetizer;
        _readyDessert = dessert;
    }
    public void VerifyIfOrderIsDone()
    {
        if(_order == 0)
            return;
        if(_typeOfMeals == TypeOfMealAvailable.Appetizer && _readyAppertizer >= 1)
        {
            _type.GetMealSubstraction(_typeOfMeals);
            OrderDone();
        }
        else if (_typeOfMeals == TypeOfMealAvailable.Dessert && _readyDessert >= 1)
        {
            _type.GetMealSubstraction(_typeOfMeals);
            OrderDone();
        }
    }
    private void OrderDone()
    {
        StopAllCoroutines();
        _orderDisplay[_slotInt].enabled = false;
        _clientUI.color = Color.white;
        _isSlotAvalable = true;
        _orderSystem.GetConfirmationOfAvailableSlot(_slot, _isSlotAvalable);
        _orderSystem.ClientAtended(_slot);
        _order = 0;
        _clientWaitingTime = 0;
        _timeBar[_slotInt].fillAmount = 1f;
        _presentTime = _clientWaitingTime;
        _damege = 0;
    }
    private IEnumerator ClientWaitingTime()
    {
        _presentTime = _clientWaitingTime;

        while (_presentTime > 0)
        {
            _presentTime -= Time.deltaTime;

            _timeBar[_slotInt].fillAmount =
                _presentTime / _clientWaitingTime;

            yield return null;
        }

        _presentTime = 0;
        _timeBar[_slotInt].fillAmount = 0;

        _orderSystem.TakeDamege(_damege);
        OrderDone();
    }
    private void Start()
    {
        _clientUI = GetComponent<Image>();
        _type = GameController.Instance.MealTypo;
        _orderSystem = GameController.Instance.OrderSystem;
        _clientSystem = GameController.Instance.ClientSystem;
        _orderDisplay = GameController.Instance.OrderDisplay;
        _mealSprite = GameController.Instance.MealSprite;
        _timeBar = GameController.Instance.TimeBar;
        if (_slot == Slot.Slot1)
        {
            _slotInt = 0;
        }
        else if (_slot == Slot.Slot2)
        {
            _slotInt = 1;
        }
        else if (_slot == Slot.Slot3)
        {
            _slotInt = 2;
        }
        else if ( _slot == Slot.Slot4)
        {
            _slotInt = 3;
        }
        _timeBar[_slotInt].fillAmount = 1f;
    }
}
