using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] private int _slotsAvailable;
    private int _currentlife;
    private bool _isSlot1Avalable = true;
    private bool _isSlot2Avalable = true;
    private bool _isSlot3Avalable = true;
    private bool _isSlot4Avalable = true;
    private TMP_Text _lifeText;
    private GameObject _losePainel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        clientSystem = GameController.Instance.ClientSystem;
        StartCoroutine(ClientsComing());
        _lifeText = GameController.Instance.LifeText;
        _losePainel = GameController.Instance.LosePainel;
        _currentlife = _maxlife;
        _lifeText.text = _currentlife.ToString() + "/" + _maxlife.ToString();
        _losePainel.SetActive(false);
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
    public void ClientAtended()
    {
        _clientsAtended++;
    }
    private IEnumerator ClientsComing()
    {
        if (_isSlot1Avalable == false && _isSlot2Avalable == false && _isSlot3Avalable ==false && _isSlot4Avalable == false)
        {
            StartCoroutine(ClientsComing());
            yield break;
        }
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
        int randomSlot = Random.Range(1, _slotsAvailable + 1);
        int randomMeal = Random.Range(1, System.Enum.GetValues(typeof(TypeOfMealAvailable)).Length + 1);
        print(randomSlot);
        switch (randomSlot)
        {
            case 1:
                if (_isSlot1Avalable != true)
                {
                    StartCoroutine(ClientsComing());
                    yield break;
                }
                clientSystem.NewClient(/*_clientSprite,*/ _clientWaitingTime, Slot.Slot1,_damege, (TypeOfMealAvailable)randomMeal);
                break;
            case 2:
                if (_isSlot2Avalable != true)
                {
                    StartCoroutine(ClientsComing());
                    yield break;
                }
                clientSystem.NewClient(/*_clientSprite,*/ _clientWaitingTime, Slot.Slot2, _damege, (TypeOfMealAvailable)randomMeal);
                break;
            case 3:
                if (_isSlot3Avalable != true)
                {
                    StartCoroutine(ClientsComing());
                    yield break;
                }
                clientSystem.NewClient(/*_clientSprite,*/ _clientWaitingTime, Slot.Slot3, _damege, (TypeOfMealAvailable)randomMeal);
                break;
            case 4:
                if (_isSlot4Avalable != true)
                {
                    StartCoroutine(ClientsComing());
                    yield break;
                }
                clientSystem.NewClient(/*_clientSprite,*/ _clientWaitingTime, Slot.Slot4, _damege, (TypeOfMealAvailable)randomMeal);
                break;
        }
        StartCoroutine(ClientsComing());
    }
}
