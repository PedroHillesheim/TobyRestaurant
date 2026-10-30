using UnityEngine;
using UnityEngine.Events;

public class CheatCoded : MonoBehaviour
{
    [SerializeField] private UnityEvent _cheatCodeDaySwap;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _cheatCodeDaySwap.Invoke();
        }
    }
}
