using UnityEngine;

public class Rats : MonoBehaviour
{
    private RatsSpawner _spawner;
    [SerializeField] private int _ratNumber;
    public void Click()
    {
        gameObject.SetActive(false);
        _spawner.RatsClicked(_ratNumber);
    }
    private void Start()
    {
        _spawner = GameController.Instance.RatsSpawner;
    }
}
