using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RatsSpawner : MonoBehaviour
{
    private List<int> _ratsNotinScene = new List<int>();
    private GameObject[] _rats;
    private RestaurantLife _restaurantLife;
    [SerializeField] private float _ratspawnIntervalMin;
    [SerializeField] private float _ratspawnIntervalMax;
    [SerializeField] private AudioSource _ratsound;
    private bool _AfterFisrtSpecial = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rats = GameController.Instance.Rats;
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
    private IEnumerator Ratspawner()
    {
        //yield return new WaitUntil(() => _AfterFisrtSpecial);
        int _ramdomRats = Random.Range(0, _ratsNotinScene.Count);
        float _randomRatspawnInterval = Random.Range(_ratspawnIntervalMin, _ratspawnIntervalMax);
        yield return new WaitForSeconds(_randomRatspawnInterval);
        print("Ratoooooooooo!");
        _rats[_ramdomRats].SetActive(true);
        _ratsNotinScene.RemoveAt(_ramdomRats);
        if(_ratsNotinScene == null)
        {
            _restaurantLife.TakeDamege(3);
        }
    }
}
