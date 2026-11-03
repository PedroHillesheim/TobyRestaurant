using System.Collections.Generic;
using Unity.VisualScripting;
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
    private float _clientWaitingTime;
    private Slot _slot;
    private int _damege;
    private TypeOfMealAvailable _typeOfMeal;
    private int _order;
    private ClientsType _clientsType;
    private bool _isSlot1Available;
    private bool _isSlot2Available;
    private bool _isSlot3Available;
    private bool _isSlot4Available;
    //private Sprite _clientSprite;
    public void NewClient(float clientWaitingTime, Slot slot, int damege,
    TypeOfMealAvailable typeOfMeal, int order, ClientsType clientsType, Reception check)
    {
        _order = order;
        _clientsType = clientsType;
        _typeOfMeal = typeOfMeal;
        _clientWaitingTime = clientWaitingTime;
        _slot = slot;
        _damege = damege;
        for (int i = 0; i < _client.Length; i++)
        {
            _client[i].GetValue(/*clientSprite,*/ clientWaitingTime, damege, slot, typeOfMeal, order, clientsType);
        }
    }
    public void GetoTable(int slotNumber)
    {
        if(slotNumber == 1)
        {
            if( _isSlot1Available == true)
            {
                _slot = Slot.Slot1;
                _isSlot1Available = false;
                _client[slotNumber - 1].GetValue(_clientWaitingTime, _damege, _slot, _typeOfMeal, _order, _clientsType);
            }
            else
            {

            }
        }
        else if (slotNumber == 2)
        {
            if (_isSlot2Available == true)
            {
                _slot = Slot.Slot2;
                _isSlot2Available = false;
                _client[slotNumber - 1].GetValue(_clientWaitingTime, _damege, _slot, _typeOfMeal, _order, _clientsType);
            }
            else
            {

            }
        }
        else if (slotNumber == 3)
        {
            if (_isSlot3Available == true)
            {
                _slot = Slot.Slot3;
                _isSlot3Available = false;
                _client[slotNumber - 1].GetValue(_clientWaitingTime, _damege, _slot, _typeOfMeal, _order, _clientsType);
            }
            else
            {

            }
        }
        else
        {
            if (_isSlot4Available == true)
            {
                _slot = Slot.Slot4;
                _isSlot4Available = false;
                _client[slotNumber - 1].GetValue(_clientWaitingTime, _damege, _slot, _typeOfMeal, _order, _clientsType);
            }
            else
            {

            }
        }
    }
    public void VerifyIfSlotIsAvailable(Slot slot, bool VF)
    {
        if(slot == Slot.Slot1)
        {
            _isSlot1Available = VF;
        }
        else if (slot == Slot.Slot2)
        {
            _isSlot2Available = VF;
        }
        else if (slot == Slot.Slot3)
        {
            _isSlot3Available = VF;
        }
        else
        {
            _isSlot4Available = VF;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //_clientSprite = null;
        _client = GameController.Instance.ClientsElements;
    }
}
