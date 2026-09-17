using System.Collections.Generic;
using TMPro;
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
    public void NewClient(/*Sprite clientSprite,*/ float clientWaitingTime, Slot slot, int damege, TypeOfMealAvailable typeOfMeal)
    {
        _isSlotAvalable = false;
        _orderSystem.GetConfirmationOfAvailableSlot(slot, _isSlotAvalable);
        for (int i = 0; i < _client.Length; i++)
        {
            _client[i].GetValue(/*clientSprite,*/ clientWaitingTime, damege, slot, typeOfMeal);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _clientSprite = null;
        _client = GameController.Instance.ClientsElements;
    }
}
