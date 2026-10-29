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
    public Image MotivationTimeBar { get => _motivationTimeBar;}
    public RestaurantLife RestaurantLife { get => restaurantLife;}
    public TMP_Text BestTotalClientsAtendedText { get => _bestTotalClientsAtendedText;}
    public EventSystem EventSystem { get => eventSystem;}
    public Image FrenezyTimeBar { get => _frenezyTimeBar;}
    public Image BackGroundColor { get => _backGroundColor;}
    public Image DesmotivationTimeBar { get => _desmotivationTimeBar;}
    public GameObject Blur { get => _blur;}
    public float TimeofBlur { get => _timeofBlur;}
    public GameObject[] Rats { get => _rats;}
    public RatsSpawner RatsSpawner { get => ratsSpawner;}
    public GameObject SwitchSceneButton { get => _switchSceneButton;}
    public Store Store { get => store;}
    public int EndOfDay { get => _endOfDay;}
    public int[] EndOfDaysValue { get => _endOfDaysValue;}
    public TMP_Text TimeOfCookingBeingOnTextShop { get => _timeOfCookingBeingOnTextShop;}
    public float AppetizerPrize { get => _appetizerPrize;}
    public float DessertPrize { get => _dessertPrize;}
    public TMP_Text MoneyText { get => _moneyText;}

    [Header("Scripts")]
    [SerializeField] private OrderSystem orderSystem;
    [SerializeField] private ClientSystem clientSystem;
    [SerializeField] private MealType mealType;
    [SerializeField] private ClientElement[] clientsElements;
    [SerializeField] private RestaurantLife restaurantLife;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private RatsSpawner ratsSpawner;
    [SerializeField] private Store store;
    [Header("GameObject")]
    [SerializeField] private GameObject _losePainel;
    [SerializeField] private GameObject _blur;
    [SerializeField] private GameObject[] _rats;
    [SerializeField] private GameObject _switchSceneButton;
    [Header("Text")]
    [SerializeField] private TMP_Text appetizerText;
    [SerializeField] private TMP_Text dessertText;
    [SerializeField] private TMP_Text _alertText;
    [SerializeField] private TMP_Text _lifeText;
    [SerializeField] private TMP_Text _orderDonesText;
    [SerializeField] private TMP_Text _totalClientsAtendedText;
    [SerializeField] private TMP_Text _bestTotalClientsAtendedText;
    [SerializeField] private TMP_Text _timeOfCookingBeingOnTextShop;
    [SerializeField] private TMP_Text _moneyText;
    [Header("Button")]
    [SerializeField] private Button _appetizerButton;
    [SerializeField] private Button _dessertButton;
    [Header("Image")]
    [SerializeField] private Image[] _orderDisplay;
    [SerializeField] private Image[] _timeBarSlots;
    [SerializeField] private Image[] _timeBarMeals;
    [SerializeField] private Image _motivationTimeBar;
    [SerializeField] private Image _desmotivationTimeBar;
    [SerializeField] private Image _frenezyTimeBar;
    [SerializeField] private Image _backGroundColor;
    [Header("Sprite")]
    [SerializeField] private Sprite[] _mealSprite;
    [Header("Audio")]
    [SerializeField] private AudioSource _bell;
    [Header("Float")]
    [SerializeField] private float _timeofBlur;
    [SerializeField] private float _appetizerPrize;
    [SerializeField] private float _dessertPrize;
    [Header("Int")]
    [SerializeField] private int _endOfDay;
    [SerializeField] private int[] _endOfDaysValue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
    }
}
