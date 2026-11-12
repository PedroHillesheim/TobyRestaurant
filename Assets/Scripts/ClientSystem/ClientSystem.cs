using System.Collections.Generic;
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
    private ClientsType _clientTypeReception1;
    private ClientsType _clientTypeReception2;
    private RestaurantLife _restaurantLife;
    private Reception _clientinReception;
    private ClientElement[] _client;
    [SerializeField] ClientType _clientType;
    [SerializeField] private MealType _mealType;
    private float _clientWaitingTime1;
    private float _clientWaitingTime2;
    private Slot _slot;
    private int _damege1;
    private int _damege2;
    private TypeOfMealAvailable _typeOfMeal1;
    private TypeOfMealAvailable _typeOfMeal2;
    private int _order1;
    private int _order2;
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
    private Button[] _slotComands;
    private Button[] _clientsWaiting;
    private Image[] _timeBarPacience;
    private float _presentTimeClient1;
    private float _presentTimeClient2;
    private float _waitingClientTimeOld1;
    private float _waitingClientTimeOld2;
    [SerializeField] private Sprite[] _clientImagefromData;
    private Color _alphaOne;
    private Color _alphaZero;
    private bool _client1Waiting;
    private bool _client2Waiting;
    private int _currentClient;
    void Awake()
    {
        //_clientSprite = null;
        _alphaZero.a = 0;
        _alphaZero.b = 255;
        _alphaZero.r = 255;
        _alphaZero.g = 255;
        _alphaOne.a = 1;
        _alphaOne.b = 255;
        _alphaOne.r = 255;
        _alphaOne.g = 255;
        _client = GameController.Instance.ClientsElements;
        _slotComands = GameController.Instance.SlotComands;
        _clientsWaiting = GameController.Instance.ClientsWaiting;
        _orderSystem = GameController.Instance.OrderSystem;
        _clientInReception1 = GameController.Instance.ClientInReception1;
        _clientInReception2 = GameController.Instance.ClientInReception2;
        _timeBarPacience = GameController.Instance.TimeBarPacience;
        _restaurantLife = GameController.Instance.RestaurantLife;
        _clientInReception1.color = _alphaZero; 
        _clientInReception2.color = _alphaZero;
        for (int i = 0; i < _slotComands.Length; i++)
        {
            _slotComands[i].interactable = false;
        }
        for (int i = 0; i < _clientsWaiting.Length; i++)
        {
            _clientsWaiting[i].interactable = false;
        }
    }
    public void DayEnded()
    {
        print("Vamos ver o dia sem referencia funcionar");
        _isReception1Available = true;
        _isReception2Available = true;
        _orderSystem.GetAvailableStateOfReception(Reception.CheckIn1, _isReception1Available);
        _orderSystem.GetAvailableStateOfReception(Reception.CheckIn2, _isReception1Available);
        _isSlot1Available = true;
        _isSlot2Available = true;
        _isSlot3Available = true;
        _isSlot4Available = true;
        _clientInReception1.color = _alphaZero;
        _clientInReception2.color = _alphaZero;
        _clientsWaiting[0].interactable = false;
        _clientsWaiting[1].interactable = false;
    }
    public void NewClient(float clientWaitingTime, int damege,
    TypeOfMealAvailable typeOfMeal, int order, ClientsType clientsType, Reception check)
    {
        _clientsType = clientsType;
        
        if(check == Reception.CheckIn1)
        {
            _typeOfMeal1 = typeOfMeal;
            _clientTypeReception1 = clientsType;
            _clientWaitingTime1 = clientWaitingTime;
            _damege1 = damege;
            _order1 = order;
            _isReception1Available = false;
            _clientInReception1.color = _alphaOne;
            _clientsWaiting[0].interactable = true;
            if(clientsType == ClientsType.NormalClients)
            {
                _clientInReception1.sprite = _clientImagefromData[0];
            }
            else if (clientsType == ClientsType.Critic)
            {
                _clientInReception1.sprite = _clientImagefromData[1];
            }
            else if (clientsType == ClientsType.Influencer)
            {
                _clientInReception1.sprite = _clientImagefromData[2];
            }
            else if (clientsType == ClientsType.Karen)
            {
                _clientInReception1.sprite = _clientImagefromData[3];
            }
            else if (clientsType == ClientsType.Monster)
            {
                _clientInReception1.sprite = _clientImagefromData[4];
            }
            _orderSystem.GetAvailableStateOfReception(check, _isReception1Available);
        }
        else if (check == Reception.CheckIn2)
        {
            _typeOfMeal2 = typeOfMeal;
            _clientWaitingTime2 = clientWaitingTime;
            _damege2 = damege;
            _order2 = order;
            _clientTypeReception2 = clientsType;
            _isReception2Available = false;
            _clientInReception2.color = _alphaOne;
            _clientsWaiting[1].interactable = true;
            if (clientsType == ClientsType.NormalClients)
            {
                _clientInReception2.sprite = _clientImagefromData[0];
            }
            else if (clientsType == ClientsType.Critic)
            {
                _clientInReception2.sprite = _clientImagefromData[1];
            }
            else if (clientsType == ClientsType.Influencer)
            {
                _clientInReception2.sprite = _clientImagefromData[2];
            }
            else if (clientsType == ClientsType.Karen)
            {
                _clientInReception2.sprite = _clientImagefromData[3];
            }
            else if (clientsType == ClientsType.Monster)
            {
                _clientInReception2.sprite = _clientImagefromData[4];
            }
            _orderSystem.GetAvailableStateOfReception(check, _isReception2Available);
        }
    }
    private void Update()
    {
        if (_isReception1Available == false)
        {
            if (!_client1Waiting)
            {
                _client1Waiting = true;
                _waitingClientTimeOld1 = _clientWaitingTime1;
                _presentTimeClient1 = 0;
                _timeBarPacience[0].fillAmount = 0;
            }
            _presentTimeClient1 += Time.deltaTime;
            _timeBarPacience[0].fillAmount = _presentTimeClient1 / _waitingClientTimeOld1;
            if (_clientTypeReception1 == ClientsType.Monster)
            {
                _restaurantLife.TakeDamege(_damege1);
                _timeBarPacience[0].fillAmount = 0f;
                _presentTimeClient1 = 0;
            }
            else if (_presentTimeClient1 >= _waitingClientTimeOld1)
            {
                _presentTimeClient1 = _waitingClientTimeOld1;
                _timeBarPacience[0].fillAmount = 1;
                _restaurantLife.TakeDamege(_damege1);
                _client1Waiting = false;
            }

        }
        else
        {
            _client1Waiting = false;
            _presentTimeClient1 = 0;
            _timeBarPacience[0].fillAmount = 0;
        }
        if (_isReception2Available == false)
        {
            if (!_client2Waiting)
            {
                _client2Waiting = true;
                _waitingClientTimeOld2 = _clientWaitingTime2;
                _presentTimeClient2 = 0;
                _timeBarPacience[1].fillAmount = 0;
            }
            _presentTimeClient2 += Time.deltaTime;
            _timeBarPacience[1].fillAmount = _presentTimeClient2 / _waitingClientTimeOld2;
            if (_clientTypeReception1 == ClientsType.Monster)
            {
                _restaurantLife.TakeDamege(_damege2);
                _timeBarPacience[1].fillAmount = 0f;
                _presentTimeClient2 = 0;
            }
            else if (_presentTimeClient2 >= _waitingClientTimeOld2)
            {
                _presentTimeClient2 = _waitingClientTimeOld2;
                _timeBarPacience[1].fillAmount = 1;
                _restaurantLife.TakeDamege(_damege2);
                _client2Waiting = false;
            }
        }
        else
        {
            _client2Waiting = false;
            _presentTimeClient2 = 0;
            _timeBarPacience[1].fillAmount = 0;
        }
    }
    public void GetClientToChose(int receptionNumber)
    {
        _clientinReception = (Reception)receptionNumber;
        if(_clientinReception == Reception.CheckIn1 && _clientTypeReception1 == ClientsType.Monster)
        {
            _restaurantLife.TakeDamege(_damege1);
            _isReception1Available = true;
            _clientInReception1.color = _alphaZero;
            _clientsWaiting[0].interactable = false;
            _orderSystem.GetAvailableStateOfReception(_clientinReception, _isReception1Available);
            _timeBarPacience[0].fillAmount = 0;
            _presentTimeClient1 = 0;
        }
        else if (_clientinReception == Reception.CheckIn2 && _clientTypeReception2 == ClientsType.Monster)
        {
            _restaurantLife.TakeDamege(_damege2);
            _isReception2Available = true;
            _clientInReception2.sprite = null;
            _clientInReception2.color = _alphaZero;
            _clientsWaiting[1].interactable = false;
            _orderSystem.GetAvailableStateOfReception(_clientinReception, _isReception2Available);
            _timeBarPacience[1].fillAmount = 0;
            _presentTimeClient2 = 0;
        }
        else
        {
            for (int i = 0; i < _slotComands.Length; i++)
            {
                _slotComands[i].interactable = true;
            }
        }
    }
    public void GetoTable(int slotNumber)
    {
        if (slotNumber == 1)
        {
            if( _isSlot1Available == true)
            {
                _slot = Slot.Slot1;
                _isSlot1Available = false;
                if(_clientinReception == Reception.CheckIn1)
                {
                    _client[slotNumber - 1].GetValue(_clientWaitingTime1, _damege1,(Slot)slotNumber, _typeOfMeal1,
                        _order1, _clientTypeReception1);
                }
                else
                {
                    _client[slotNumber - 1].GetValue(_clientWaitingTime2, _damege2, (Slot)slotNumber, _typeOfMeal2,
                        _order2, _clientTypeReception2);
                }
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
                if (_clientinReception == Reception.CheckIn1)
                {
                    _client[slotNumber - 1].GetValue(_clientWaitingTime1, _damege1, (Slot)slotNumber, _typeOfMeal1,
                        _order1, _clientTypeReception1);
                }
                else
                {
                    _client[slotNumber - 1].GetValue(_clientWaitingTime2, _damege2, (Slot)slotNumber, _typeOfMeal2,
                        _order2, _clientTypeReception2);
                }
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
                if (_clientinReception == Reception.CheckIn1)
                {
                    _client[slotNumber - 1].GetValue(_clientWaitingTime1, _damege1, (Slot)slotNumber, _typeOfMeal1,
                        _order1, _clientTypeReception1);
                }
                else
                {
                    _client[slotNumber - 1].GetValue(_clientWaitingTime2, _damege2, (Slot)slotNumber, _typeOfMeal2,
                        _order2, _clientTypeReception2);
                }
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
                if (_clientinReception == Reception.CheckIn1)
                {
                    _client[slotNumber - 1].GetValue(_clientWaitingTime1, _damege1, (Slot)slotNumber, _typeOfMeal1,
                        _order1, _clientTypeReception1);
                }
                else
                {
                    _client[slotNumber - 1].GetValue(_clientWaitingTime2, _damege2, (Slot)slotNumber, _typeOfMeal2,
                        _order2, _clientTypeReception2);
                }
            }
            else
            {
                throw new System.NotImplementedException("_slot 4 não disponivel");
            }
        }
        if (_clientinReception == Reception.CheckIn1)
        {
            _isReception1Available = true;
            _clientInReception1.sprite = null;
            _clientInReception1.color = _alphaZero;
            _clientsWaiting[0].interactable = false;
            _orderSystem.GetAvailableStateOfReception(_clientinReception, _isReception1Available);
            _timeBarPacience[0].fillAmount = 0;
            _presentTimeClient1 = 0;
            for (int i = 0; i < _slotComands.Length; i++)
            {
                _slotComands[i].interactable = false;
            }
        }
        else if (_clientinReception == Reception.CheckIn2)
        {
            _isReception2Available = true;
            _clientInReception2.sprite = null;
            _clientInReception2.color = _alphaZero;
            _clientsWaiting[1].interactable = false;
            _orderSystem.GetAvailableStateOfReception(_clientinReception, _isReception2Available);
            _timeBarPacience[1].fillAmount = 0;
            _presentTimeClient2 = 0;
            for (int i = 0; i < _slotComands.Length; i++)
            {
                _slotComands[i].interactable = false;
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
