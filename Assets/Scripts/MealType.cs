using System.Collections;
using UnityEngine;

public enum TypeOfMealAvailable
{
    Appetizer,
    Dessert
}
public class MealType : MonoBehaviour
{
    [SerializeField] private TypeOfMealAvailable typeOfMealAvailable;
    [SerializeField] private float _timeOfCooking;
    private int _readyMadeMeals = 0;
    [SerializeField] private int _maxOfReadyMadeMeals;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartCooking(TypeOfMealAvailable type)
    {
        if(type == typeOfMealAvailable)
        {
            StartCoroutine(CookingMeal());
        }
    }
    private IEnumerator CookingMeal()
    {
        if (_readyMadeMeals == _maxOfReadyMadeMeals)
            yield return null;
        yield return new WaitForSeconds(_timeOfCooking);
        _readyMadeMeals++;
    }
}
