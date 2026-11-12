using UnityEngine;

public class Rats : MonoBehaviour
{
    private RatsSpawner _spawner;
    [SerializeField] private int _ratNumber;
    private bool _isonPlace;
    public void Click()
    {
        if (_isonPlace == false)
            return;
        gameObject.SetActive(false);
        _isonPlace = false;
        _spawner.RatsClicked(_ratNumber);
    }
    public void RatAppered()
    {
        _isonPlace = true;
    }
    private void Start()
    {
        _spawner = GameController.Instance.RatsSpawner;
    }
}
