using System.Collections;
using UnityEngine;

public class ClientElement : MonoBehaviour
{
    [SerializeField] private Slot _slot;
    private OrderSystem _orderSystem;
    private TypeOfMealAvailable _typeOfMeals;
    private MealType _type;
    private Sprite _clientSprite;
    private float _clientWaitingTime;
    private int _damege;
    private int _order;
    private int _readyAppertizer;
    private int _readyDessert;
    
    public void GetValue(Sprite clientSprite, float clientWaitingTime, int damege, Slot slot, TypeOfMealAvailable typeOfMeal)
    {
        if(slot != _slot)
            return;
        _clientSprite = clientSprite;
        _clientWaitingTime = clientWaitingTime;
        _damege = damege;
        _typeOfMeals = typeOfMeal;
        _order = 1;
        StartCoroutine(ClientWaitingTime());
    }
    public void GetMealsQuantityAndType(int madeMeals, TypeOfMealAvailable typeOfMeal)
    {
        if (typeOfMeal == TypeOfMealAvailable.Appetizer)
        {
            _readyAppertizer = madeMeals;
        }
        else if (typeOfMeal == TypeOfMealAvailable.Dessert)
        {
            _readyDessert = madeMeals;
        }
    }
    public void VerifyIfOrderIsDone()
    {
        if(_order == 0)
            return;
        if(_typeOfMeals == TypeOfMealAvailable.Appetizer || _readyAppertizer >= 1)
        {
            OrderDone();
        }
        else if (_typeOfMeals == TypeOfMealAvailable.Dessert || _readyDessert >= 1)
        {
            OrderDone();
        }
    }
    private void OrderDone()
    {
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
    private void Awake()
    {
        _type = GameController.Instance.MealType;
        _orderSystem = GameController.Instance.OrderSystem;
    }
}
