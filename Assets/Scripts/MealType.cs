using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum TypeOfMealAvailable
{
    None,
    Appetizer = 1,
    Dessert = 2
}
public class MealType : MonoBehaviour
{
    [SerializeField] private ClientSystem _clientSystem;
    private TypeOfMealAvailable typeOfMealAvailable;
    [SerializeField] private float _timeOfCooking;
    private ClientElement[] _clients;
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
    private float _presentTimeAppetizer;
    private float _presentTimeDessert;
    private float _presentTimeMotivation;
    private float _presentTimeDesmotivation;
    [SerializeField] private float _motivationDuration;
    [SerializeField] private float _desmotivationDuration;
    private Image[] _timeBarMeals;
    private Image _motivationTimeBar;
    private Image _desmotivationTimeBar;
    private bool _karenAtack = false;
    [SerializeField] private float _timeOfCookingTimeDecreasing;
    private TMP_Text _timeOfCookingBeingOnTextShop;
    [SerializeField] private UnityEvent _disableButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _clients = GameController.Instance.ClientsElements;
        _appetizerButton = GameController.Instance.AppetizerButton;
        _dessertButton = GameController.Instance.DessertButton;
        _appetizerText = GameController.Instance.AppetizerText;
        _dessertText = GameController.Instance.DessertText;
        _alertText = GameController.Instance.AlertText;
        _timeBarMeals = GameController.Instance.TimeBarMeals;
        _motivationTimeBar = GameController.Instance.MotivationTimeBar;
        _desmotivationTimeBar = GameController.Instance.DesmotivationTimeBar;
        _appetizerText.text = _appetizerReady.ToString() + "/" + _maxOfReadyMadeMeals.ToString();
        _dessertText.text = _dessertReady.ToString() + "/" + _maxOfReadyMadeMeals.ToString();
        _timeOfCookingBeingOnTextShop = GameController.Instance.TimeOfCookingBeingOnTextShop;
        float _timeOfOvenAfterUpdate = _timeOfCooking - _timeOfCookingTimeDecreasing;
        _timeOfCookingBeingOnTextShop.text = $"{_timeOfCooking.ToString()} = {_timeOfOvenAfterUpdate.ToString()}";
    }
    public void ResetScene()
    {
        _karenAtack = false;
        _timeBarMeals[0].fillAmount = 0;
        _appetizerReady = 0;
        _dessertReady = 0;
        _timeBarMeals[1].fillAmount = 0;
    }
public void StartCooking(int type)
    {
        StartCoroutine(CookingMeal((TypeOfMealAvailable)type));
    }
    private IEnumerator CookingMeal(TypeOfMealAvailable type)
    {
        if (_karenAtack == true) { }
        else if (type == TypeOfMealAvailable.Appetizer && _isCookingAppetizer == false)
        {
            if (_appetizerReady >= _maxOfReadyMadeMeals)
            {
                _alertText.text = "You alredy full of appetizer";
                yield break;
            }
            _isCookingAppetizer = true;
            _appetizerButton.interactable = false;

            _timeBarMeals[0].fillAmount = 1;
            _presentTimeAppetizer = _timeOfCooking;
            while (_presentTimeAppetizer > 0)
            {
                _presentTimeAppetizer -= Time.deltaTime;

                _timeBarMeals[0].fillAmount = _presentTimeAppetizer / _timeOfCooking;

                yield return null;
            }
            _presentTimeAppetizer = 0;
            _timeBarMeals[0].fillAmount = 0;

            _appetizerButton.interactable = true;
            _isCookingAppetizer = false;
            _appetizerReady++;
            _appetizerButton.interactable = true;
            if (_appetizerReady >= _maxOfReadyMadeMeals)
            {
                _presentTimeAppetizer = 0;
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

            _timeBarMeals[1].fillAmount = 1;
            _presentTimeDessert = _timeOfCooking;
            while (_presentTimeDessert > 0)
            {
                _presentTimeDessert -= Time.deltaTime;

                _timeBarMeals[1].fillAmount = _presentTimeDessert / _timeOfCooking;

                yield return null;
            }
            _presentTimeDessert = 0;
            _timeBarMeals[1].fillAmount = 0;

            _dessertButton.interactable = true;
            _isCookingDessert = false;
            _dessertReady++;
            if (_dessertReady >= _maxOfReadyMadeMeals)
            {
                _presentTimeDessert = 0;
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
            if(_presentTimeAppetizer > 0)
            {
                _appetizerButton.interactable = false;
            }
            else
            {
                _appetizerButton.interactable = true;
            }
        }
        else if (typeOfMeal == TypeOfMealAvailable.Dessert)
        {
            _dessertReady--;
            if (_presentTimeDessert > 0)
            {
                _dessertButton.interactable = false;
            }
            else
            {
                _dessertButton.interactable = true;
            }
        }
        _appetizerText.text = _appetizerReady.ToString() + "/" + _maxOfReadyMadeMeals.ToString();
        _dessertText.text = _dessertReady.ToString() + "/" + _maxOfReadyMadeMeals.ToString();
        for (int i = 0; i < _clients.Length; i++)
        {
            _clients[i].GetMealsValue(_appetizerReady, _dessertReady);
        }
    }
    public void Motivation()
    {
        StartCoroutine(MotivationDuration());
    }
    public void Desmotivation()
    {
        StartCoroutine(DesmotivationDuration());
    }
    private IEnumerator MotivationDuration()
    {
        _timeOfCooking /= 2;
        _motivationTimeBar.fillAmount = 1;
        _presentTimeMotivation = _motivationDuration;
        while (_presentTimeMotivation > 0)
        {
            _presentTimeMotivation -= Time.deltaTime;

            _motivationTimeBar.fillAmount = _presentTimeMotivation / _motivationDuration;

            yield return null;
        }
        _presentTimeMotivation = 0;
        _motivationTimeBar.fillAmount = 0;
        _timeOfCooking *= 2;
    }
    private IEnumerator DesmotivationDuration()
    {
        _karenAtack = true;
        _desmotivationTimeBar.fillAmount = 1;
        _presentTimeDesmotivation = _desmotivationDuration;
        while (_presentTimeDesmotivation > 0)
        {
            _presentTimeDesmotivation -= Time.deltaTime;

            _desmotivationTimeBar.fillAmount = _presentTimeDesmotivation / _desmotivationDuration;

            yield return null;
        }
        _presentTimeDesmotivation = 0;
        _desmotivationTimeBar.fillAmount = 0;
        _karenAtack = false;
    }
    public void StoreUpgradeOvenTime()
    {
        float _timeOfOvenAfterUpdate = _timeOfCooking - _timeOfCookingTimeDecreasing;
        _timeOfCooking = _timeOfOvenAfterUpdate;
        float _timeOfOvenAfterUpdate2 = _timeOfCooking - _timeOfCookingTimeDecreasing;
        _timeOfCookingBeingOnTextShop.text = $"{_timeOfCooking.ToString()} = {_timeOfOvenAfterUpdate2.ToString()}";
        if (_timeOfOvenAfterUpdate2 <= 1.0f)
        {
            _disableButton.Invoke();
        }
    }
}
