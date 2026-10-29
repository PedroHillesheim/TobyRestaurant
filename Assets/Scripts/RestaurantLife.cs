using System.IO;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveScore
{
    public int _highestTotalClientsAtendede;
    public SaveScore(int highestTotalClientsAtended)
    {
        _highestTotalClientsAtendede = highestTotalClientsAtended;
    }
    public int HighestScore { get => _highestTotalClientsAtendede; }
}
public class RestaurantLife : MonoBehaviour
{
    private Day _day = Day.Day1;
    [SerializeField] private int _maxlife = 3;
    [SerializeField] private UnityEvent _endOfDayEvent;
    private ClientElement[] _clientElement;
    private Image _backGroundColor;
    private OrderSystem _orderSystem;
    private int _currentLife;
    private TMP_Text _lifeText;
    private GameObject _losePainel;
    private int _highestTotalClientsAtended;
    private TMP_Text _orderDoneText;
    private TMP_Text _totalClientAtendedText;
    private TMP_Text _bestTotalClientAtendedText;
    private int _clientsAtendedTotal;
    private int _nextChangeofColor = 20;
    private int _endOfDay;
    private int[] _endOfDaysValue;

    private void Start()
    {
        _lifeText = GameController.Instance.LifeText;
        _losePainel = GameController.Instance.LosePainel;
        _totalClientAtendedText = GameController.Instance.TotalClientsAtendedText;
        _bestTotalClientAtendedText = GameController.Instance.BestTotalClientsAtendedText;
        _orderSystem = GameController.Instance.OrderSystem;
        _orderDoneText = GameController.Instance.OrderDonesText;
        _backGroundColor = GameController.Instance.BackGroundColor;
        _endOfDay = GameController.Instance.EndOfDay;
        _endOfDaysValue = GameController.Instance.EndOfDaysValue;
        _clientElement = GameController.Instance.ClientsElements;
        _currentLife = _maxlife;
        _orderDoneText.text = _clientsAtendedTotal.ToString();
        _lifeText.text = _currentLife.ToString() + "/" + _maxlife.ToString();
        LoadHighestScore();
        _losePainel.SetActive(false);
    }
    public void ClientAtended()
    {
        _clientsAtendedTotal++;
        _orderDoneText.text = _clientsAtendedTotal.ToString();
        if(_clientsAtendedTotal >= _nextChangeofColor)
        {
            _backGroundColor.color = Random.ColorHSV();
            _nextChangeofColor += 20;
        }
        VerifyBestScore();
    }
    private void SaveHighestScore()
    {
        SaveScore status = new SaveScore(_highestTotalClientsAtended);
        string json = JsonUtility.ToJson(status);
        JsonUtility.ToJson(json);

        string path = Application.persistentDataPath + "/bestScore.json";
        File.WriteAllText(path, json);
    }
    public void VerifyBestScore()
    {
        if(_clientsAtendedTotal >= _endOfDay)
        {
            if(_day == Day.Day1)
            {
                _day = Day.Day2;
                _endOfDay = _endOfDaysValue[1];
            }
            else if (_day == Day.Day2)
            {
                _day = Day.Day3;
                _endOfDay = _endOfDaysValue[2];
            }
            else if (_day == Day.Day3)
            {
                _day = Day.Day4;
                _endOfDay = _endOfDaysValue[3];
            }
            else
            {
                _day = Day.Day5;
                _endOfDay = _endOfDaysValue[4];
            }
            _clientsAtendedTotal = 0;
            for (int i = 0; i < _clientElement.Length; i++)
            {
                _clientElement[i].ResetScene();
            }
            _orderDoneText.text = _clientsAtendedTotal.ToString();
            _endOfDayEvent.Invoke();
        }
        if (_clientsAtendedTotal >= _highestTotalClientsAtended)
        {
            _highestTotalClientsAtended = _clientsAtendedTotal;
            _totalClientAtendedText.text = _clientsAtendedTotal.ToString();
            _bestTotalClientAtendedText.text = "";
            SaveHighestScore();
        }
        else
        {
            _totalClientAtendedText.text = _clientsAtendedTotal.ToString();
            _bestTotalClientAtendedText.text = _highestTotalClientsAtended.ToString();
        }
    }
    private void LoadHighestScore()
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/bestScore.json");
        SaveScore status = JsonUtility.FromJson<SaveScore>(json);
        _highestTotalClientsAtended = status.HighestScore;
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
