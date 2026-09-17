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
    private int _order;
    private int _readyAppertizer;
    private int _readyDessert;
    private Image _clientUI;

    public void GetValue(/*Sprite clientSprite,*/ float clientWaitingTime, int damege, Slot slot, TypeOfMealAvailable typeOfMeal)
    {
        if(slot != _slot)
            return;
        _typeOfMeals = typeOfMeal;
        _isSlotAvalable = false;
        _clientUI.color = Color.gold;
        print(typeOfMeal + " " + _slot);
        //_clientSprite = clientSprite;
        _clientWaitingTime = clientWaitingTime;
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
        _clientUI.color = Color.brown;
        _isSlotAvalable = true;
        _orderSystem.GetConfirmationOfAvailableSlot(_slot, _isSlotAvalable);
        _order = 0;
        _clientWaitingTime = 0;
        _damege = 0;
    }
    private IEnumerator ClientWaitingTime()
    {
        yield return new WaitForSeconds(_clientWaitingTime);
        _orderSystem.TakeDamege(_damege);
        OrderDone();
    }
    private void Start()
    {
        _clientUI = GetComponent<Image>();
        _type = GameController.Instance.MealTypo;
        _orderSystem = GameController.Instance.OrderSystem;
        _clientSystem = GameController.Instance.ClientSystem;
    }
}
