using UnityEngine;

public class SortingZoneSwitcher : MonoBehaviour
{
    [SerializeField] private Transform[] _zones;

    private Vector3[] _originalPositions;

    private void Awake()
    {
        _originalPositions = new Vector3[_zones.Length];

        for (int i = 0; i < _zones.Length; i++)
            _originalPositions[i] = _zones[i].position;
    }

    public void ShuffleZones()
    {
        Vector3[] availablePositions =
            (Vector3[])_originalPositions.Clone();

        for (int i = availablePositions.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            (
                availablePositions[i],
                availablePositions[randomIndex]
            ) =
            (
                availablePositions[randomIndex],
                availablePositions[i]
            );
        }

        for (int i = 0; i < _zones.Length; i++)
            _zones[i].position = availablePositions[i];
    }

    public void ResetZones()
    {
        for (int i = 0; i < _zones.Length; i++)
            _zones[i].position = _originalPositions[i];
    }
}