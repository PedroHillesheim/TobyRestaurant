using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RatsSpawner : MonoBehaviour
{
    private List<int> _ratsNotinScene = new List<int>();
    private GameObject[] _rats;
    private RestaurantLife _restaurantLife;
    private GameObject _switchSceneButton;
    [SerializeField] private float _ratspawnIntervalMin;
    [SerializeField] private float _ratspawnIntervalMax;
    [SerializeField] private AudioSource _ratsound;
    [SerializeField] private UnityEvent _ActivateThatButtonThatDecideToNot;
    private bool _afterFisrtSpecial = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rats = GameController.Instance.Rats;
        _restaurantLife = GameController.Instance.RestaurantLife;
        _switchSceneButton = GameController.Instance.SwitchSceneButton;
        for (int i = 0; i <= 4; i++)
        {
            _ratsNotinScene.Add(i);
            _rats[i].SetActive(false);
        }
        StartCoroutine(Ratspawner());
    }
    public void RatsClicked(int _ratsNumber)
    {
        print("Va emboraaa");
        _ratsNotinScene.Add(_ratsNumber);
    }
    public void StartRatspawner()
    {
        _afterFisrtSpecial = true;
        _switchSceneButton.SetActive(true);
    }
    public void EndOFDay()
    {

    }
    private IEnumerator Ratspawner()
    {
        yield return new WaitUntil(() => _afterFisrtSpecial);
        if (_ratsNotinScene.Count <= 0)
        {
            _restaurantLife.TakeDamege(3);
            yield break;
        }
        int randomIndex = Random.Range(0, _ratsNotinScene.Count);
        int _randomRats = _ratsNotinScene[randomIndex];
        float _randomRatspawnInterval = Random.Range(_ratspawnIntervalMin, _ratspawnIntervalMax);
        yield return new WaitForSeconds(_randomRatspawnInterval);
        print("Ratoooooooooo!");
        _rats[_randomRats].SetActive(true);
        _ratsNotinScene.Remove(_randomRats);
        if (_ratsNotinScene.Count <= 0)
        {
            _restaurantLife.TakeDamege(3);
            yield break;
        }
        StartCoroutine(Ratspawner());
    }
}
