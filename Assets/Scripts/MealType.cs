using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum TypeOfMealAvailable
{
    Appetizer = 1,
    Dessert = 2
}
public class MealType : MonoBehaviour
{
    [SerializeField] private ClientSystem _clientSystem;
    private TypeOfMealAvailable typeOfMealAvailable;
    [SerializeField] private float _timeOfCooking;
    private ClientElement[] _clients;
    private int _readyMadeMeals = 0;
    [SerializeField] private int _maxOfReadyMadeMeals;
    private int _appetizerReady;
    private int _dessertReady;
    private TMP_Text _appetizerText;
    private TMP_Text _dessertText;
    private TMP_Text _alertText;
    private bool _isCookingAppetizer;
    private bool _isCookingDessert;
    private Button _appetizerButton;
    private Button _dessertButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _clients = GameController.Instance.ClientsElements;
        _appetizerButton = GameController.Instance.AppetizerButton;
        _dessertButton = GameController.Instance.DessertButton;
        _appetizerText = GameController.Instance.AppetizerText;
        _dessertText = GameController.Instance.DessertText;
        _alertText = GameController.Instance.AlertText;
        _appetizerText.text = _appetizerReady.ToString() + "/" + _maxOfReadyMadeMeals.ToString();
        _dessertText.text = _dessertReady.ToString() + "/" + _maxOfReadyMadeMeals.ToString();
    }
    public void StartCooking(int type)
    {
        StartCoroutine(CookingMeal((TypeOfMealAvailable)type));
    }
    private IEnumerator CookingMeal(TypeOfMealAvailable type)
    {
        if (type == TypeOfMealAvailable.Appetizer && _isCookingAppetizer == false)
        {
            if (_appetizerReady >= _maxOfReadyMadeMeals)
            {
                _alertText.text = "You alredy full of appetizer";
                yield break;
            }
            _isCookingAppetizer = true;
            _appetizerButton.interactable = false;
            yield return new WaitForSeconds(_timeOfCooking);
            _appetizerButton.interactable = true;
            _isCookingAppetizer = false;
            _appetizerReady++;
            if (_appetizerReady >= _maxOfReadyMadeMeals)
            {
                _appetizerButton.interactable = false;
            }
        }
        else if (type == TypeOfMealAvailable.Dessert && _isCookingDessert == false)
        {
            if (_dessertReady >= _maxOfReadyMadeMeals)
            {
                _alertText.text = "You alredy full of dessert";
                yield break;
            }
            _isCookingDessert = true;
            _dessertButton.interactable = false;
            yield return new WaitForSeconds(_timeOfCooking);
            _dessertButton.interactable = true;
            _isCookingDessert = false;
            _dessertReady++;
            if (_dessertReady >= _maxOfReadyMadeMeals)
            {
                _dessertButton.interactable = false;
            }
        }
        _appetizerText.text = _appetizerReady.ToString() + "/" + _maxOfReadyMadeMeals.ToString();
        _dessertText.text = _dessertReady.ToString() + "/" + _maxOfReadyMadeMeals.ToString();
        for (int i = 0; i < _clients.Length; i++)
        {
            _clients[i].GetMealsValue(_appetizerReady, _dessertReady);
        }
    }
    public void GetMealSubstraction(TypeOfMealAvailable typeOfMeal)
    {
        if (typeOfMeal == TypeOfMealAvailable.Appetizer)
        {
            _appetizerReady--;
            _appetizerButton.interactable = true;
        }
        else if (typeOfMeal == TypeOfMealAvailable.Dessert)
        {
            _dessertReady--;
            _dessertButton.interactable = true;
        }
        _appetizerText.text = _appetizerReady.ToString() + "/" + _maxOfReadyMadeMeals.ToString();
        _dessertText.text = _dessertReady.ToString() + "/" + _maxOfReadyMadeMeals.ToString();
        for (int i = 0; i < _clients.Length; i++)
        {
            _clients[i].GetMealsValue(_appetizerReady, _dessertReady);
        }
    }
}
