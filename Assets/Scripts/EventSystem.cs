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
    [SerializeField] private float _timeForTryAgain;
    private void Start()
    {
        _orderSystem = GameController.Instance.OrderSystem;
        _frenezyTimeBar = GameController.Instance.FrenezyTimeBar;
    }
    public void StartChanceOfEvent()
    {
        StartCoroutine(EventChance());
    }
    private IEnumerator EventChance()
    {
        while (true)
        {
            yield return new WaitUntil(() => !_isFrenezyHappening);
            yield return new WaitForSeconds(_timeForTryAgain);
            int EventChanceInt = Random.Range(0, 10);
            switch (EventChanceInt)
            {
                case 0:
                    print(EventChanceInt.ToString());
                    break;
                case 1:
                    print(EventChanceInt.ToString());
                    break;
                case 2:
                    print(EventChanceInt.ToString());
                    break;
                case 3:
                    print(EventChanceInt.ToString());
                    break;
                case 4:
                    print(EventChanceInt.ToString());
                    StartCoroutine(FrenezyControlEvent());
                    break;
                case 5:
                    print(EventChanceInt.ToString());
                    break;
                case 6:
                    print(EventChanceInt.ToString());
                    break;
                case 7:
                    print(EventChanceInt.ToString());
                    break;
                case 8:
                    print(EventChanceInt.ToString());
                    break;
                case 9:
                    print(EventChanceInt.ToString());
                    break;

            }
        }
    }
    private IEnumerator FrenezyControlEvent()
    {
        print("Frenezy");
        _isFrenezyHappening = true;
        _orderSystem.GetoFrenezy(0);
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
        int ramdomClient = Random.Range(6, 14);
        _orderSystem.GetoFrenezy(ramdomClient);
        print("frenezyOver");
        _isFrenezyHappening = false;
    }
}
