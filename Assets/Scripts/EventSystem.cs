using System.Collections;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    private OrderSystem _orderSystem;
    private void Start()
    {
        _orderSystem = GameController.Instance.OrderSystem;
    }
    public void StartChanceOfEvent()
    {
        StartCoroutine(EventChance());
    }
    private IEnumerator EventChance()
    {
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
                _orderSystem.GetoFrenezy();
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
        StartCoroutine(EventChance());
    }
}
