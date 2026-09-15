using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public OrderSystem OrderSystem { get => orderSystem;}
    public ClientSystem ClientSystem { get => clientSystem;}
    public MealType MealType { get => mealType;}
    public ClientElement[] ClientsElements { get => clientsElements;}

    [SerializeField] private OrderSystem orderSystem;
    [SerializeField] private ClientSystem clientSystem;
    [SerializeField] private MealType mealType;
    [SerializeField] private ClientElement[] clientsElements;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
    }
}
