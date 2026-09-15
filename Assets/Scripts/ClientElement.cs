using System.Collections;
using TMPro;
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
    private TMP_Text _appetizerText;
    private TMP_Text _dessertText;

    public void GetValue(Sprite clientSprite, float clientWaitingTime, int damege, Slot slot, TypeOfMealAvailable typeOfMeal)
    {
        print("Antes de verificar" + _slot);
        if(slot != _slot)
            return;
        print("Depois de verificar" + _slot);
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
            _readyAppertizer++;
        }
        else if (typeOfMeal == TypeOfMealAvailable.Dessert)
        {
            _readyDessert++;
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
        _appetizerText = GameController.Instance.AppetizerText;
        _dessertText = GameController.Instance.DessertText;
    }
    private void Update()
    {
        _appetizerText.text = _readyAppertizer.ToString();
        _dessertText.text = _readyDessert.ToString();
    }
}
