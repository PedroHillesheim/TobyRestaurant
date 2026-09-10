using System.Collections;
using UnityEngine;

public class OrderSystem : MonoBehaviour
{
    [SerializeField] private ClientSystem clientSystem;
    [SerializeField] private int _maxlife = 3;
    [SerializeField] private float _arrivalInterval = 5f;
    private int _slotsAvailable;
    private int _currentlife;
    private bool _isSlot1Avalable = true;
    private bool _isSlot2Avalable = true;
    private bool _isSlot3Avalable = true;
    private bool _isSlot4Avalable = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
            return;
        _currentlife = damege;
        if (_currentlife <= 0)
        {
            throw new System.NotImplementedException("Death is not programmed");
        }
    }
    private IEnumerator ClientsComing()
    {
        yield return new WaitForSeconds(4);
        int randomSlot = Random.Range(0, _slotsAvailable + 1);
        if (randomSlot == 1)
        {
            if (_isSlot1Avalable != true)
                yield return null;

        }
    }
}
