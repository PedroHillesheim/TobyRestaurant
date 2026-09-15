using System.Collections;
using UnityEngine;

public class OrderSystem : MonoBehaviour
{
    private TypeOfMealAvailable[] _typeOfMeals;
    [Header("Client")]
    private Sprite _clientSprite;
    private float _clientWaitingTime;
    private int _damege;
    [Header("Normal Client")]
    [SerializeField] private Sprite _normalClientSprite;
    [SerializeField] private float _normalClientWaitingTime;
    [SerializeField] private int _normalClientDamege;
    [Header("Food critic")]
    [SerializeField] private Sprite _criticClientSprite;
    [SerializeField] private float _criticWaitingTime;
    [SerializeField] private int _criticDamege;
    [Header("Values")]
    [SerializeField] private ClientSystem clientSystem;
    [SerializeField] private int _maxlife = 3;
    [SerializeField] private float _arrivalInterval = 5f;
    [SerializeField] private int _clientAttendedUntilCritic = 8;
    private int _clientsAtended;
    private int _slotsAvailable;
    private int _currentlife;
    private bool _isSlot1Avalable = true;
    private bool _isSlot2Avalable = true;
    private bool _isSlot3Avalable = true;
    private bool _isSlot4Avalable = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ClientsComing());
    }

    // Update is called once per frame
    void Update()
    {
        
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
    public void TakeDamege(int damege)
    {
        if (_currentlife >= _maxlife)
        {
            _currentlife = _maxlife;
            return;
        }       
        _currentlife = damege;
        if (_currentlife <= 0)
        {
            throw new System.NotImplementedException("Death is not programmed");
        }
    }
    public void ClientAtended()
    {
        _clientsAtended++;
    }
    private IEnumerator ClientsComing()
    {
        if(_clientsAtended >= _clientAttendedUntilCritic)
        {
            _clientSprite = _criticClientSprite;
            _clientWaitingTime = _criticWaitingTime;
            _damege = _criticDamege;
            _clientsAtended = 0;
        }
        else
        {
            _clientSprite = _normalClientSprite;
            _clientWaitingTime = _normalClientWaitingTime;
            _damege = _normalClientDamege;
        }
        yield return new WaitForSeconds(_arrivalInterval);
        int randomSlot = Random.Range(0, _slotsAvailable);
        int randomMeal = Random.Range(1, 3);
        switch (randomSlot)
        {
            case 1:
                if (_isSlot1Avalable != true)
                {
                    StartCoroutine(ClientsComing());
                }
                clientSystem.NewClient(_clientSprite, _clientWaitingTime, Slot.Slot1,_damege, _typeOfMeals[randomMeal]);
                break;
            case 2:
                if (_isSlot2Avalable != true)
                {
                    StartCoroutine(ClientsComing());
                }
                clientSystem.NewClient(_clientSprite, _clientWaitingTime, Slot.Slot2, _damege, _typeOfMeals[randomMeal]);
                break;
            case 3:
                if (_isSlot3Avalable != true)
                {
                    StartCoroutine(ClientsComing());
                }
                clientSystem.NewClient(_clientSprite, _clientWaitingTime, Slot.Slot3, _damege, _typeOfMeals[randomMeal]);
                break;
            case 4:
                if (_isSlot4Avalable != true)
                {
                    StartCoroutine(ClientsComing());
                }
                clientSystem.NewClient(_clientSprite, _clientWaitingTime, Slot.Slot4, _damege, _typeOfMeals[randomMeal]);
                break;
        }
        StartCoroutine(ClientsComing());
    }
}
