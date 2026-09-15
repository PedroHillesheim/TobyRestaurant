using System.Collections;
using UnityEngine;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartCooking(TypeOfMealAvailable type)
    {
        StartCoroutine(CookingMeal(type));
    }
    private IEnumerator CookingMeal(TypeOfMealAvailable type)
    {
        if (_readyMadeMeals == _maxOfReadyMadeMeals)
            yield return null;
        yield return new WaitForSeconds(_timeOfCooking);
        _readyMadeMeals++;
        _clientSystem.GetMealsQuantity(_readyMadeMeals);
        for (int i = 0; i < _clients.Length; i++)
        {
            _clients[i].GetMealsQuantityAndType(_readyMadeMeals, type);
        }
    }
    public void GetMealsLost(int meals, TypeOfMealAvailable mealTypo)
    {
        if(typeOfMealAvailable != mealTypo)
            return;
        _readyMadeMeals -= meals;
        for (int i = 0; i < _clients.Length; i++)
        {
            _clients[i].GetMealsQuantityAndType(_readyMadeMeals, typeOfMealAvailable);
        }
    }
    private void Start()
    {
        _clients = GameController.Instance.ClientsElements;
    }
}
