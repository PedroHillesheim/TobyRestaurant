using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public OrderSystem OrderSystem { get => orderSystem;}
    public ClientSystem ClientSystem { get => clientSystem;}
    public MealType MealTypo { get => mealType;}
    public ClientElement[] ClientsElements { get => clientsElements;}
    public GameObject LosePainel { get => _losePainel; }
    public TMP_Text AppetizerText { get => appetizerText;}
    public TMP_Text DessertText { get => dessertText;}
    public TMP_Text AlertText { get => _alertText; }
    public TMP_Text LifeText { get => _lifeText;}
    public Button AppetizerButton { get => _appetizerButton;}
    public Button DessertButton { get => _dessertButton;}
    public Image[] OrderDisplay { get => _orderDisplay;}
    public Sprite[] MealSprite { get => _mealSprite;}
    public Image[] TimeBar { get => _timeBarSlots;}
    public Image[] TimeBarMeals { get => _timeBarMeals;}
    public AudioSource Bell { get => _bell;}
    public TMP_Text OrderDonesText { get => _orderDonesText;}
    public TMP_Text TotalClientsAtendedText { get => _totalClientsAtendedText;}

    [Header("Scripts")]
    [SerializeField] private OrderSystem orderSystem;
    [SerializeField] private ClientSystem clientSystem;
    [SerializeField] private MealType mealType;
    [SerializeField] private ClientElement[] clientsElements;
    [Header("GameObject")]
    [SerializeField] private GameObject _losePainel;
    [Header("Text")]
    [SerializeField] private TMP_Text appetizerText;
    [SerializeField] private TMP_Text dessertText;
    [SerializeField] private TMP_Text _alertText;
    [SerializeField] private TMP_Text _lifeText;
    [SerializeField] private TMP_Text _orderDonesText;
    [SerializeField] private TMP_Text _totalClientsAtendedText;
    [Header("Button")]
    [SerializeField] private Button _appetizerButton;
    [SerializeField] private Button _dessertButton;
    [Header("Image")]
    [SerializeField] private Image[] _orderDisplay;
    [SerializeField] private Image[] _timeBarSlots;
    [SerializeField] private Image[] _timeBarMeals;
    [Header("Sprite")]
    [SerializeField] private Sprite[] _mealSprite;
    [Header("Audio")]
    [SerializeField] private AudioSource _bell;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
    }
}
