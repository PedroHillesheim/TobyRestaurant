using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public enum Slot
{
    Slot1 = 1,
    Slot2 = 2,
    Slot3 = 3,
    Slot4 = 4
}
public class ClientType
{
    [SerializeField] private List<ClientElement> _clientElement;


    public List<ClientElement> ClientsElemnts { get => _clientElement; }
}
public class ClientSystem : MonoBehaviour
{
    [SerializeField] OrderSystem _orderSystem;
    private ClientElement[] _client;
    [SerializeField] ClientType _clientType;
    [SerializeField] private MealType _mealType;
    private Slot _slot;
    private bool _isSlotAvalable = true;
    private Sprite _clientSprite;
    private int _mealsReady;
    private int order1;
    public void NewClient(Sprite clientSprite, float clientWaitingTime, Slot slot, int damege, TypeOfMealAvailable typeOfMeal)
    {
        _isSlotAvalable = false;
        _orderSystem.GetConfirmationOfAvailableSlot(_slot, _isSlotAvalable);
        for (int i = 0; i < _client.Length; i++)
        {
            _client[i].GetValue(clientSprite, clientWaitingTime, damege, slot, typeOfMeal);
        }
    }
    public void Order(Slot slot)
    {
        if (_slot == slot)
        {
            if (order1 == 0)
            {
                OrderDone();
            }
            else
            {
                throw new System.NotImplementedException("Order not done not programmed");
            }
        }
    }

    private void OrderDone()
    {
        _isSlotAvalable = true;
        _orderSystem.GetConfirmationOfAvailableSlot(_slot, _isSlotAvalable);
        Destroy(_clientSprite);
        order1 = 0;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _clientSprite = null;
    }
    public void IsOrderCorrect()
    {
        if(_mealsReady > 1 && order1 == 1)
        {
            _orderSystem.ClientAtended();
            OrderDone();
        }
    }
    public void GetMealsQuantity(int mealsReady)
    {
        _mealsReady = mealsReady;
    }
}
