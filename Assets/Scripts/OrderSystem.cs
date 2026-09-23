using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OrderSystem : MonoBehaviour
{
    private List<int> _avalableSlot = new List<int>();
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
    [SerializeField] private int _slotsAvailable;
    private int _currentlife;
    private bool _isSlot1Avalable = true;
    private bool _isSlot2Avalable = true;
    private bool _isSlot3Avalable = true;
    private bool _isSlot4Avalable = true;
    private TMP_Text _lifeText;
    private GameObject _losePainel;
    private TMP_Text _alertText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        clientSystem = GameController.Instance.ClientSystem;
        StartCoroutine(ClientsComing());
        _lifeText = GameController.Instance.LifeText;
        _losePainel = GameController.Instance.LosePainel;
        _currentlife = _maxlife;
        _alertText = GameController.Instance.AlertText;
        _lifeText.text = _currentlife.ToString() + "/" + _maxlife.ToString();
        _losePainel.SetActive(false);
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
    public void TakeDamege(int damege)
    {
        if (_currentlife >= _maxlife)
        {
            _currentlife = _maxlife;
        }       
        _currentlife -= damege;
        _lifeText.text = _currentlife.ToString() + "/" + _maxlife.ToString();
        if (_currentlife <= 0)
        {
            _losePainel.SetActive(true);
            StopAllCoroutines();
            Time.timeScale = 0;
        }
    }
    public void ResetScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        if (_isSlot1Avalable == false && _isSlot2Avalable == false && _isSlot3Avalable ==false && _isSlot4Avalable == false)
        {
            StartCoroutine(ClientsComing());
            yield break;
        }
        if(_clientsAtended >= _clientAttendedUntilCritic -1)
        {
            print("Critic Coming");
            _clientSprite = _criticClientSprite;
            _clientWaitingTime = _criticWaitingTime;
            _damege = _criticDamege;
            int _randomClientAtended = Random.Range(6, 15);
            _clientAttendedUntilCritic = _randomClientAtended;
            _clientsAtended = 0;
        }
        else
        {
            _clientSprite = _normalClientSprite;
            _clientWaitingTime = _normalClientWaitingTime;
            _damege = _normalClientDamege;
        }
        yield return new WaitForSeconds(_arrivalInterval);
        int randomSlot = Random.Range(1, _avalableSlot.Count);
        int randomMeal = Random.Range(1, 3);
        if (_damege >= 3)
        {
            _alertText.text = "The critic arrived";
            StartCoroutine(Disapear());
        }
        switch (randomSlot)
        {
            case 1:
                if (_isSlot1Avalable != true)
                {
                    StartCoroutine(ClientsComing());
                    yield break;
                }
                clientSystem.NewClient(/*_clientSprite,*/ _clientWaitingTime, Slot.Slot1, _damege, (TypeOfMealAvailable)randomMeal);
                _avalableSlot.Remove(1);
                break;
            case 2:
                if (_isSlot2Avalable != true)
                {
                    StartCoroutine(ClientsComing());
                    yield break;
                }
                clientSystem.NewClient(/*_clientSprite,*/ _clientWaitingTime, Slot.Slot2, _damege, (TypeOfMealAvailable)randomMeal);
                _avalableSlot.Remove(2);
                break;
            case 3:
                if (_isSlot3Avalable != true)
                {
                    StartCoroutine(ClientsComing());
                    yield break;
                }
                clientSystem.NewClient(/*_clientSprite,*/ _clientWaitingTime, Slot.Slot3, _damege, (TypeOfMealAvailable)randomMeal);
                _avalableSlot.Remove(3);
                break;
            case 4:
                if (_isSlot4Avalable != true)
                {
                    StartCoroutine(ClientsComing());
                    yield break;
                }
                clientSystem.NewClient(/*_clientSprite,*/ _clientWaitingTime, Slot.Slot4, _damege, (TypeOfMealAvailable)randomMeal);
                _avalableSlot.Remove(4);
                break;
        }
        StartCoroutine(ClientsComing());
    }
    private IEnumerator Disapear()
    {
        yield return new WaitForSeconds(2);
        _alertText.text = string.Empty;
    }
}
