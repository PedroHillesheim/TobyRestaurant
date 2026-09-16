using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public OrderSystem OrderSystem { get => orderSystem;}
    public ClientSystem ClientSystem { get => clientSystem;}
    public MealType MealTypo { get => mealType;}
    public ClientElement[] ClientsElements { get => clientsElements;}
    public TMP_Text AppetizerText { get => appetizerText;}
    public TMP_Text DessertText { get => dessertText;}

    [Header("Scripts")]
    [SerializeField] private OrderSystem orderSystem;
    [SerializeField] private ClientSystem clientSystem;
    [SerializeField] private MealType mealType;
    [SerializeField] private ClientElement[] clientsElements;
    [Header("Text")]
    [SerializeField] private TMP_Text appetizerText;
    [SerializeField] private TMP_Text dessertText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
    }
}
