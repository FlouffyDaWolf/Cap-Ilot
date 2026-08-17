using UnityEngine;

public class SortingZone : MonoBehaviour
{
    [SerializeField] private BalanceGame _balanceGame;
    [SerializeField] private FoodCategory _acceptedCategory;

    public FoodCategory AcceptedCategory
    {
        get => _acceptedCategory;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Food food = other.GetComponent<Food>();

        if (food == null || _balanceGame == null)
            return;

        _balanceGame.HandleFoodEnteredZone(food, this);
    }
}