using UnityEngine;

public class ClientElement : MonoBehaviour
{
    [SerializeField] private Slot _slot;
    private Sprite _clientSprite;
    private float _clientWaitingTime;

    public ClientElement(Sprite clientSprite, float clientWaitingTime)
    {
        _clientSprite = clientSprite;
        _clientWaitingTime = clientWaitingTime;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

}
