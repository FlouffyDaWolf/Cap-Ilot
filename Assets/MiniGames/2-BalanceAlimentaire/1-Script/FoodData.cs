using UnityEngine;

[CreateAssetMenu(
    fileName = "FoodData",
    menuName = "MiniGames/Balance/Food Data"
)]
public class FoodData : ScriptableObject
{
    [SerializeField] private string _foodName;
    [SerializeField] private FoodCategory _category;
    [SerializeField] private Sprite _sprite;

    public string FoodName
    {
        get => _foodName;
    }

    public FoodCategory Category
    {
        get => _category;
    }

    public Sprite Sprite
    {
        get => _sprite;
    }
}