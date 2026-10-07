using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EventSystem : MonoBehaviour
{
    [SerializeField] private float _frenezyDuration;
    private OrderSystem _orderSystem;
    private float _presentTimeFrenezy;
    private Image _frenezyTimeBar;
    private bool _isFrenezyHappening = false;
    private void Start()
    {
        _orderSystem = GameController.Instance.OrderSystem;
        _frenezyTimeBar = GameController.Instance.FrenezyTimeBar;
    }
    public void StartChanceOfEvent(float arrivalTime)
    {
        StartCoroutine(EventChance(arrivalTime));
    }
    private IEnumerator EventChance(float arrivalTime)
    {
        yield return new WaitUntil(() => !_isFrenezyHappening);
        int EventChanceInt = Random.Range(4, 5);
        print("Foi o antre switch e ranfodm");
        switch (EventChanceInt)
        {
            case 0:
                yield break;
            case 1: 
                yield break;
            case 2: 
                yield break;
            case 3: 
                yield break;
            case 4:
                StartCoroutine(FrenezyControlEvent(arrivalTime));
                yield break;
            case 5: 
                yield break;
            case 6: 
                yield break;
            case 7: 
                yield break;
            case 8: 
                yield break;
            case 9: 
                yield break;

        }
        StartCoroutine(EventChance(arrivalTime));
    }
    private IEnumerator FrenezyControlEvent(float _arrivalInterval)
    {
        _isFrenezyHappening = true;
        _arrivalInterval /= 2;
        _orderSystem.GetoFrenezy(_arrivalInterval);
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
        _orderSystem.GetoFrenezy(_arrivalInterval);
        _isFrenezyHappening = false;
    }
}
