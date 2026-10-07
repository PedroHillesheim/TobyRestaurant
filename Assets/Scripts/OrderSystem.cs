using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ClientsType
{
    NormalClients,
    Critic,
    Influencer
}
public class OrderSystem : MonoBehaviour
{
    private List<int> _avalableSlot = new List<int>();
    private ClientsType _clientType;
    [Header("Client")]
    private Sprite _clientSprite;
    private float _clientWaitingTime;
    private int _damege;
    private int _quantityOfOrder;
    [Header("Normal Client")]
    [SerializeField] private Sprite _normalClientSprite;
    [SerializeField] private float _minimalNormalClientWaitingTime;
    [SerializeField] private float _maxNormalClientWaitingTime;
    [SerializeField] private int _normalClientDamege;
    [SerializeField] private int _normalClientQuantityOfOrder;
    [Header("Food critic")]
    [SerializeField] private Sprite _criticClientSprite;
    [SerializeField] private float _criticWaitingTime;
    [SerializeField] private int _criticDamege;
    [SerializeField] private int _criticQuantityOfOrder;
    [Header("Influencer")]
    [SerializeField] private Sprite _influencerSprite;
    [SerializeField] private float _influencerWaitingTime;
    [SerializeField] private int _influencerDamege;
    [SerializeField] private int _influencerQuantityOfOrder;
    [Header("Values")]
    [SerializeField] private ClientSystem clientSystem;
    private EventSystem eventSystem;
    [SerializeField] private float _arrivalInterval = 5f;
    [SerializeField] private int _clientAttendedUntilSpecial = 8;
    [SerializeField] private float _frenezyDuration;
    private int _clientsAtended;
    [SerializeField] private int _slotsAvailable;
    private bool _isSlot1Avalable = true;
    private bool _isSlot2Avalable = true;
    private bool _isSlot3Avalable = true;
    private bool _isSlot4Avalable = true;
    private bool _firstTime = true;
    private TMP_Text _alertText;
    private TMP_Text _orderDoneText;
    private float _presentTimeFrenezy;
    private Image _frenezyTimeBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        clientSystem = GameController.Instance.ClientSystem;
        StartCoroutine(ClientsComing());
        _alertText = GameController.Instance.AlertText;
        _orderDoneText = GameController.Instance.OrderDonesText;
        eventSystem = GameController.Instance.EventSystem;
        _frenezyTimeBar = GameController.Instance.FrenezyTimeBar;
        for (int i = 1; i <= 4; i++)
        {
            _avalableSlot.Add(i);
        }
    }
    public void GetConfirmationOfAvailableSlot(Slot slot, bool availableState)
    {
        if (slot == Slot.Slot1)
        {
            _isSlot1Avalable = availableState;
        }
        if (slot == Slot.Slot2)
        {
            _isSlot2Avalable = availableState;
        }
        if (slot == Slot.Slot3)
        {
            _isSlot3Avalable = availableState;
        }
        if (slot == Slot.Slot4)
        {
            _isSlot4Avalable = availableState;
        }
    }
    public void ClientAtended(Slot slot)
    {
        _clientsAtended++;
        if (slot == Slot.Slot1)
        {
            _avalableSlot.Add(1);
        }
        else if (slot == Slot.Slot2)
        {
            _avalableSlot.Add(2);
        }
        else if (slot == Slot.Slot3)
        {
            _avalableSlot.Add(3);
        }
        else if (slot == Slot.Slot4)
        {
            _avalableSlot.Add(4);
        }
    }
    private IEnumerator ClientsComing()
    {
        if (_isSlot1Avalable == false && _isSlot2Avalable == false &&
        _isSlot3Avalable ==false && _isSlot4Avalable == false)
        {
            StartCoroutine(ClientsComing());
            yield break;
        }
        if(_clientsAtended >= _clientAttendedUntilSpecial -1)
        {
            int randomCase = Random.Range(1, 3);
            switch (randomCase)
            {
                case 1:
                    print("Critic Coming");
                    _clientSprite = _criticClientSprite;
                    _clientWaitingTime = _criticWaitingTime;
                    _damege = _criticDamege;
                    _quantityOfOrder = _criticQuantityOfOrder;
                    _clientType = ClientsType.Critic;
                    break;
                case 2:
                    print("Influencer coming");
                    _clientSprite = _influencerSprite;
                    _clientWaitingTime = _influencerWaitingTime;
                    _damege = _influencerDamege;
                    _influencerQuantityOfOrder = Random.Range(1, 4);
                    if (_influencerQuantityOfOrder <= 1)
                    {
                        _influencerQuantityOfOrder = Random.Range(1, 4);
                    }
                    _quantityOfOrder = _influencerQuantityOfOrder;
                    _clientType = ClientsType.Influencer;
                    break; 
            }
            if (_firstTime == true)
            {
                eventSystem.StartChanceOfEvent(_arrivalInterval);
                _firstTime = false;
            }
        }
        else
        {
            _clientSprite = _normalClientSprite;
            float randomWaitingTime = Random.Range(_minimalNormalClientWaitingTime, _maxNormalClientWaitingTime);
            _clientWaitingTime = _minimalNormalClientWaitingTime;
            _damege = _normalClientDamege;
            _quantityOfOrder = _normalClientQuantityOfOrder;
            _clientType = ClientsType.NormalClients;
        }
        yield return new WaitForSeconds(_arrivalInterval);
        int randomSlot = Random.Range(1, _avalableSlot.Count);
        int randomMeal = Random.Range(1, 3);
        switch (randomSlot)
        {
            case 1:
                if (_isSlot1Avalable != true)
                {
                    StartCoroutine(ClientsComing());
                    yield break;
                }
                _avalableSlot.Remove(1);
                if (_clientType == ClientsType.Critic)
                {
                    _alertText.text = "The critic arrived";
                    StartCoroutine(Disapear());
                    int _randomClientAtended = Random.Range(6, 15);
                    _clientAttendedUntilSpecial = _randomClientAtended;
                    _clientsAtended = 0;
                }
                else if (_clientType == ClientsType.Influencer)
                {
                    _alertText.text = "An influencer arrived";
                    StartCoroutine(Disapear());
                    int _randomClientAtended = Random.Range(6, 15);
                    _clientAttendedUntilSpecial = _randomClientAtended;
                    _clientsAtended = 0;
                }
                clientSystem.NewClient(/*_clientSprite,*/ _clientWaitingTime, Slot.Slot1, _damege,
                (TypeOfMealAvailable)randomMeal, _quantityOfOrder, _clientType);
                break;
            case 2:
                if (_isSlot2Avalable != true)
                {
                    StartCoroutine(ClientsComing());
                    yield break;
                }
                _avalableSlot.Remove(2);
                if (_clientType == ClientsType.Critic)
                {
                    _alertText.text = "The critic arrived";
                    StartCoroutine(Disapear());
                    int _randomClientAtended = Random.Range(6, 15);
                    _clientAttendedUntilSpecial = _randomClientAtended;
                    _clientsAtended = 0;
                }
                else if (_clientType == ClientsType.Influencer)
                {
                    _alertText.text = "An influencer arrived";
                    StartCoroutine(Disapear());
                    int _randomClientAtended = Random.Range(6, 15);
                    _clientAttendedUntilSpecial = _randomClientAtended;
                    _clientsAtended = 0;
                }
                clientSystem.NewClient(/*_clientSprite,*/ _clientWaitingTime, Slot.Slot2, _damege,
                (TypeOfMealAvailable)randomMeal, _quantityOfOrder, _clientType);
                break;
            case 3:
                if (_isSlot3Avalable != true)
                {
                    StartCoroutine(ClientsComing());
                    yield break;
                }
                _avalableSlot.Remove(3);
                if (_clientType == ClientsType.Critic)
                {
                    _alertText.text = "The critic arrived";
                    StartCoroutine(Disapear());
                    int _randomClientAtended = Random.Range(6, 15);
                    _clientAttendedUntilSpecial = _randomClientAtended;
                    _clientsAtended = 0;
                }
                else if (_clientType == ClientsType.Influencer)
                {
                    _alertText.text = "An influencer arrived";
                    StartCoroutine(Disapear());
                    int _randomClientAtended = Random.Range(6, 15);
                    _clientAttendedUntilSpecial = _randomClientAtended;
                    _clientsAtended = 0;
                }
                clientSystem.NewClient(/*_clientSprite,*/ _clientWaitingTime, Slot.Slot3, _damege,
                (TypeOfMealAvailable)randomMeal, _quantityOfOrder, _clientType);
                break;
            case 4:
                if (_isSlot4Avalable != true)
                {
                    StartCoroutine(ClientsComing());
                    yield break;
                }
                _avalableSlot.Remove(4);
                if (_clientType == ClientsType.Critic)
                {
                    _alertText.text = "The critic arrived";
                    StartCoroutine(Disapear());
                    int _randomClientAtended = Random.Range(6, 15);
                    _clientAttendedUntilSpecial = _randomClientAtended;
                    _clientsAtended = 0;
                }
                else if (_clientType == ClientsType.Influencer)
                {
                    _alertText.text = "An influencer arrived";
                    StartCoroutine(Disapear());
                    int _randomClientAtended = Random.Range(6, 15);
                    _clientAttendedUntilSpecial = _randomClientAtended;
                    _clientsAtended = 0;
                }
                clientSystem.NewClient(/*_clientSprite,*/ _clientWaitingTime, Slot.Slot4, _damege,
                (TypeOfMealAvailable)randomMeal, _quantityOfOrder, _clientType);
                break;
        }
        StartCoroutine(ClientsComing());
    }
    private IEnumerator Disapear()
    {
        yield return new WaitForSeconds(2);
        _alertText.text = string.Empty;
    }
    public void GetoFrenezy(float arrivalTimeEvent)
    {
        _arrivalInterval = arrivalTimeEvent;
    }
    private IEnumerator FrenezyEvent()
    {
        _arrivalInterval /= 2;
        _frenezyTimeBar.fillAmount = 1;
        _presentTimeFrenezy = _frenezyDuration;
        while (_presentTimeFrenezy > 0)
        {
            _presentTimeFrenezy -= Time.deltaTime;

            _frenezyTimeBar.fillAmount = _presentTimeFrenezy / _frenezyDuration;

            yield return null;
        }
        _presentTimeFrenezy = 0;
        _frenezyTimeBar.fillAmount = 0;
        _arrivalInterval *= 2;
    }
}
