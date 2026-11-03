using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum Slot
{
    Slot1 = 1,
    Slot2 = 2,
    Slot3 = 3,
    Slot4 = 4
}
public enum Reception
{
    CheckIn1 = 1,
    CheckIn2 = 2
}
public class ClientType
{
    [SerializeField] private List<ClientElement> _clientElement;


    public List<ClientElement> ClientsElemnts { get => _clientElement; }
}
public class ClientSystem : MonoBehaviour
{
    [SerializeField] OrderSystem _orderSystem;
    private Reception _clientinReception;
    private ClientElement[] _client;
    [SerializeField] ClientType _clientType;
    [SerializeField] private MealType _mealType;
    private float _clientWaitingTime;
    private Slot _slot;
    private int _damege;
    private TypeOfMealAvailable _typeOfMeal;
    private int _order;
    private ClientsType _clientsType;
    private bool _isSlot1Available = true;
    private bool _isSlot2Available = true;
    private bool _isSlot3Available = true;
    private bool _isSlot4Available = true;
    private bool _isReception1Available = true;
    private bool _isReception2Available = true;
    private AudioSource _unavailableSound;
    private Image _clientInReception1;
    private Image _clientInReception2;
    private Image[] _clientsImages;
    void Awake()
    {
        //_clientSprite = null;
        _client = GameController.Instance.ClientsElements;
        _clientInReception1 = GameController.Instance.ClientInReception1;
        _clientInReception2 = GameController.Instance.ClientInReception2;
    }
    public void NewClient(float clientWaitingTime, int damege,
    TypeOfMealAvailable typeOfMeal, int order, ClientsType clientsType, Reception check)
    {
        _order = order;
        _clientsType = clientsType;
        _typeOfMeal = typeOfMeal;
        _clientWaitingTime = clientWaitingTime;
        _damege = damege;
        if(check == Reception.CheckIn1)
        {
            _isReception1Available = false;
            _clientInReception1.color = Color.gold;
        }
        else if (check == Reception.CheckIn2)
        {
            _isReception2Available = false;
            _clientInReception2.color = Color.gold;
        }
    }
    public void GetClientToChose(int receptionNumber)
    {
        _clientinReception = (Reception)receptionNumber;
    }
    public void GetoTable(int slotNumber)
    {
        if(slotNumber == 1)
        {
            if( _isSlot1Available == true)
            {
                _slot = Slot.Slot1;
                _isSlot1Available = false;
                _client[slotNumber - 1].GetValue(_clientWaitingTime, _damege, (Slot)slotNumber, _typeOfMeal, _order, _clientsType);
            }
            else
            {
                throw new System.NotImplementedException("_slot 1 não disponivel");
            }
        }
        else if (slotNumber == 2)
        {
            if (_isSlot2Available == true)
            {
                _slot = Slot.Slot2;
                _isSlot2Available = false;
                _client[slotNumber - 1].GetValue(_clientWaitingTime, _damege, (Slot)slotNumber, _typeOfMeal, _order, _clientsType);
            }
            else
            {
                throw new System.NotImplementedException("_slot 2 não disponivel");
            }
        }
        else if (slotNumber == 3)
        {
            if (_isSlot3Available == true)
            {
                _slot = Slot.Slot3;
                _isSlot3Available = false;
                _client[slotNumber - 1].GetValue(_clientWaitingTime, _damege, (Slot)slotNumber, _typeOfMeal, _order, _clientsType);
            }
            else
            {
                throw new System.NotImplementedException("_slot 3 não disponivel");
            }
        }
        else
        {
            if (_isSlot4Available == true)
            {
                _slot = Slot.Slot4;
                _isSlot4Available = false;
                _client[slotNumber - 1].GetValue(_clientWaitingTime, _damege, (Slot)slotNumber, _typeOfMeal, _order, _clientsType);
            }
            else
            {
                throw new System.NotImplementedException("_slot 4 não disponivel");
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
}
