using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ClientsType
{
    None,
    NormalClients,
    Critic,
    Influencer,
    Karen,
    Monster
}
public enum Day
{
    Day1,
    Day2,
    Day3,
    Day4,
    Day5
}
public class OrderSystem : MonoBehaviour
{
    private List<int> _avalableSlot = new List<int>();
    private Day _day;
    private ClientsType _clientType;
    private RatsSpawner _spawner;
    [Header("Client")]
    private ClientElement[] _clientElement;
    private Sprite _clientSprite;
    private float _clientWaitingTime;
    private int _clientDamege;
    private int _clientQuantityOfOrder;
    private int _clientQuantityOfOrderTotal;
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
    [Header("Karen")]
    [SerializeField] private Sprite _karenSprite;
    [SerializeField] private float _karenWaitingTime;
    [SerializeField] private int _karenDamege;
    [SerializeField] private int _karenQuantityOfOrder;
    [Header("Monster")]
    [SerializeField] private Sprite _monsterSprite;
    [SerializeField] private float _monsterWaitingTime;
    [SerializeField] private int _monsterDamege;
    [SerializeField] private int _monsterQuantityOfOrder;
    [Header("Values")]
    [SerializeField] private ClientSystem clientSystem;
    private EventSystem eventSystem;
    [SerializeField] private float _arrivalInterval = 5f;
    [SerializeField] private int _clientAttendedUntilSpecial = 8;
    [SerializeField] private float _frenezyDuration;
    private int _clientsAtended;
    [SerializeField] private int _slotsAvailable;
    private bool _firstTime = true;
    private bool _onFrenezy = false;
    //private TMP_Text _alertText;
    private int _endOfDay;
    private int[] _endOfDaysValue;
    private bool _dayEnded;
    private bool _day1Critic;
    private bool _day3Karen = true;
    private int _clientChoice;
    private GameObject _dialogueObject;
    private TMP_Text _dialogueText;
    private string[] _dialogueString;
    private bool _isreception1Available = true;
    private bool _isreception2Available = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        clientSystem = GameController.Instance.ClientSystem;
        //_alertText = GameController.Instance.AlertText;
        eventSystem = GameController.Instance.EventSystem;
        _spawner = GameController.Instance.RatsSpawner;
        _endOfDay = GameController.Instance.EndOfDay;
        _clientElement = GameController.Instance.ClientsElements;
        _dialogueObject = GameController.Instance.DialogueVisual;
        _dialogueText = GameController.Instance.DialogueText;
        _dialogueString = GameController.Instance.DialogueString;
        for (int i = 1; i <= 4; i++)
        {
            _avalableSlot.Add(i);
        }
        StartCoroutine(ClientsComing());
    }
    public void ClientAtended(Slot slot)
    {
        _clientsAtended++;
        StartCoroutine(ReleaseSlotAfterDelay(slot));
    }
    private IEnumerator ReleaseSlotAfterDelay(Slot slot)
    {
        yield return new WaitForSeconds(2f);

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
    public void AddSlotAsAvailable(Slot slot)
    {
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
    public void GetClientAtendedQuantity(int value)
    {
        _clientQuantityOfOrderTotal = value;
    }
    public void BetweenDays()
    {
        Time.timeScale = 0;
    }
    public void NextDay()
    {
        Time.timeScale = 1;
        if (_day == Day.Day1)
        {
            _day = Day.Day2;
        }
        else if (_day == Day.Day2)
        {
            _day = Day.Day3;
        }
        else if (_day == Day.Day3)
        {
            _day = Day.Day4;
        }
        else
        {
            _day = Day.Day5;
        }
        _firstTime = true;
        _clientsAtended = 0;
        _clientQuantityOfOrderTotal = 0;
        for (int i = 1; i <= 4; i++)
        {
            _avalableSlot.Remove(i);
        }
        for (int i = 0; i < _clientElement.Length; i++)
        {
            _clientElement[i].ResetScene();
        }
        StopAllCoroutines();
        StartCoroutine(ClientsComing());
    }
    private IEnumerator ClientsComing()
    {
        if (_clientsAtended >= _clientAttendedUntilSpecial - 1)
        {
            if (_day == Day.Day1)
            {
                _clientSprite = _criticClientSprite;
                _clientWaitingTime = _criticWaitingTime;
                _clientDamege = 2;
                _clientQuantityOfOrder = 1;
                _clientType = ClientsType.Critic;
                _day1Critic = true;
            }
            else if (_day == Day.Day2)
            {
                int randomCase = Random.Range(1, 3);
                switch (randomCase)
                {
                    case 1:
                        _clientSprite = _criticClientSprite;
                        _clientWaitingTime = _criticWaitingTime;
                        _clientDamege = 2;
                        _clientQuantityOfOrder = _criticQuantityOfOrder;
                        _clientType = ClientsType.Critic;
                        _clientsAtended = 0;
                        break;
                    case 2:
                        _clientSprite = _influencerSprite;
                        _clientWaitingTime = _influencerWaitingTime;
                        _clientDamege = _influencerDamege;
                        _influencerQuantityOfOrder = Random.Range(1, 4);
                        if (_influencerQuantityOfOrder <= 1)
                        {
                            _influencerQuantityOfOrder = Random.Range(1, 4);
                        }
                        _clientQuantityOfOrder = _influencerQuantityOfOrder;
                        _clientType = ClientsType.Influencer;
                        _clientsAtended = 0;
                        break;
                }
            }
            else if (_day == Day.Day3)
            {
                int randomCase = Random.Range(1, 3);
                switch (randomCase)
                {
                    case 1:
                        _clientSprite = _criticClientSprite;
                        _clientWaitingTime = _criticWaitingTime;
                        _clientDamege = 2;
                        _clientQuantityOfOrder = _criticQuantityOfOrder;
                        _clientType = ClientsType.Critic;
                        _clientsAtended = 0;
                        if (_onFrenezy == false)
                        {
                            int _randomClientAtended = Random.Range(6, 15);
                            _clientAttendedUntilSpecial = _randomClientAtended;
                        }
                        break;
                    case 2:
                        _clientSprite = _influencerSprite;
                        _clientWaitingTime = _influencerWaitingTime;
                        _clientDamege = _influencerDamege;
                        _influencerQuantityOfOrder = Random.Range(1, 4);
                        if (_influencerQuantityOfOrder <= 1)
                        {
                            _influencerQuantityOfOrder = Random.Range(1, 4);
                        }
                        _clientQuantityOfOrder = _influencerQuantityOfOrder;
                        _clientType = ClientsType.Influencer;
                        _clientsAtended = 0;
                        if (_onFrenezy == false)
                        {
                            int _randomClientAtended = Random.Range(6, 15);
                            _clientAttendedUntilSpecial = _randomClientAtended;
                        }
                        break;
                }
            }
            else
            {
                int randomCase = Random.Range(1, 4);
                switch (randomCase)
                {
                    case 1:
                        _clientSprite = _criticClientSprite;
                        _clientWaitingTime = _criticWaitingTime;
                        _clientDamege = _criticDamege;
                        _clientQuantityOfOrder = _criticQuantityOfOrder;
                        _clientType = ClientsType.Critic;
                        _clientsAtended = 0;
                        if (_onFrenezy == false)
                        {
                            int _randomClientAtended = Random.Range(6, 15);
                            _clientAttendedUntilSpecial = _randomClientAtended;
                        }
                        break;
                    case 2:
                        _clientSprite = _influencerSprite;
                        _clientWaitingTime = _influencerWaitingTime;
                        _clientDamege = _influencerDamege;
                        _influencerQuantityOfOrder = Random.Range(1, 4);
                        if (_influencerQuantityOfOrder <= 1)
                        {
                            _influencerQuantityOfOrder = Random.Range(1, 4);
                        }
                        _clientQuantityOfOrder = _influencerQuantityOfOrder;
                        _clientType = ClientsType.Influencer;
                        _clientsAtended = 0;
                        if (_onFrenezy == false)
                        {
                            int _randomClientAtended = Random.Range(6, 15);
                            _clientAttendedUntilSpecial = _randomClientAtended;
                        }
                        break;
                    case 3:
                        _clientSprite = _monsterSprite;
                        _clientWaitingTime = _monsterWaitingTime;
                        _clientDamege = _monsterDamege;
                        _clientQuantityOfOrder = _monsterQuantityOfOrder;
                        _clientType = ClientsType.Monster;
                        _clientsAtended = 0;
                        if (_onFrenezy == false)
                        {
                            int _randomClientAtended = Random.Range(6, 15);
                            _clientAttendedUntilSpecial = _randomClientAtended;
                        }
                        break;
                }
                if (_firstTime == true)
                {
                    if (_day == Day.Day1 || _day == Day.Day2) { }
                    else
                    {
                        eventSystem.StartChanceOfEvent();
                        _spawner.StartRatspawner();
                    }
                    _firstTime = false;
                }
            }
        }
        else
        {
            if (_day == Day.Day1 || _day == Day.Day2)
            {
                _clientSprite = _normalClientSprite;
                float randomWaitingTime = Random.Range(_minimalNormalClientWaitingTime, _maxNormalClientWaitingTime);
                _clientWaitingTime = randomWaitingTime;
                _clientDamege = _normalClientDamege;
                _clientQuantityOfOrder = _normalClientQuantityOfOrder;
                _clientType = ClientsType.NormalClients;
            }
            else
            {
                if(_day == Day.Day3 && _day3Karen == true)
                {
                    _clientChoice = 1;
                }
                else
                {
                    int ramdomCase = Random.Range(0, 5);
                    _clientChoice = ramdomCase;
                }
                switch (_clientChoice)
                {
                    case 0:
                        _clientSprite = _normalClientSprite;
                        float randomWaitingTime = Random.Range(_minimalNormalClientWaitingTime, _maxNormalClientWaitingTime);
                        _clientWaitingTime = randomWaitingTime;
                        _clientDamege = _normalClientDamege;
                        _clientQuantityOfOrder = _normalClientQuantityOfOrder;
                        _clientType = ClientsType.NormalClients;
                        break;
                    case 1:
                        _clientSprite = _karenSprite;
                        _clientWaitingTime = _karenWaitingTime;
                        _clientDamege = _karenDamege;
                        _clientQuantityOfOrder = _karenQuantityOfOrder;
                        _clientType = ClientsType.Karen;
                        break;
                    case 2:
                        _clientSprite = _normalClientSprite;
                        float randomWaitingTime2 = Random.Range(_minimalNormalClientWaitingTime, _maxNormalClientWaitingTime);
                        _clientWaitingTime = randomWaitingTime2;
                        _clientDamege = _normalClientDamege;
                        _clientQuantityOfOrder = _normalClientQuantityOfOrder;
                        _clientType = ClientsType.NormalClients;
                        break;
                    case 3:
                        _clientSprite = _normalClientSprite;
                        float randomWaitingTime3 = Random.Range(_minimalNormalClientWaitingTime, _maxNormalClientWaitingTime);
                        _clientWaitingTime = randomWaitingTime3;
                        _clientDamege = _normalClientDamege;
                        _clientQuantityOfOrder = _normalClientQuantityOfOrder;
                        _clientType = ClientsType.NormalClients;
                        break;
                    case 4:
                        _clientSprite = _normalClientSprite;
                        float randomWaitingTime4 = Random.Range(_minimalNormalClientWaitingTime, _maxNormalClientWaitingTime);
                        _clientWaitingTime = randomWaitingTime4;
                        _clientDamege = _normalClientDamege;
                        _clientQuantityOfOrder = _normalClientQuantityOfOrder;
                        _clientType = ClientsType.NormalClients;
                        break;
                }
            }
        }
        int randomReception = Random.Range(0, 2);
        Reception reception = (Reception)randomReception;
        if (_isreception1Available == false && _isreception2Available == false)
        {
            yield return new WaitUntil(() => _isreception1Available || _isreception2Available);
            if (_isreception1Available)
            {
                reception = Reception.CheckIn1;
            }
            else
            {
                reception = Reception.CheckIn2;
            }
        }
        else if (reception == Reception.CheckIn1 && _isreception1Available == true)
        {
            _isreception1Available = false;
        }
        else if (reception == Reception.CheckIn2 && _isreception2Available == true)
        {
            _isreception2Available = false;
        }
        else if (reception == Reception.CheckIn1 && _isreception1Available == false)
        {
            reception = Reception.CheckIn2;
        }
        else
        {
            reception = Reception.CheckIn1;
        }
        yield return new WaitUntil(() => _avalableSlot.Count > 0);
        yield return new WaitUntil(() => _dayEnded = true);
        yield return new WaitForSeconds(_arrivalInterval);
        //Slot slot = (Slot)randomSlot; 
        int randomMeal = Random.Range(1, 3);
        if(_day1Critic == true)
        {
            _dayEnded = true;
        }
        if(_day3Karen == true)
        {
            _day3Karen = false;
            //Time.timeScale = 0f;
            //_dialogueObject.SetActive(true);
            //_dialogueText.text = _dialogueString[0];
        }      
        if (_clientType == ClientsType.Critic)
        {
            //_alertText.text = "The critic arrived";
            //StartCoroutine(Disapear());
        }
        else if (_clientType == ClientsType.Influencer)
        {
            //_alertText.text = "An influencer arrived";
            //StartCoroutine(Disapear());
        }
        clientSystem.NewClient(/*_clientSprite,*/ _clientWaitingTime, _clientDamege,
        (TypeOfMealAvailable)randomMeal, _clientQuantityOfOrder, _clientType, reception);
        StartCoroutine(ClientsComing());
    }
    private IEnumerator Disapear()
    {
        yield return new WaitForSeconds(2);
        //_alertText.text = string.Empty;
        StopCoroutine(Disapear());
    }
    public void GetAvailableStateOfReception(Reception reception,  bool state)
    {
        if (reception  == Reception.CheckIn1)
        {
            _isreception1Available = state;
        }
        else if (reception == Reception.CheckIn2)
        {
            _isreception2Available = state;
        }
    }
    public void GetoFrenezy(int _clientUntilSpecial)
    {
        _clientAttendedUntilSpecial = _clientUntilSpecial;
        if (_clientUntilSpecial == 0)
        {
            _onFrenezy = true;
        }
        else
        {
             _onFrenezy = false;
        }
    }
}