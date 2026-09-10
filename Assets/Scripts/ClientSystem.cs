using System.Collections;
using System.Collections.Generic;
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
public class ClientSystem : MonoBehaviour, IAmAClient
{
    [SerializeField] OrderSystem _orderSystem;
    [SerializeField] ClientElement _client;
    [SerializeField] ClientType _clientType;
    [SerializeField] private Slot _slot;
    private bool _isSlotAvalable = true;
    private Sprite _clientSprite;
    private float _clientWaitingTime;
    [SerializeField] private int _maxLimitOfOrder = 3;
    private int order1;
    private int order2;
    private int order3;
    private int order4;
    public void NewClient(Sprite clientSprite, float clientWaitingTime)
    {
        _isSlotAvalable = false;
        _orderSystem.GetConfirmationOfAvailableSlot(_slot, _isSlotAvalable);
        _clientSprite = clientSprite;
        _clientWaitingTime = clientWaitingTime;
        int order1Ramdom = Random.Range(1, _maxLimitOfOrder);
        int order2Ramdom = Random.Range(0, _maxLimitOfOrder);
        int order3Ramdom = Random.Range(0, _maxLimitOfOrder);
        int order4Ramdom = Random.Range(0, _maxLimitOfOrder);
        order1 = order1Ramdom;
        order2 = order2Ramdom;
        order3 = order3Ramdom;
        order4 = order4Ramdom;
        StartCoroutine(ClientWating());
        throw new System.NotImplementedException("New Client is not fully programmed");
    }
    public void Order(Slot slot)
    {
        if (_slot == slot)
        {
            if (order1 == 0 && order2 == 0 && order3 == 0 && order4 == 0)
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
        StopCoroutine(ClientWating());
        order1 = 0;
        order2 = 0;
        order3 = 0;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _clientSprite = null;
        _clientWaitingTime = 0;
    }
    private IEnumerator ClientWating()
    {
        yield return new WaitForSecondsRealtime(_clientWaitingTime);
        OrderDone();
    }
}
