using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestaurantLife : MonoBehaviour
{
    [SerializeField] private int _maxlife = 3;
    private int _currentLife;
    private TMP_Text _lifeText;
    private GameObject _losePainel;

    private void Start()
    {
        _lifeText = GameController.Instance.LifeText;
        _losePainel = GameController.Instance.LosePainel;
        _currentLife = _maxlife;
    }
    public void TakeDamege(int damege)
    {
        if (_currentLife >= _maxlife)
        {
            _currentLife = _maxlife;
        }
        _currentLife -= damege;
        _lifeText.text = _currentLife.ToString() + "/" + _maxlife.ToString();
        if (_currentLife <= 0)
        {
            _losePainel.SetActive(true);
            StopAllCoroutines();
            Time.timeScale = 0;
        }
    }
    public void Heal()
    {
        if (_currentLife >= _maxlife)
        {
            _currentLife = _maxlife;
            _lifeText.text = _currentLife.ToString() + "/" + _maxlife.ToString();
        }
        else
        {
            _currentLife++;
            _lifeText.text = _currentLife.ToString() + "/" + _maxlife.ToString();
        }
    }
    public void ResetScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
