using System;
using UnityEngine;

public static class RessourcesManager
{
    // --------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
    // ----------------------------------------------------------------------------- Variables ----------------------------------------------------------------------------- //
    // --------------------------------------------------------------------------------------------------------------------------------------------------------------------- //

    // --------------------------- Public Variables --------------------------- //
    // Events
    public static event Action<int> OnStarsChanged;
    public static event Action<int> OnFoodChanged;
    public static event Action<int> OnDiamondsChanged;

    // Current Resources
    public static int Stars { get; private set; }
    public static int Food { get; private set; }
    public static int Diamonds { get; private set; }

    // Max Resources
    public static int MaxStars { get; private set; } = 999;
    public static int MaxFood { get; private set; } = 999;
    public static int MaxDiamonds { get; private set; } = 999;

    // -------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
    // ----------------------------------------------------------------------------- Public Methods ----------------------------------------------------------------------------- //
    // -------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //

    // --------------------------- Main Methods --------------------------- //
    // Add Ressources
    public static void AddStars(int value)
    {
        Stars = Mathf.Min(Stars + value, MaxStars);
        OnStarsChanged?.Invoke(Stars);
    }

    public static void AddFood(int value)
    {
        Food = Mathf.Min(Food + value, MaxFood);
        OnFoodChanged?.Invoke(Food);
    }

    public static void AddDiamonds(int value)
    {
        Diamonds = Mathf.Min(Diamonds + value, MaxDiamonds);
        OnDiamondsChanged?.Invoke(Diamonds);
    }

    // Remove Ressources
    public static void RemoveStars(int value)
    {
        Stars = Mathf.Max(Stars - value, 0);
        OnStarsChanged?.Invoke(Stars);
    }

    public static void RemoveFood(int value)
    {
        Food = Mathf.Max(Food - value, 0);
        OnFoodChanged?.Invoke(Food);
    }

    public static void RemoveDiamonds(int value)
    {
        Diamonds = Mathf.Max(Diamonds - value, 0);
        OnDiamondsChanged?.Invoke(Diamonds);
    }

    // Check if there are enough resources
    public static bool HasEnoughStars(int value) { return Stars >= value; }
    public static bool HasEnoughFood(int value) { return Food >= value; }
    public static bool HasEnoughDiamonds(int value) { return Diamonds >= value; }

    // Reset Resources
    public static void ResetResources()
    {
        Stars = 0;
        Food = 0;
        Diamonds = 0;
    }
    public static void ResetStars() { Stars = 0; }
    public static void ResetFood() { Food = 0; }
    public static void ResetDiamonds() { Diamonds = 0; }
}