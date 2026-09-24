using System.Collections;
using TMPro;
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
    private int _quantityOfOrder;
    private int _slotInt;
    private int _readyAppertizer;
    private int _readyDessert;
    private Image _clientUI;
    private Image[] _orderDisplay;
    private Sprite[] _mealSprite;
    private Image[] _timeBar;
    private float _presentTime;
    private AudioSource _bellRing;
    public void GetValue(float clientWaitingTime, int damege, Slot slot,
    TypeOfMealAvailable typeOfMeal, int order)
    {
        if(slot != _slot)
            return;
        _bellRing.Play();
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
        _quantityOfOrder = order;
        StartCoroutine(ClientWaitingTime());
    }
    public void GetMealsValue(int appetizer, int dessert)
    {
        _readyAppertizer = appetizer;
        _readyDessert = dessert;
    }
    public void VerifyIfOrderIsDone()
    {
        
        if(_typeOfMeals == TypeOfMealAvailable.Appetizer && _readyAppertizer >= 1 && _quantityOfOrder == 1)
        {
            _type.GetMealSubstraction(_typeOfMeals);
            _orderSystem.GetClientAtended();
            OrderDone();
        }
        else if (_typeOfMeals == TypeOfMealAvailable.Dessert && _readyDessert >= 1 && _quantityOfOrder == 1)
        {
            _type.GetMealSubstraction(_typeOfMeals);
            _orderSystem.GetClientAtended();
            OrderDone();
        }
        else if (_typeOfMeals == TypeOfMealAvailable.Appetizer && _readyAppertizer >= 1 && _quantityOfOrder >= 2)
        {
            _type.GetMealSubstraction(_typeOfMeals);
            StopAllCoroutines();
            StartCoroutine(NextOrder());
        }
        else if (_typeOfMeals == TypeOfMealAvailable.Dessert && _readyDessert >= 1 && _quantityOfOrder >= 2)
        {
            _type.GetMealSubstraction(_typeOfMeals);
            StopAllCoroutines();
            StartCoroutine(NextOrder());
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
        _quantityOfOrder = 0;
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
    private IEnumerator NextOrder()
    {
        _quantityOfOrder--;
        _timeBar[_slotInt].fillAmount = 1;
        _orderDisplay[_slotInt].enabled = false;
        _clientUI.color = Color.white;
        yield return new WaitForSeconds(2.3f);
        _bellRing.Play();
        int randomMeal = Random.Range(1, 3);
        _typeOfMeals = (TypeOfMealAvailable)randomMeal;
        _orderDisplay[_slotInt].enabled = true;
        _clientUI.color = Color.gold;
        if (_typeOfMeals == TypeOfMealAvailable.Appetizer)
        {
            _orderDisplay[_slotInt].sprite = _mealSprite[0];
        }
        else if (_typeOfMeals == TypeOfMealAvailable.Dessert)
        {
            _orderDisplay[_slotInt].sprite = _mealSprite[1];
        }
        StartCoroutine(ClientWaitingTime());
    }
    private void Start()
    {
        _clientUI = GetComponent<Image>();
        _type = GameController.Instance.MealTypo;
        _orderSystem = GameController.Instance.OrderSystem;
        _clientSystem = GameController.Instance.ClientSystem;
        _orderDisplay = GameController.Instance.OrderDisplay;
        _mealSprite = GameController.Instance.MealSprite;
        _bellRing = GameController.Instance.Bell;
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
