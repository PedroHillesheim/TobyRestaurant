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
    private bool _firstTime = true;
    private bool _onFrenezy = false;
    private TMP_Text _alertText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        clientSystem = GameController.Instance.ClientSystem;
        _alertText = GameController.Instance.AlertText;
        eventSystem = GameController.Instance.EventSystem;
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
    private IEnumerator ClientsComing()
    {
        while (true)
        {
            if (_avalableSlot.Count == 0)
            {
                print("No primeiro if que na teoria é inutil");
                continue;
            }
            if (_clientsAtended >= _clientAttendedUntilSpecial - 1)
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
                        _clientsAtended = 0;
                        if (_onFrenezy == false)
                        {
                            int _randomClientAtended = Random.Range(6, 15);
                            _clientAttendedUntilSpecial = _randomClientAtended;
                        }
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
                    eventSystem.StartChanceOfEvent();
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
            int randomIndex = Random.Range(0, _avalableSlot.Count);
            int randomSlot = _avalableSlot[randomIndex];
            _avalableSlot.RemoveAt(randomIndex);
            int randomMeal = Random.Range(1, 3);
            switch (randomSlot)
            {
                case 1:
                    if (_clientType == ClientsType.Critic)
                    {
                        _alertText.text = "The critic arrived";
                        StartCoroutine(Disapear());
                    }
                    else if (_clientType == ClientsType.Influencer)
                    {
                        _alertText.text = "An influencer arrived";
                        StartCoroutine(Disapear());
                    }
                    clientSystem.NewClient(/*_clientSprite,*/ _clientWaitingTime, Slot.Slot1, _damege,
                    (TypeOfMealAvailable)randomMeal, _quantityOfOrder, _clientType);
                    break;
                case 2:
                    if (_clientType == ClientsType.Critic)
                    {
                        _alertText.text = "The critic arrived";
                        StartCoroutine(Disapear());
                    }
                    else if (_clientType == ClientsType.Influencer)
                    {
                        _alertText.text = "An influencer arrived";
                        StartCoroutine(Disapear());
                    }
                    clientSystem.NewClient(/*_clientSprite,*/ _clientWaitingTime, Slot.Slot2, _damege,
                    (TypeOfMealAvailable)randomMeal, _quantityOfOrder, _clientType);
                    break;
                case 3:
                    if (_clientType == ClientsType.Critic)
                    {
                        _alertText.text = "The critic arrived";
                        StartCoroutine(Disapear());
                    }
                    else if (_clientType == ClientsType.Influencer)
                    {
                        _alertText.text = "An influencer arrived";
                        StartCoroutine(Disapear());
                    }
                    clientSystem.NewClient(/*_clientSprite,*/ _clientWaitingTime, Slot.Slot3, _damege,
                    (TypeOfMealAvailable)randomMeal, _quantityOfOrder, _clientType);
                    break;
                case 4:
                    if (_clientType == ClientsType.Critic)
                    {
                        _alertText.text = "The critic arrived";
                        StartCoroutine(Disapear());
                    }
                    else if (_clientType == ClientsType.Influencer)
                    {
                        _alertText.text = "An influencer arrived";
                        StartCoroutine(Disapear());
                    }
                    clientSystem.NewClient(/*_clientSprite,*/ _clientWaitingTime, Slot.Slot4, _damege,
                    (TypeOfMealAvailable)randomMeal, _quantityOfOrder, _clientType);
                    break;
            }
        }
    }
    private IEnumerator Disapear()
    {
        yield return new WaitForSeconds(2);
        _alertText.text = string.Empty;
        StopCoroutine(Disapear());
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