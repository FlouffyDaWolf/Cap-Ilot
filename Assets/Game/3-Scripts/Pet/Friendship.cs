using UnityEngine;

public class Friendship : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] private int _friendshipValue;

    public int FriendshipValue
    {
        get => _friendshipValue;
    }

    public FriendshipLevel CurrentLevel
    {
        get
        {
            if (_friendshipValue >= 70)
                return FriendshipLevel.High;

            if (_friendshipValue >= 35)
                return FriendshipLevel.Medium;

            return FriendshipLevel.Low;
        }
    }
}