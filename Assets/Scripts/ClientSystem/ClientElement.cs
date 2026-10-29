using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClientElement : MonoBehaviour
{
    [SerializeField] private Slot _slot;
    private ClientSystem _clientSystem;
    private ClientsType _clientType;
    private OrderSystem _orderSystem;
    private EventSystem eventSystem;
    private RestaurantLife _restaurantLife;
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
    private bool _isWaitinPacience;
    private float _appetizerPrize;
    private float _dessertPrize;
    private float _money;
    private Store _store;
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
        _restaurantLife = GameController.Instance.RestaurantLife;
        eventSystem = GameController.Instance.EventSystem;
        _store = GameController.Instance.Store;
        _appetizerPrize = GameController.Instance.AppetizerPrize;
        _dessertPrize = GameController.Instance.DessertPrize;
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
        _timeBar[_slotInt].fillAmount = 0f;
    }
    public void GetValue(float clientWaitingTime, int damege, Slot slot,
    TypeOfMealAvailable typeOfMeal, int order, ClientsType clientsType)
    {
        if(slot != _slot)
            return;
        _bellRing.Play();
        _orderDisplay[_slotInt].enabled = true;
        _typeOfMeals = typeOfMeal;
        _clientType = clientsType;
        _isSlotAvalable = false;
        _clientUI.color = Color.gold;
        if (_clientType == ClientsType.Monster)
        {
            _clientUI.color = Color.red;
        }
        if (typeOfMeal == TypeOfMealAvailable.Appetizer)
        {
            _orderDisplay[_slotInt].sprite = _mealSprite[0];
        }
        else if (typeOfMeal == TypeOfMealAvailable.Dessert)
        {
            _orderDisplay[_slotInt].sprite = _mealSprite[1];
        }
        //_clientSprite = clientSprite;
        _isWaitinPacience = true;
        _clientWaitingTime = clientWaitingTime;
        _presentTime = 0;
        _damege = damege;
        _typeOfMeals = typeOfMeal;
        _quantityOfOrder = order;
    }
    public void ResetScene()
    {
        print(_isSlotAvalable + " " + _slot);
        StopAllCoroutines();
        _typeOfMeals = TypeOfMealAvailable.None;
        _clientType = ClientsType.None;
        _clientUI.color = Color.white;
        _isSlotAvalable = true;
        _orderSystem.ClientAtended(_slot);
        _isWaitinPacience = false;
        _clientWaitingTime = 0;
        _presentTime = 0;
        _damege = 0;
        _quantityOfOrder = 0;
        _orderDisplay[_slotInt].enabled = false;
        _timeBar[_slotInt].fillAmount = 0;
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
            _store.UpdateMoney(_appetizerPrize);
            OrderDone();
        }
        else if (_typeOfMeals == TypeOfMealAvailable.Dessert && _readyDessert >= 1 && _quantityOfOrder == 1)
        {
            _type.GetMealSubstraction(_typeOfMeals);
            _store.UpdateMoney(_dessertPrize);
            OrderDone();
        }
        else if (_typeOfMeals == TypeOfMealAvailable.Appetizer && _readyAppertizer >= 1 && _quantityOfOrder >= 2)
        {
            _type.GetMealSubstraction(_typeOfMeals);
            _store.UpdateMoney(_appetizerPrize);
            StopAllCoroutines();
            StartCoroutine(NextOrder());
        }
        else if (_typeOfMeals == TypeOfMealAvailable.Dessert && _readyDessert >= 1 && _quantityOfOrder >= 2)
        {
            _type.GetMealSubstraction(_typeOfMeals);
            _store.UpdateMoney(_dessertPrize);
            StopAllCoroutines();
            StartCoroutine(NextOrder());
        }
    }
    private void OrderDone()
    {
        _isWaitinPacience = false;
        StopAllCoroutines();
        _orderDisplay[_slotInt].enabled = false;
        _clientUI.color = Color.white;
        _isSlotAvalable = true;
        _orderSystem.ClientAtended(_slot);
        _quantityOfOrder = 0;
        _clientWaitingTime = 0;
        _timeBar[_slotInt].fillAmount = 0f;
        _presentTime = _clientWaitingTime;
        _damege = 0;
        if (_clientType == ClientsType.Influencer)
        {
            _restaurantLife.Heal();
            _restaurantLife.ClientAtended();
        }
        else if(_clientType == ClientsType.Critic)
        {
            _type.Motivation();
            _restaurantLife.ClientAtended();
        }
        else if (_clientType == ClientsType.Monster)
        {
            _restaurantLife.TakeDamege(_damege);
            eventSystem.Blur();
        }
        else
        {
            _restaurantLife.ClientAtended();
        }
    }
    private void Update()
    {
        if (_isWaitinPacience == true)
        {
            _presentTime += Time.deltaTime;
            _timeBar[_slotInt].fillAmount =
                _presentTime / _clientWaitingTime;

            
            if (_presentTime >= _clientWaitingTime)
            {
                _presentTime = _clientWaitingTime;
                _timeBar[_slotInt].fillAmount = 1f;
                if(_clientType == ClientsType.Monster)
                {
                    _isWaitinPacience = false;
                    _orderDisplay[_slotInt].enabled = false;
                    _clientUI.color = Color.white;
                    _isSlotAvalable = true;
                    _orderSystem.ClientAtended(_slot);
                    _quantityOfOrder = 0;
                    _clientWaitingTime = 0;
                    _timeBar[_slotInt].fillAmount = 0f;
                    _presentTime = _clientWaitingTime;
                    _damege = 0;
                }  
                else if (_clientType == ClientsType.Karen)
                {
                    _restaurantLife.TakeDamege(_damege);
                    StartCoroutine(KarenBeingKaren());
                    _isWaitinPacience = false;
                }
                else if (_clientType == ClientsType.None)
                {
                    _isSlotAvalable = false;
                    _isWaitinPacience = false;
                    ResetScene();
                }
                else
                {
                    _restaurantLife.TakeDamege(_damege);
                    OrderDone();
                    _isWaitinPacience = false;
                }
            }
        }
    }
    private IEnumerator KarenBeingKaren()
    {
        _clientUI.color = Color.black;
        _orderDisplay[_slotInt].enabled = false;
        _type.Desmotivation();
        yield return new WaitForSeconds(3f);
        OrderDone();
    }
    private IEnumerator NextOrder()
    {
        _isWaitinPacience = false;
        _presentTime = 0;
        _quantityOfOrder--;
        _timeBar[_slotInt].fillAmount = 0f;
        _orderDisplay[_slotInt].enabled = false;
        _clientUI.color = Color.orange;
        _typeOfMeals = TypeOfMealAvailable.None;
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
        _isWaitinPacience = true;
    }
}
