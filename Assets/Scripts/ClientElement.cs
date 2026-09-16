using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClientElement : MonoBehaviour
{
    [SerializeField] private Slot _slot;
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
    private TMP_Text _appetizerText;
    private TMP_Text _dessertText;
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
    public void GetMealsQuantityAndType(int madeMeals, TypeOfMealAvailable typeOfMeal)
    {
        print("Antes do if de pegar os meals");
        if (typeOfMeal == TypeOfMealAvailable.Appetizer)
        {
            print("Antes de pegar o appetizer " + _readyAppertizer.ToString() + " " + _slot);
            _readyAppertizer++;
            print("Depois de pegar o appetizer " + _readyAppertizer.ToString() + " " + _slot);
            _appetizerText.text = _readyAppertizer.ToString();
        }
        else if (typeOfMeal == TypeOfMealAvailable.Dessert)
        {
            _readyDessert++;
            print("Antes de pegar o dessert " + _readyDessert.ToString() + " " + _slot);
            _dessertText.text = _readyDessert.ToString();
            print("Depois de pegar o dessert " + _readyDessert.ToString() + " " + _slot);
        }
    }
    public void VerifyIfOrderIsDone()
    {
        print("Inicio de Verify");
        if(_order == 0)
            return;
        print("Antes do if");
        if(_typeOfMeals == TypeOfMealAvailable.Appetizer && _readyAppertizer >= 1)
        {
            print("Verify Appetizer");
            _appetizerText.text = _readyAppertizer.ToString();
            print("Antes de diminuir o Appertizer " + _readyAppertizer.ToString() + " " + _slot);
            _readyAppertizer -= 1;
            print("Depois de diminuir o Appertizer " + _readyAppertizer.ToString() + " " + _slot);
            _appetizerText.text = _readyAppertizer.ToString();
            OrderDone();
        }
        else if (_typeOfMeals == TypeOfMealAvailable.Dessert && _readyDessert >= 1)
        {
            print("Verify Dessert");
            _dessertText.text = _readyDessert.ToString();
            print("Antes de diminuir o Dessert " + _readyDessert.ToString() + " " + _slot);
            _readyDessert -= 1;
            print("Depois de diminuir o Dessert " + _readyDessert.ToString() + " " + _slot);
            _dessertText.text = _readyDessert.ToString();
            OrderDone();
        }
    }
    private void OrderDone()
    {
        _clientUI.color = Color.white;
        _isSlotAvalable = true;
        _orderSystem.GetConfirmationOfAvailableSlot(_slot, _isSlotAvalable);
        _order = 0;
        _clientWaitingTime = 0;
        _dessertText.text = _readyDessert.ToString();
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
        _appetizerText = GameController.Instance.AppetizerText;
        _dessertText = GameController.Instance.DessertText;
        _appetizerText.text = _readyAppertizer.ToString();
        _dessertText.text = _readyDessert.ToString();
    }
}
