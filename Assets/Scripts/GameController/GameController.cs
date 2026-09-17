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
    public TMP_Text AppetizerText { get => appetizerText;}
    public TMP_Text DessertText { get => dessertText;}
    public Button AppetizerButton { get => _appetizerButton;}
    public Button DessertButton { get => _dessertButton;}
    public TMP_Text AlertText { get => _alertText;}

    [Header("Scripts")]
    [SerializeField] private OrderSystem orderSystem;
    [SerializeField] private ClientSystem clientSystem;
    [SerializeField] private MealType mealType;
    [SerializeField] private ClientElement[] clientsElements;
    [Header("Text")]
    [SerializeField] private TMP_Text appetizerText;
    [SerializeField] private TMP_Text dessertText;
    [SerializeField] private TMP_Text _alertText;
    [Header("Button")]
    [SerializeField] private Button _appetizerButton;
    [SerializeField] private Button _dessertButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
    }
}
